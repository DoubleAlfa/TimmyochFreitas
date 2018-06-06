using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class State
{
    #region Variabler
    Tile[][] _tileRows;
    Marble[,] _playerMarbles;
    Tile[][] _tileNests;
    Player _activePlayer;
    GameManager _gm;
    #endregion

    #region Properties
    public Tile[][] TileRows
    {
        get { return _tileRows; }
    }
    public Marble[,] PlayerMarbles
    {
        get { return _playerMarbles; }
    }
    public Player ActivePlayer
    {
        get { return _activePlayer; }
    }
    #endregion

    #region Metoder
   
    #endregion

    #region Konstruktor
    public State(Tile[][] tiles, Marble[,] marbles, Player activePlayer)
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _tileRows = new Tile[17][];
        for (int i = 0; i < tiles.Length; i++) //Gör en djup kopia av alla tiles
        {
            _tileRows[i] = new Tile[tiles[i].Length];
            for (int j = 0; j < tiles[i].Length; j++)
            {
                _tileRows[i][j] = new Tile(tiles[i][j]);
            }
        }

        _playerMarbles = new Marble[marbles.GetLength(0), marbles.GetLength(1)];
        int c = 0;
        for (int i = 0; i < marbles.GetLength(0); i++) //Gör en djup kopia av alla pjäser
        {
            for (int j = 0; j < marbles.GetLength(1); j++)
            {
                if (marbles[i, j].tile.isOccupied)
                {
                    _playerMarbles[marbles[i, j].owner.playerIndex, c] = marbles[i, j];
                    c++;
                }

            }
            c = 0;
        }
        _activePlayer = activePlayer;
        _tileNests = new Tile[6][];
        if (_gm.NumberOfPlayers == 2)
        {
            _tileNests[0] = new Tile[] { _tileRows[0][0], _tileRows[1][0], _tileRows[1][1], _tileRows[2][0], _tileRows[2][1], _tileRows[2][2], _tileRows[3][0], _tileRows[3][1], _tileRows[3][2], _tileRows[3][3], _tileRows[4][4], _tileRows[4][5], _tileRows[4][6], _tileRows[4][7], _tileRows[4][8] };
            _tileNests[5] = new Tile[] { _tileRows[16][0], _tileRows[13][0], _tileRows[13][1], _tileRows[13][2], _tileRows[13][3], _tileRows[14][0], _tileRows[14][1], _tileRows[14][2], _tileRows[15][0], _tileRows[15][1], _tileRows[12][4], _tileRows[12][5], _tileRows[12][6], _tileRows[12][7], _tileRows[12][8] };
        }
        else
        {
            _tileNests[0] = new Tile[] { _tileRows[0][0], _tileRows[1][0], _tileRows[1][1], _tileRows[2][0], _tileRows[2][1], _tileRows[2][2], _tileRows[3][0], _tileRows[3][1], _tileRows[3][2], _tileRows[3][3] };
            _tileNests[5] = new Tile[] { _tileRows[16][0], _tileRows[13][0], _tileRows[13][1], _tileRows[13][2], _tileRows[13][3], _tileRows[14][0], _tileRows[14][1], _tileRows[14][2], _tileRows[15][0], _tileRows[15][1] };
        }
        _tileNests[1] = new Tile[] { _tileRows[4][0], _tileRows[4][1], _tileRows[4][2], _tileRows[4][3], _tileRows[5][0], _tileRows[5][1], _tileRows[5][2], _tileRows[6][0], _tileRows[6][1], _tileRows[7][0] };
        _tileNests[2] = new Tile[] { _tileRows[4][12], _tileRows[4][9], _tileRows[4][10], _tileRows[4][11], _tileRows[5][9], _tileRows[5][10], _tileRows[5][11], _tileRows[6][9], _tileRows[6][10], _tileRows[7][9] };
        _tileNests[3] = new Tile[] { _tileRows[12][0], _tileRows[9][0], _tileRows[10][0], _tileRows[10][1], _tileRows[11][0], _tileRows[11][1], _tileRows[11][2], _tileRows[12][1], _tileRows[12][2], _tileRows[12][3] };
        _tileNests[4] = new Tile[] { _tileRows[12][12], _tileRows[9][9], _tileRows[10][9], _tileRows[10][10], _tileRows[11][9], _tileRows[11][10], _tileRows[11][11], _tileRows[12][9], _tileRows[12][10], _tileRows[12][11] };

    }
    public State(State s)
    {
        State tempState = new State(s._tileRows, s.PlayerMarbles, s._activePlayer);
        _tileRows = tempState.TileRows;
        _playerMarbles = tempState.PlayerMarbles;
        _activePlayer = tempState.ActivePlayer;
        _tileNests = s._tileNests;
    }
    #endregion
}
