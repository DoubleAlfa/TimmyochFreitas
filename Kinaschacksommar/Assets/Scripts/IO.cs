using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Av Andreas de Freitas && Timmy Alvelöv
public class IO : MonoBehaviour
{
    #region Variabler
    GameManager _gm;
    State _currentState;
    string _save = "";
    #endregion
    #region Metoder
    void Start()
    {
        _gm = GetComponent<GameManager>();
    }
    public void SaveGame() //Sparar spelet
    {
        if (_gm.currentPlayer.human)
        {
            _currentState = _gm.gl.currentState;
            for (int i = 0; i < _currentState.TileRows.Length; i++)
            {
                for (int j = 0; j < _currentState.TileRows[i].Length; j++)
                {
                    if (_currentState.TileRows[i][j].isOccupied)
                        _save += _currentState.TileRows[i][j].Marble.owner.playerIndex;
                    else
                        _save += "x";
                }
            }
            PlayerPrefs.SetString("SavedGame", _save);
        }
    }
    public void LoadGame() //Laddar spelet
    {

    }
    public void NewGame() //Laddar om scenen
    {
        SceneManager.LoadScene(0);
    }
    public void Quit() //Stänger av spelet
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    #endregion
}
