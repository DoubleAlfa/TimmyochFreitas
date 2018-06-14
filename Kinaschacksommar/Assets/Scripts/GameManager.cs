using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class GameManager : MonoBehaviour
{
    #region Variabler
    int _numberOfPlayers, _state, playerIndex, _depth = 1;
    GameLogic _gl;
    Board _board;
    [SerializeField]
    GameObject _wintext;
    Player[] _players;
    Player _currentPlayer;
    Camera _camera;
    Tile _startTile;
    Tile _endTile;
    Minimax _mm;

    #endregion

    #region Properties
    public int NumberOfPlayers
    {
        get
        {
            return _numberOfPlayers;
        }
    }
    public Player currentPlayer
    {
        get { return _currentPlayer; }
    }
    public GameLogic gl
    {
        get { return _gl; }
    }
    public int depth
    {
        get { return _depth; }
    }
    public int state
    {
        get { return _state; }
    }

    #endregion

    #region Metoder
    private void Awake()
    {
        _numberOfPlayers = PlayerPrefs.GetInt("NoP");
    }
    void Start()
    {
        _gl = GetComponent<GameLogic>();
        _board = GameObject.Find("Board").GetComponent<Board>();

        if (PlayerPrefs.GetInt("LoadedGame") == 0)
            _gl.CreateStartState();
        else
        {
            _gl.CreateSavedState();
        }
        _players = _gl.players;
        _currentPlayer = _players[playerIndex];
        _board.AssignOwners();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _mm = GetComponent<Minimax>();
    }
    void Update()
    {
        if (_currentPlayer.human)
        {
            switch (_state)
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

                            _state++;
                        }
                    }
                    break;
                case 1://Välj en bricka att hoppa till
                    _board.ShowValidMoves(_startTile, true);
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
                                _state++;
                            }

                        }
                    }
                    else if (Input.GetMouseButtonDown(1)) //Avsluta pågående drag
                    {
                        _board.ShowValidMoves(_startTile, false); //Tar bort färgen från hoppbara kulor
                        _state = 0;
                    }
                    break;
                case 2: //Gör själva flytten
                    int kidOfMove = _gl.MoveAMarble(_gl.currentState, _startTile, _endTile);
                    _board.PlaceTheMarbles(_gl.currentState);
                    if (kidOfMove == 0)
                        _state = -1;
                    else if (kidOfMove == 1)
                    {
                        _gl.currentState.VisitTile(_startTile);
                        _state++;
                    }
                    else
                    {
                        _state = 1;
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
                                    _state++;
                                }
                                else
                                {
                                    _endTile = _startTile;
                                }

                            }
                        }
                        if (Input.GetMouseButtonDown(1))
                        {
                            _state = -1;
                            _board.ShowJumpMoves(_startTile, false);
                        }
                    }
                    else
                        _state = -1;
                    break;
                case 4:
                    kidOfMove = _gl.MoveAMarble(_gl.currentState, _startTile, _endTile);
                    _board.PlaceTheMarbles(_gl.currentState);
                    _gl.currentState.VisitTile(_startTile);
                    _state = 3;
                    break;
                case -1:
                    PassTheTurn();
                    _state = 0;
                    _gl.currentState.ClearVisitList();
                    break;
                case -2:
                    _wintext.SetActive(true);
                    if (_currentPlayer.human)
                    {
                        _wintext.transform.GetChild(0).GetComponent<TextMesh>().text = "You Won!\nClick 'restart' to play again!";
                    }
                    else
                    {
                        _wintext.transform.GetChild(0).GetComponent<TextMesh>().text = "Player "+ currentPlayer.playerIndex+1 + " Won!\nClick 'restart' to try again!";
                    }
                    _state--;
                    break;
                case -3:
                    break;
            }
        }
        else
            AITurn();
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
    void AITurn()
    {
        State s = _mm.BubbleDown(_gl.currentState, _currentPlayer, _players[0], _depth, true);
        _gl.currentState = s;
        _board.PlaceTheMarbles(_gl.currentState);
        PassTheTurn();
    }
    void PassTheTurn()
    {
        //print("Player" + _currentPlayer.playerIndex + " has won the game = " + );
        playerIndex++;
        _currentPlayer = _players[playerIndex % _numberOfPlayers];
    }

    #endregion
}
