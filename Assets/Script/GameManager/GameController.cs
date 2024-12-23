using Fungus;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public bool isGamePaused = false;

    public bool Move = false; // variavel de acesso por qualquer script para verificar se player está em movimento




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

        GameEvents.Instance.OnMenu += Pause;
    
    }

    [System.Obsolete]

    private struct Variables
    {

        public const string Part = "Part";
      
    }

  

    public void SalvarBattle(int qtd, int have,int al)
    {

         //   PlayerPrefs.SetInt("Inim", qtd);


   
             //PlayerPrefs.Save();
    }




    private void Pause()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
        
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }


    private void OnDestroy()
    {
        GameEvents.Instance.OnMenu -= Pause;
    }

    public void Exit()
    {
        Application.Quit();
    }





}

