using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Marble
{
    #region Variabler
    Tile _tile;
    Player _owner;
    string _text ="nope";
    #endregion
    #region Properties
    public Tile tile
    {
        get { return _tile; }
        set { _tile = value; }
    }
    public Player owner
    {
        get { return _owner; }
        set { _owner = value; }
    }
    public string text
    {
        get { return _text; }
        set { _text = value; }
    }
    #endregion
    #region Konstruktor
    public Marble(Marble m, Tile t) //Gör en djup kopia av pjäsen
    {
        _owner = m.owner;
        _tile = t;
        Debug.Log("Om det här inte är null så borde det fungera: " + t.xPos);
    }
    public Marble(Player owner, Tile t) //Skapar en ny pjäs
    {
        _owner = owner;
        _tile = t;
    }
    #endregion

}
