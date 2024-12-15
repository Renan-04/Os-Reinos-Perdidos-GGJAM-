using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
using TMPro;

public class GameView : MonoBehaviour
{

    public static GameView Instance { get; private set; }

    public GameObject TelaPause;

    [SerializeField]
    Flowchart fc;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);

        }
    }



    private void Start()
    {
        GameEvents.Instance.OnMenu += Menu;
    }


    private void Menu()  /// usa evento para ativar classe privada
    {
        if(GameController.Instance.isGamePaused)
        TelaPause.SetActive(true); 
        else
         TelaPause.SetActive(false);
    }


    private void OnDestroy()
    {
        GameEvents.Instance.OnMenu -= Menu;
    }

    public void GameOver()
    {
        fc.ExecuteBlock("GameOver");
    }

    public void Victory()
    {
        fc.ExecuteBlock("Victory");
    }
}
