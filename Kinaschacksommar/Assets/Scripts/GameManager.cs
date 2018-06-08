using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class GameManager : MonoBehaviour
{
    #region Variabler
    int _numberOfPlayers = 6, state;
    GameLogic _gl;
    Board _board;
    Player[] _players;
    Player _currentPlayer;
    Camera _camera;
    Tile _startTile;
    Tile _endTile;
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
        _players = _gl.players;
        _currentPlayer = _players[0];
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if (_currentPlayer.human)
        {
            switch (state)
            {
                case 0:
                    if (Input.GetMouseButtonDown(0))
                    {
                        RaycastHit hit;
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            ObjectTile obj = hit.transform.GetComponent<ObjectTile>();
                            if (_gl.currentState.TileRows[obj.yIndex][obj.xIndex].isOccupied)
                                _startTile = _gl.currentState.TileRows[obj.yIndex][obj.xIndex];
                            else
                                break;
                            //Färga grejer lila
                            Debug.Log(_gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].yPos + " " + _gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].xPos);
                            state++;
                        }
                    }
                    break;
                case 1:
                    if (Input.GetMouseButtonDown(0))
                    {
                        RaycastHit hit;
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            ObjectTile obj = hit.transform.GetComponent<ObjectTile>();
                            _endTile = _gl.currentState.TileRows[obj.yIndex][obj.xIndex];
                            //Färga grejer lila
                            Debug.Log(_gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].yPos + " " + _gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].xPos);
                            state++;
                        }
                    }
                    break;
                case 2:
                    print("Jaharuuu");
                    state = 0;
                    _gl.MoveAMarble(_gl.currentState, _startTile, _endTile);
                    _board.PlaceTheMarbles(_gl.currentState);
                    break;
            }
        }

    }

    #endregion
}
