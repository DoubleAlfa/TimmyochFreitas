using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Tile
{
    #region Variabler

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
    public Marble marble
    {
        get; set;
    }


    #endregion
    #region Konstruktor
    public Tile(Tile t)
    {
        isOccupied = t.isOccupied;
        xPos = t.xPos;
        yPos = t.yPos;
        if (t.isOccupied)
            marble = new Marble(t.marble, this);
        else
            marble = null;
    }
    public Tile(int x,int y,Marble m)
    {
        isOccupied = true;
        xPos = x;
        yPos = y;
        marble = new Marble(m,this);
    }
    public Tile(int x,int y)
    {
        isOccupied = true;
        xPos = x;
        yPos = y;
        marble = null;
    }
    #endregion
}
