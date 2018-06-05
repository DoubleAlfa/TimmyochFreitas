using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Player : MonoBehaviour
{
    #region Variabler
    int _playerIndex;
    bool _human;
    #endregion
    #region Properties
    public int playerIndex
    {
        get { return _playerIndex; }
    }
    public bool human
    {
        get { return _human; }
    }
    #endregion
}
