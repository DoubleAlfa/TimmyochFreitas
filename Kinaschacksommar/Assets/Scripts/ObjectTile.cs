using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class ObjectTile : MonoBehaviour
{
    #region Variabler
    Color _startColor;
    ObjectMarble _marbleObject;
    #endregion
    #region Properties
    public int xIndex
    {
        get;set;
    }
    public int yIndex
    {
        get;set;
    }
    public ObjectMarble marbleObject
    {
        get { return _marbleObject; }
        set { _marbleObject = value; }
    }
    #endregion
    #region Metoder
    void Start()
    {
        _startColor = GetComponent<Renderer>().material.color;
    }

    public void ShowColor()
    {
        GetComponent<Renderer>().material.color = Color.magenta;
    }
    public void ResetColor()
    {
        GetComponent<Renderer>().material.color = _startColor;
    }

    #endregion
}
