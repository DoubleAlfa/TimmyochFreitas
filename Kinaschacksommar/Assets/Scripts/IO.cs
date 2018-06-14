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
    GameObject[] _buttons;
    #endregion
    #region Metoder
    void Start()
    {
        _gm = GetComponent<GameManager>();
    }
    public void SaveGame() //Sparar spelet
    {
        if (_gm.currentPlayer.human && _gm.state == 0)
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
            PlayerPrefs.SetInt("NoPSave", _gm.NumberOfPlayers);
            PlayerPrefs.SetInt("DiffSave", _gm.depth);
        }
    }
    public void LoadGame() //Laddar spelet
    {
        PlayerPrefs.SetInt("LoadedGame", 1);
        PlayerPrefs.SetInt("NoP", PlayerPrefs.GetInt("NoPSave"));
        PlayerPrefs.SetInt("Diff", PlayerPrefs.GetInt("DiffSave"));
        SceneManager.LoadScene(1);
    }
    public void NewGame() //Laddar om scenen
    {
        PlayerPrefs.SetInt("LoadedGame", 0);
        _buttons = new GameObject[] { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject };
        _buttons[1].SetActive(true);
        _buttons[0].SetActive(false);

    }
    public void Restart() //Startar om spelet
    {
        PlayerPrefs.SetInt("NoP", _gm.NumberOfPlayers);
        PlayerPrefs.SetInt("LoadedGame", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit() //Stänger av spelet
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    //Väljer antalet spelare
    public void TwoPlayers()
    {
        PlayerPrefs.SetInt("NoP", 2);
        SceneManager.LoadScene(1);
    }
    public void ThreePlayers()
    {
        PlayerPrefs.SetInt("NoP", 3);
        SceneManager.LoadScene(1);
    }
    public void FourPlayers()
    {
        PlayerPrefs.SetInt("NoP", 4);
        SceneManager.LoadScene(1);
    }
    public void SixPlayers()
    {
        PlayerPrefs.SetInt("NoP", 6);
        SceneManager.LoadScene(1);
    }

    public void Easy()
    {
        PlayerPrefs.SetInt("Diff", 1);
    }
    public void Medium()
    {
        PlayerPrefs.SetInt("Diff", 2);
    }
    public void Hard()
    {
        PlayerPrefs.SetInt("Diff", 3);
    }
    public void Extreme()
    {
        PlayerPrefs.SetInt("Diff", 4);
    }
    #endregion
}
