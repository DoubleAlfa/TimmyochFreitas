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

    #region TA BORT INNAN INLÄMNING
    /*
    -Lands-
    Hissing quagmire
    Smoldering March
    Cinder Glade
    Gruul guildgate
    Naya Panorama
    Jund Panorama
    Highland Weald
    Rugged Highlands


    -Non-dragons
    Sakura tribe elder
    Dragonlord's servant
    Dragonspeaker Shaman

    -Non-creatures-
    Sarkhan vol
    Crucible of Fire (m15 foil)
    Sarkhan's Triumph
    Atarka Monument
    Draconic roar

    -Dragons-
    Shivan Dragon
    Hoarding dragon
    Atarka, World render
    Steel hellkite
    Rirux Bladewing
    Freejam Regent
    Flameblast dragon
    Siege  dragon
    Skyship stalker
    Spawn oh Thraxes
    Mordant Dragon
    Moltensteel Dragon
    Mindscour Dragon
    Shockmaw dragon
    Destructor dragon
    Dragon hatchling
    Dragon Egg
    Volcanic dragon
    Woodland Changeling
    Furnance Whelp
    Herdchaser Dragon
    Stormwing Dragon
    Savage Ventmaw
    Dragonlord Atarka
    Dragonlord Kolaghan

    */
    #endregion
}
