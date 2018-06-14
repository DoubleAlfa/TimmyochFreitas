using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class ObjectMarble : MonoBehaviour {

    #region Variabler
    [SerializeField]
    int ownerIndex;
    Player _owner;
    #endregion
    #region Properties
    public Player owner
    {
        get { return _owner; }
        set { _owner = value; ownerIndex = _owner.playerIndex; }
    }
    #endregion

}
