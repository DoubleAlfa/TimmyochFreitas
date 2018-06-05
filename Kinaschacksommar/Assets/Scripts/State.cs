using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class State
{
    #region Variabler
    Tile[][] _tileRows;
    Marble[,] _playerMarbles;
    Tile[,] _tileNests;
    Player _activePlayer;
    #endregion
    

    #region Konstruktor
    public State(Tile[][] tiles, Marble[,] marbles, Player activePlayer)
    {

        for (int i = 0; i < tiles.Length; i++) //Gör en djup kopia av alla tiles
        {
            _tileRows[i] = new Tile[tiles[i].Length];
            for (int j = 0; j < tiles[i].Length; j++)
            {
                _tileRows[i][j] = new Tile(tiles[i][j]);
            }
        }

        _playerMarbles = new Marble[marbles.GetLength(0), marbles.GetLength(1)];
        int c =0;
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
        }
        _activePlayer = activePlayer;

    }
    #endregion
}
