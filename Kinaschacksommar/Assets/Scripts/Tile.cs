using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Tile
{
    #region Variabler
    Marble marble;
    #endregion
    #region Properties
    public bool isOccupied
    {
        get; set;
    }
    public int xPos
    {
        get; set;
    }
    public int yPos
    {
        get; set;
    }
    public Marble Marble
    {
        get { return marble; }
        set { marble = value; marble.tile = this; isOccupied = true; }
    }


    #endregion
    #region Konstruktor
    public Tile(Tile t) //Gör en djup kopia av brickan
    {
        isOccupied = t.isOccupied;
        xPos = t.xPos;
        yPos = t.yPos;
        if (t.isOccupied)
            marble = new Marble(t.marble.owner, this);
        else
            marble = null;
    }
    public Tile(int x, int y, Marble m) //Ifall det finns en pjäs utplacerad på brickan
    {
        isOccupied = true;
        xPos = x;
        yPos = y;
        marble = new Marble(m, this);
    }
    public Tile(int x, int y) //Ifall det inte finns en pjäs utplacerad på brickan
    {
        isOccupied = false;
        xPos = x;
        yPos = y;
        marble = null;
    }
    #endregion
}
