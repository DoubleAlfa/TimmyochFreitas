﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Av Andreas de Freitas && Timmy Alvelöv
public class Buttons : MonoBehaviour
{

    #region Variabler

    IO _io;
    GameManager _gm;
    #endregion

    #region Metoder

    void Start()
    {
        _gm = GetComponent<GameManager>();
        _io = new IO();
    }

    public void Load() 
    {
        _io.LoadGame();
    }

    public void Save() 
    {
        _io.SaveGame();
    }
    #endregion
}
