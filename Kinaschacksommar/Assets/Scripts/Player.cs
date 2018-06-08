using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Player
{
    #region Variabler
    int _playerIndex, _nestIndex;
    bool _human;
    Tile[] _goalNest;
    #endregion
    #region Properties
    public int playerIndex
    {
        get { return _playerIndex; }
    }
    public int nestIndex
    {
        get { return _nestIndex; }
    }
    public bool human
    {
        get { return _human; }
    }
    public Tile[] goalNest
    {
        get { return _goalNest; }
        set { _goalNest = value; }
    }
    #endregion

    #region Konstruktor
    public Player (int index,int nestIndex,Tile[] goalNest, bool isHuman)
    {
        _playerIndex = index;
        _human = isHuman;
        _goalNest = goalNest;
        _nestIndex = nestIndex;
    }
    #endregion
}
