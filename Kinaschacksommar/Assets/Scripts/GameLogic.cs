using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public enum Dir { NW, NE, E, SE, SW, W };

public class GameLogic : MonoBehaviour
{
    #region Variabler
    State _currentState;
    GameManager _gm;
    Board _board;
    int[] _tileRows = new int[] { 1, 2, 3, 4, 13, 12, 11, 10, 9, 10, 11, 12, 13, 4, 3, 2, 1 }, nestOrder = new int[] { 0, 5, 1, 4, 2, 3 };
    Player[] players;
    #endregion

    #region Properties
    #endregion

    #region Metoder
    void Awake()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _board = GameObject.Find("Board").GetComponent<Board>();
    }
    public void CreateStartState()
    {
        Tile[][] tempTiles = new Tile[17][];
        Tile[][] tempNests = new Tile[6][];
        Marble[,] tempMarbles;
        players = new Player[_gm.NumberOfPlayers];

        if (_gm.NumberOfPlayers == 2)
        {
            tempMarbles = new Marble[_gm.NumberOfPlayers, 15];
        }
        else
        {
            tempMarbles = new Marble[_gm.NumberOfPlayers, 10];
        }

        for (int i = 0; i < _tileRows.Length; i++)
        {
            tempTiles[i] = new Tile[_tileRows[i]];

            for (int j = 0; j < _tileRows[i]; j++)
            {
                tempTiles[i][j] = new Tile(6 - (_tileRows[i] / 2) + j, i);
            }
        }

        tempNests = new Tile[6][];

        if (_gm.NumberOfPlayers == 2)
        {
            tempNests[0] = new Tile[] { tempTiles[0][0], tempTiles[1][0], tempTiles[1][1], tempTiles[2][0], tempTiles[2][1], tempTiles[2][2], tempTiles[3][0], tempTiles[3][1], tempTiles[3][2], tempTiles[3][3], tempTiles[4][4], tempTiles[4][5], tempTiles[4][6], tempTiles[4][7], tempTiles[4][8] };
            tempNests[5] = new Tile[] { tempTiles[16][0], tempTiles[13][0], tempTiles[13][1], tempTiles[13][2], tempTiles[13][3], tempTiles[14][0], tempTiles[14][1], tempTiles[14][2], tempTiles[15][0], tempTiles[15][1], tempTiles[12][4], tempTiles[12][5], tempTiles[12][6], tempTiles[12][7], tempTiles[12][8] };
        }
        else
        {
            tempNests[0] = new Tile[] { tempTiles[0][0], tempTiles[1][0], tempTiles[1][1], tempTiles[2][0], tempTiles[2][1], tempTiles[2][2], tempTiles[3][0], tempTiles[3][1], tempTiles[3][2], tempTiles[3][3] };
            tempNests[5] = new Tile[] { tempTiles[16][0], tempTiles[13][0], tempTiles[13][1], tempTiles[13][2], tempTiles[13][3], tempTiles[14][0], tempTiles[14][1], tempTiles[14][2], tempTiles[15][0], tempTiles[15][1] };
        }
        tempNests[1] = new Tile[] { tempTiles[4][0], tempTiles[4][1], tempTiles[4][2], tempTiles[4][3], tempTiles[5][0], tempTiles[5][1], tempTiles[5][2], tempTiles[6][0], tempTiles[6][1], tempTiles[7][0] };
        tempNests[2] = new Tile[] { tempTiles[4][12], tempTiles[4][9], tempTiles[4][10], tempTiles[4][11], tempTiles[5][9], tempTiles[5][10], tempTiles[5][11], tempTiles[6][9], tempTiles[6][10], tempTiles[7][9] };
        tempNests[3] = new Tile[] { tempTiles[12][0], tempTiles[9][0], tempTiles[10][0], tempTiles[10][1], tempTiles[11][0], tempTiles[11][1], tempTiles[11][2], tempTiles[12][1], tempTiles[12][2], tempTiles[12][3] };
        tempNests[4] = new Tile[] { tempTiles[12][12], tempTiles[9][9], tempTiles[10][9], tempTiles[10][10], tempTiles[11][9], tempTiles[11][10], tempTiles[11][11], tempTiles[12][9], tempTiles[12][10], tempTiles[12][11] };

        for (int i = 0; i < tempNests.Length; i++)
        {
            for (int j = 0; j < tempNests[i].Length; j++)
            {
                tempNests[i][j].isOccupied = true; //LÄGG TILL STÖD FÖR ATT MAN INTE ÄR SEX SPELARE
            }
        }
        bool isHuman = true;
        for (int i = 0; i < _gm.NumberOfPlayers; i++)
        {
            if (_gm.NumberOfPlayers != 3)
                players[i] = new Player(i, nestOrder[i], isHuman);
            else
                players[i] = new Player(i, i * 2, isHuman);
            isHuman = false;
        }

        for (int i = 0; i < tempNests.Length; i++)
        {
            for (int j = 0; j < tempNests[i].Length; j++)
            {
                tempMarbles[i, j] = new Marble(players[i], tempNests[nestOrder[i]][j]);
                tempNests[nestOrder[i]][j].Marble = tempMarbles[i, j];
            }
        }
        _currentState = new State(tempTiles, tempMarbles, players[0]);
        _board.PlaceTheMarbles(_currentState);
    }
    Tile GetAdjacent(Tile t, Dir direction, State s)
    {
        if (t.yPos % 2 != 0)
        {
            switch (direction)
            {
                case Dir.NW:
                    if (t.yPos <= 0 || t.xPos < s.TileRows[t.yPos - 1][0].xPos || t.xPos > s.TileRows[t.yPos - 1][s.TileRows[t.yPos - 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos - 1][GetXIndex(t.yPos - 1, t.xPos, s)];
                case Dir.NE:
                    if (t.yPos <= 0 || t.xPos + 1 < s.TileRows[t.yPos - 1][0].xPos || t.xPos + 1 > s.TileRows[t.yPos - 1][s.TileRows[t.yPos - 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos - 1][GetXIndex(t.yPos - 1, t.xPos + 1, s)];
                case Dir.E:
                    if (t.xPos >= s.TileRows[t.yPos][s.TileRows[t.yPos].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos][GetXIndex(t.yPos, t.xPos + 1, s)];
                case Dir.SE:
                    if (t.yPos >= s.TileRows.Length - 1 || t.xPos + 1 < s.TileRows[t.yPos + 1][0].xPos || t.xPos + 1 > s.TileRows[t.yPos + 1][s.TileRows[t.yPos + 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos + 1][GetXIndex(t.yPos + 1, t.xPos + 1, s)];
                case Dir.SW:
                    if (t.yPos >= s.TileRows.Length - 1 || t.xPos < s.TileRows[t.yPos + 1][0].xPos || t.xPos > s.TileRows[t.yPos + 1][s.TileRows[t.yPos + 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos + 1][GetXIndex(t.yPos + 1, t.xPos, s)];
                case Dir.W:
                    if (t.xPos >= s.TileRows[t.yPos][s.TileRows[t.yPos].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos][GetXIndex(t.yPos, t.xPos - 1, s)];
                default:
                    Debug.Log("Invalid Direction");
                    return new Tile(-1, -1);
            }
        }
        else
        {
            switch (direction)
            {
                case Dir.NW:
                    if (t.yPos <= 0 || t.xPos - 1 < s.TileRows[t.yPos - 1][0].xPos || t.xPos - 1 > s.TileRows[t.yPos - 1][s.TileRows[t.yPos - 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos - 1][GetXIndex(t.yPos - 1, t.xPos - 1, s)];
                case Dir.NE:
                    if (t.yPos <= 0 || t.xPos < s.TileRows[t.yPos - 1][0].xPos || t.xPos > s.TileRows[t.yPos - 1][s.TileRows[t.yPos - 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos - 1][GetXIndex(t.yPos - 1, t.xPos, s)];
                case Dir.E:
                    if (t.xPos >= s.TileRows[t.yPos][s.TileRows[t.yPos].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos][GetXIndex(t.yPos, t.xPos + 1, s)];
                case Dir.SE:
                    if (t.yPos >= s.TileRows.Length - 1 || t.xPos < s.TileRows[t.yPos + 1][0].xPos || t.xPos > s.TileRows[t.yPos + 1][s.TileRows[t.yPos + 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos + 1][GetXIndex(t.yPos + 1, t.xPos, s)];
                case Dir.SW:
                    if (t.yPos >= s.TileRows.Length - 1 || t.xPos - 1 < s.TileRows[t.yPos + 1][0].xPos || t.xPos - 1 > s.TileRows[t.yPos + 1][s.TileRows[t.yPos + 1].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos + 1][GetXIndex(t.yPos + 1, t.xPos - 1, s)];
                case Dir.W:
                    if (t.xPos >= s.TileRows[t.yPos][s.TileRows[t.yPos].Length - 1].xPos)
                        return new Tile(-1, -1);
                    return s.TileRows[t.yPos][GetXIndex(t.yPos, t.xPos - 1, s)];
                default:
                    Debug.Log("Invalid Direction");
                    return new Tile(-1, -1);
            }
        }
    }
    List<Tile>[] GetValidMoves(Tile t, State s)
    {
        List<Tile>[] adjacentTiles = new List<Tile>[2];
        adjacentTiles[0] = new List<Tile>();
        adjacentTiles[1] = new List<Tile>();
        Dir[] directions = { Dir.NE, Dir.NW, Dir.E, Dir.SE, Dir.SW, Dir.W };
        for (int i = 0; i < directions.Length; i++)
        {
            Tile tempTile = GetAdjacent(t, directions[i], s);
            if (tempTile.xPos != -1)
            {
                if (!tempTile.isOccupied)
                    adjacentTiles[0].Add(tempTile);
                else
                {
                    tempTile = GetAdjacent(tempTile, directions[i], s);
                    if (tempTile.xPos != -1 && !tempTile.isOccupied)
                        adjacentTiles[1].Add(tempTile);
                }
            }

        }
        return adjacentTiles;
    }
    public void MoveAMarble(State s)
    {

    }
    int GetXIndex(int y, int x, State s)
    {
        int startIndex = 6 - (s.TileRows[y].Length / 2);
        return x - startIndex;
    }
    #endregion
}
