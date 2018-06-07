using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class GameManager : MonoBehaviour
{
    #region Variabler
    int _numberOfPlayers = 4;
    GameLogic _gl;
    Board _board;
    #endregion

    #region Properties
    public int NumberOfPlayers
    {
        get
        {
            return _numberOfPlayers;
        }
    }
    
    #endregion

    #region Metoder
    void Start()
    {
        _gl = GetComponent<GameLogic>();
        _board = GameObject.Find("Board").GetComponent<Board>();
        _gl.CreateStartState();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            _gl.TestMove();
            _board.PlaceTheMarbles(_gl.currentState);
        }
    }
    #endregion
}
