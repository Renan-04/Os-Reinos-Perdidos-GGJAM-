using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-1)]
public class GameEvents : MonoBehaviour
{
     public static GameEvents Instance { get; private set; }

 
    public event Action OnMenu;


    void Awake()
    {
       
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }



    public void Menu()
    {
        OnMenu?.Invoke();

    }


}
