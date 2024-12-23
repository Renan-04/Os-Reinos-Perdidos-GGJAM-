using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu instance;

    #region Variáveis de referência
    [Header("PAINÉIS DO MENU:")]
    [Tooltip("Adicionados na ordem:\n0 = MainMenu,\n1 = OptionMenu,\n2 = CreditsMenu")]
    [SerializeField] GameObject[] menuPanels;
    [Space(10)]
    [Header("SLIDER DO VOLUME:")]
    [Tooltip("Componente Slider que vai definir o volume principal do jogo.")]
    [SerializeField] Slider volumeSlider;
    [Space(10)]
    [Header("RESOLUTION DROPDOWN:")]
    [Tooltip("Componente Dropdown que vai definir a resolução de tela do jogo.")]
    [SerializeField] Dropdown resolutionDropdown;
    [Space(10)]
    [Header("FULL SCREEN TOGGLE:")]
    [Tooltip("Componente Toggle que vai definir o modo de tela do jogo.")]
    [SerializeField] Toggle fullscreenToggle;
    [Space(10)]
    [Header("CREDITS BUTTON:")]
    [Tooltip("Botão utilizado para abrir o painel dos créditos.")]
    [SerializeField] GameObject creditsBtn;
    [Space(10)]
    [Header("PLAY BUTTON TEXT:")]
    [Tooltip("Componente de Texto do botão utilizado para iniciar o jogo.")]
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
        //    DontDestroyOnLoad(gameObject); // Mantém este objeto entre cenas
        //}
        //else
        //{
        //    Destroy(gameObject); // Destroi a cópia duplicada
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

        #region Configuração inicial do volume
        // Inicia com um volume padrão
        // Verifica se há um AudioListener na cena
        if (!mainVolume)
        {
            mainVolume = FindObjectOfType<AudioListener>();
        }
        else
        {
            // Se não houver, exibe uma mensagem de aviso
            Debug.LogWarning("Nenhum AudioListener encontrado na cena.");
        }
        volumeSlider.value = 0.5f;
        ChangeVolume();
        #endregion
        CheckCompatibleResolutions();
        ChangeScreenMode();
    }

    #region Métodos para os botões
    /// <summary>
    /// Método para alterar o modo de tela conforme o 'resolutionDropdown'
    /// </summary>
    public void ChangeScreenMode()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    /// <summary>
    /// Método para alterar a resolução conforme escolhida em 'resolutionDropdown'
    /// </summary>
    public void ChangeResolution()
    {
        // Altera a resolução para a escolhida no Dropdown
        Resolution selectedResolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Método para alterar o valor do volume no Slider
    /// </summary>
    public void ChangeVolume()
    {
        // Verifica se há um AudioListener na cena
        if (mainVolume)
        {
            AudioListener.volume = Mathf.Clamp01(volumeSlider.value);
        }
    }

    /// <summary>
    /// Método para definir qual painel da lista 'menuPanels' o botão irá ativar
    /// O index é adicionado no botão
    /// </summary>
    public void ChangeMenu(int _index)
    {
        // Verifica se há um painel com o index inserido
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
            // Se não houver, exibe uma mensagem de aviso
            Debug.LogError("Painel não está na lista!");
        }
    }

    /// <summary>
    /// Método para definir qual cena o botão irá carregar e o comportamento dos botões conforme a cena. 
    /// O nome da cena é adicionado no botão
    /// </summary>
    //public void ChangeScene(string _name)
    //{
    //    if (SceneManager.GetActiveScene().name == "Game")
    //    {
    //        // Verifica se há uma cena com o nome inserido
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
    //            // Se não houver, exibe uma mensagem de aviso
    //            Debug.LogError("Essa scene não existe no Build Index!\nO nome está incorreto ou falta adicioná-la?");
    //        }
    //    }
    //    else
    //    {
    //        showMenu = false;
    //    }
    //}

    /// <summary>
    /// Método para fechar o jogo
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Métodos de controle
    /// <summary>
    /// Método para obter e formatar as resoluções compatíveis com o monitor
    /// </summary>
    private void CheckCompatibleResolutions()
    {
        resolutions = Screen.resolutions; // Obtenha as resoluções suportadas
        resolutionDropdown.ClearOptions(); // Limpa as opções do dropdown

        List<string> options = new List<string>();
        HashSet<string> uniqueResolutions = new HashSet<string>(); // Armazena resoluções únicas
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            // Considera apenas resoluções com 59Hz
            if (resolutions[i].refreshRate == 60)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;

                // Adiciona apenas resoluções únicas
                if (uniqueResolutions.Add(option))
                {
                    options.Add(option);

                    // Verifica qual resolução é a atual para selecioná-la como padrão
                    if (resolutions[i].width == Screen.currentResolution.width &&
                        resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = options.Count - 1;
                    }
                }
            }
        }

        resolutionDropdown.AddOptions(options); // Adiciona as resoluções no dropdown
        resolutionDropdown.value = currentResolutionIndex; // Define a resolução atual como selecionada
        resolutionDropdown.RefreshShownValue(); // Atualiza o valor exibido
    }

    /// <summary>
    /// Método para verificar se uma cena existe no Build Index
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

        return false; // A cena não foi encontrada
    }
    #endregion
}
