using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Player : MonoBehaviour
{
    #region Variabler
    int _playerIndex, _nestIndex;
    bool _human;
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
    #endregion

    #region Konstruktor
    public Player (int index,int nestIndex, bool isHuman)
    {
        _playerIndex = index;
        _human = isHuman;
        _nestIndex = nestIndex;
    }
    #endregion
}
