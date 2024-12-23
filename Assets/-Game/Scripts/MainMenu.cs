using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu instance;

    #region Vari�veis de refer�ncia
    [Header("PAIN�IS DO MENU:")]
    [Tooltip("Adicionados na ordem:\n0 = MainMenu,\n1 = OptionMenu,\n2 = CreditsMenu")]
    [SerializeField] GameObject[] menuPanels;
    [Space(10)]
    [Header("SLIDER DO VOLUME:")]
    [Tooltip("Componente Slider que vai definir o volume principal do jogo.")]
    [SerializeField] Slider volumeSlider;
    [Space(10)]
    [Header("RESOLUTION DROPDOWN:")]
    [Tooltip("Componente Dropdown que vai definir a resolu��o de tela do jogo.")]
    [SerializeField] Dropdown resolutionDropdown;
    [Space(10)]
    [Header("FULL SCREEN TOGGLE:")]
    [Tooltip("Componente Toggle que vai definir o modo de tela do jogo.")]
    [SerializeField] Toggle fullscreenToggle;
    [Space(10)]
    [Header("CREDITS BUTTON:")]
    [Tooltip("Bot�o utilizado para abrir o painel dos cr�ditos.")]
    [SerializeField] GameObject creditsBtn;
    [Space(10)]
    [Header("PLAY BUTTON TEXT:")]
    [Tooltip("Componente de Texto do bot�o utilizado para iniciar o jogo.")]
    [SerializeField] Text playBtnTxt;
    #endregion

    private Canvas myCv;
    private AudioListener mainVolume;
    private Resolution[] resolutions;
    private bool showMenu;
    
    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject); // Mant�m este objeto entre cenas
        //}
        //else
        //{
        //    Destroy(gameObject); // Destroi a c�pia duplicada
        //}
    }

    void Start()
    {
        myCv = GetComponent<Canvas>();
        myCv.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        // Inicia com o menu principal ativo
        //ChangeMenu(0);

        #region Configura��o inicial do volume
        // Inicia com um volume padr�o
        // Verifica se h� um AudioListener na cena
        if (!mainVolume)
        {
            mainVolume = FindObjectOfType<AudioListener>();
        }
        else
        {
            // Se n�o houver, exibe uma mensagem de aviso
            Debug.LogWarning("Nenhum AudioListener encontrado na cena.");
        }
        volumeSlider.value = 0.5f;
        ChangeVolume();
        #endregion
        CheckCompatibleResolutions();
        ChangeScreenMode();
    }

    #region M�todos para os bot�es
    /// <summary>
    /// M�todo para alterar o modo de tela conforme o 'resolutionDropdown'
    /// </summary>
    public void ChangeScreenMode()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    /// <summary>
    /// M�todo para alterar a resolu��o conforme escolhida em 'resolutionDropdown'
    /// </summary>
    public void ChangeResolution()
    {
        // Altera a resolu��o para a escolhida no Dropdown
        Resolution selectedResolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// M�todo para alterar o valor do volume no Slider
    /// </summary>
    public void ChangeVolume()
    {
        // Verifica se h� um AudioListener na cena
        if (mainVolume)
        {
            AudioListener.volume = Mathf.Clamp01(volumeSlider.value);
        }
    }

    /// <summary>
    /// M�todo para definir qual painel da lista 'menuPanels' o bot�o ir� ativar
    /// O index � adicionado no bot�o
    /// </summary>
    public void ChangeMenu(int _index)
    {
        // Verifica se h� um painel com o index inserido
        if (_index < menuPanels.Length)
        {
            for (int i = 0; i < menuPanels.Length; i++)
            {
                menuPanels[i].SetActive(false);
            }
            menuPanels[_index].SetActive(true);
        }
        else
        {
            // Se n�o houver, exibe uma mensagem de aviso
            Debug.LogError("Painel n�o est� na lista!");
        }
    }

    /// <summary>
    /// M�todo para definir qual cena o bot�o ir� carregar e o comportamento dos bot�es conforme a cena. 
    /// O nome da cena � adicionado no bot�o
    /// </summary>
    //public void ChangeScene(string _name)
    //{
    //    if (SceneManager.GetActiveScene().name == "Game")
    //    {
    //        // Verifica se h� uma cena com o nome inserido
    //        if (IsSceneInBuild(_name))
    //        {
    //            myCv.enabled = false;
    //            ChangeMenu(0);
    //            Cursor.visible = false;
    //            Cursor.lockState = CursorLockMode.Locked;
    //            Time.timeScale = 1f;
    //        }
    //        else
    //        {
    //            // Se n�o houver, exibe uma mensagem de aviso
    //            Debug.LogError("Essa scene n�o existe no Build Index!\nO nome est� incorreto ou falta adicion�-la?");
    //        }
    //    }
    //    else
    //    {
    //        showMenu = false;
    //    }
    //}

    /// <summary>
    /// M�todo para fechar o jogo
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region M�todos de controle
    /// <summary>
    /// M�todo para obter e formatar as resolu��es compat�veis com o monitor
    /// </summary>
    private void CheckCompatibleResolutions()
    {
        resolutions = Screen.resolutions; // Obtenha as resolu��es suportadas
        resolutionDropdown.ClearOptions(); // Limpa as op��es do dropdown

        List<string> options = new List<string>();
        HashSet<string> uniqueResolutions = new HashSet<string>(); // Armazena resolu��es �nicas
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            // Considera apenas resolu��es com 59Hz
            if (resolutions[i].refreshRate == 60)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;

                // Adiciona apenas resolu��es �nicas
                if (uniqueResolutions.Add(option))
                {
                    options.Add(option);

                    // Verifica qual resolu��o � a atual para selecion�-la como padr�o
                    if (resolutions[i].width == Screen.currentResolution.width &&
                        resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = options.Count - 1;
                    }
                }
            }
        }

        resolutionDropdown.AddOptions(options); // Adiciona as resolu��es no dropdown
        resolutionDropdown.value = currentResolutionIndex; // Define a resolu��o atual como selecionada
        resolutionDropdown.RefreshShownValue(); // Atualiza o valor exibido
    }

    /// <summary>
    /// M�todo para verificar se uma cena existe no Build Index
    /// </summary>
    private bool IsSceneInBuild(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string currentScenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string currentSceneName = System.IO.Path.GetFileNameWithoutExtension(currentScenePath);

            if (currentSceneName.Equals(sceneName, System.StringComparison.OrdinalIgnoreCase))
            {
                return true; // A cena foi encontrada
            }
        }

        return false; // A cena n�o foi encontrada
    }
    #endregion
}
