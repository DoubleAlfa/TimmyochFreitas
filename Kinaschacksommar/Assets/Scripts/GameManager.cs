using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class GameManager : MonoBehaviour
{
    #region Variabler
    int _numberOfPlayers = 2, state, playerIndex;
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
    public GameLogic gl
    {
        get { return _gl; }
    }

    #endregion

    #region Metoder
    void Start()
    {
        _gl = GetComponent<GameLogic>();
        _board = GameObject.Find("Board").GetComponent<Board>();
        _gl.CreateStartState();
        _players = _gl.players;
        _currentPlayer = _players[playerIndex];
        _board.AssignOwners();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if (/*_currentPlayer.human*/ true)
        {
            switch (state)
            {
                case 0: //Välj en bricka att hoppa från
                    if (Input.GetMouseButtonDown(0))
                    {
                        RaycastHit hit;
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            ObjectTile obj = hit.transform.GetComponent<ObjectTile>();
                            if (_gl.currentState.TileRows[obj.yIndex][obj.xIndex].isOccupied)
                            {
                                _startTile = _gl.currentState.TileRows[obj.yIndex][obj.xIndex];
                                if (_board.GetTileCoords(_startTile.xPos, _startTile.yPos).GetComponent<ObjectTile>().marbleObject.owner != _currentPlayer) //Kollar om rätt person äger kulan
                                {
                                    _startTile = null;
                                    break;
                                }
                            }

                            else
                                break;
                            //Färga grejer lila
                            _board.ShowValidMoves(_startTile, true);
                            Debug.Log(_gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].yPos + " " + _gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].xPos);
                            state++;
                        }
                    }
                    break;
                case 1://Välj en bricka att hoppa till
                    if (Input.GetMouseButtonDown(0))
                    {
                        RaycastHit hit;
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            ObjectTile obj = hit.transform.GetComponent<ObjectTile>();
                            _endTile = _gl.currentState.TileRows[obj.yIndex][obj.xIndex];
                            if (_endTile != _startTile)
                            {
                                _board.ShowValidMoves(_startTile, false); //Tar bort färgen från hoppbara kulor
                                Debug.Log(_gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].yPos + " " + _gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].xPos);
                                state++;
                            }

                        }
                    }
                    else if (Input.GetMouseButtonDown(1)) //Avsluta pågående drag
                    {
                        _board.ShowValidMoves(_startTile, false); //Tar bort färgen från hoppbara kulor
                        state = 0;
                    }
                    break;
                case 2: //Gör själva flytten
                    bool normalMove = _gl.MoveAMarble(_gl.currentState, _startTile, _endTile);
                    _board.PlaceTheMarbles(_gl.currentState);
                    if (normalMove)
                        state = -1;
                    else
                    {
                        _gl.currentState.VisitTile(_startTile);
                        state++;
                    }
                    break;
                case 3:
                    _board.ShowJumpMoves(_startTile, false);
                    _startTile = _endTile;
                    if (_gl.GetJumpMoves(_startTile, _gl.currentState).Count != 0)
                    {
                        _board.ShowJumpMoves(_startTile, true);
                        List<Tile> moves = _gl.GetJumpMoves(_startTile, _gl.currentState);
                        if (Input.GetMouseButtonDown(0))
                        {
                            RaycastHit hit;
                            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                            {
                                ObjectTile obj = hit.transform.GetComponent<ObjectTile>();
                                _endTile = _gl.currentState.TileRows[obj.yIndex][obj.xIndex];
                                if (_endTile != _startTile && ExistsInList(_endTile, moves))
                                {
                                    _board.ShowJumpMoves(_startTile, false); //Tar bort färgen från hoppbara kulor
                                    Debug.Log(_gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].yPos + " " + _gl.currentState.TileRows[hit.transform.gameObject.GetComponent<ObjectTile>().yIndex][hit.transform.gameObject.GetComponent<ObjectTile>().xIndex].xPos);
                                    state++;
                                }
                                else
                                {
                                    _endTile = _startTile;
                                }

                            }
                        }
                        if (Input.GetMouseButtonDown(1))
                        {
                            state = -1;
                            _board.ShowJumpMoves(_startTile, false);
                        }
                    }
                    else
                        state = -1;
                    break;
                case 4:
                     normalMove = _gl.MoveAMarble(_gl.currentState, _startTile, _endTile);
                    _board.PlaceTheMarbles(_gl.currentState);
                    _gl.currentState.VisitTile(_startTile);
                    state = 3;
                    break;
                case -1:
                    PassTheTurn();
                    state = 0;
                    _gl.currentState.ClearVisitList();
                    break;
            }
        }

    }
    bool ExistsInList(Tile t, List<Tile> tiles)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (t == tiles[i])
                return true;
        }
        return false;
    }
    void PassTheTurn()
    {
        _gl.currentState.Value(_currentPlayer);
        
        //print("Player" + _currentPlayer.playerIndex + " has won the game = " + _gl.WinCheck(_gl.currentState, _currentPlayer));
        playerIndex++;
        _currentPlayer = _players[playerIndex % _numberOfPlayers];

    }

    #endregion
}
