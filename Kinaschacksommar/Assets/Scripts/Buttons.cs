using System.Collections;
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

  

    //Väljer antalet spelare 
    //public void TwoPlayers()
    //{
    //    PlayerPrefs.SetInt("NoP", 2);
    //    SceneManager.LoadScene(1);
    //}
    //public void ThreePlayers()
    //{
    //    PlayerPrefs.SetInt("NoP", 3);
    //    SceneManager.LoadScene(1);
    //}
    //public void FourPlayers()
    //{
    //    PlayerPrefs.SetInt("NoP", 4);
    //    SceneManager.LoadScene(1);
    //}
    //public void SixPlayers()
    //{
    //    PlayerPrefs.SetInt("NoP", 6);
    //    SceneManager.LoadScene(1);
    //}
    #endregion
}
