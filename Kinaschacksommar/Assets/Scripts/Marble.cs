using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Marble
{
    #region Variabler
    Tile _tile;
    Player _owner;
    #endregion
    #region
    public Tile tile
    {
        get { return _tile; }
    }
    public Player owner
    {
        get { return _owner; }
    }
    #endregion
    #region Konstruktor
    public Marble (Marble m, Tile t)
    {
        _owner = m.owner;
        _tile = t;
        Debug.Log("Om det här inte är null så borde det fungera: " + t.xPos);
    }
    #endregion
}
