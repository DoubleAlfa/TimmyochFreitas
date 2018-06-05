using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andreas de Freitas && Timmy Alvelöv
public class Board : MonoBehaviour
{
    #region Variabler
    [SerializeField]
    GameObject _marble, _tile;
    [SerializeField]
    float _offset;
    int[] _tileRows = new int[] { 1, 2, 3, 4, 13, 12, 11, 10, 9, 10, 11, 12, 13, 4, 3, 2, 1 };
    int _numberOfRows;
    GameObject[][] _tiles, _marbles, _nests;
    GameManager _gm;
    [SerializeField]
    Color[] colors = new Color[6];
    #endregion

    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _numberOfRows = _tileRows.Length;
        _tiles = new GameObject[_numberOfRows][];
        for (int i = 0; i < _numberOfRows; i++) //Spawnar ut brädet
        {
            _tiles[i] = new GameObject[_tileRows[i]];
            for (int j = 0; j < _tileRows[i]; j++)
            {
                _tiles[i][j] = Instantiate(_tile);
                _tiles[i][j].transform.position = transform.position - new Vector3(_offset * ((float)_tileRows[i] / 2) - _offset * j, _offset * i, 0);
            }
        }

        SetNests();
    }

    void SetNests()
    {
        _nests = new GameObject[6][];

        if (_gm.NumberOfPlayers == 2)
        {
            _nests[0] = new GameObject[] { _tiles[0][0], _tiles[1][0], _tiles[1][1], _tiles[2][0], _tiles[2][1], _tiles[2][2], _tiles[3][0], _tiles[3][1], _tiles[3][2], _tiles[3][3], _tiles[4][4], _tiles[4][5], _tiles[4][6], _tiles[4][7], _tiles[4][8] };
            _nests[5] = new GameObject[] { _tiles[16][0], _tiles[13][0], _tiles[13][1], _tiles[13][2], _tiles[13][3], _tiles[14][0], _tiles[14][1], _tiles[14][2], _tiles[15][0], _tiles[15][1], _tiles[12][4], _tiles[12][5], _tiles[12][6], _tiles[12][7], _tiles[12][8] };
        }
        else
        {
            _nests[0] = new GameObject[] { _tiles[0][0], _tiles[1][0], _tiles[1][1], _tiles[2][0], _tiles[2][1], _tiles[2][2], _tiles[3][0], _tiles[3][1], _tiles[3][2], _tiles[3][3] };
            _nests[5] = new GameObject[] { _tiles[16][0], _tiles[13][0], _tiles[13][1], _tiles[13][2], _tiles[13][3], _tiles[14][0], _tiles[14][1], _tiles[14][2], _tiles[15][0], _tiles[15][1] };
        }
        _nests[1] = new GameObject[] { _tiles[4][0], _tiles[4][1], _tiles[4][2], _tiles[4][3], _tiles[5][0], _tiles[5][1], _tiles[5][2], _tiles[6][0], _tiles[6][1], _tiles[7][0] };
        _nests[2] = new GameObject[] { _tiles[4][12], _tiles[4][9], _tiles[4][10], _tiles[4][11], _tiles[5][9], _tiles[5][10], _tiles[5][11], _tiles[6][9], _tiles[6][10], _tiles[7][9] };
        _nests[3] = new GameObject[] { _tiles[12][0], _tiles[9][0], _tiles[10][0], _tiles[10][1], _tiles[11][0], _tiles[11][1], _tiles[11][2], _tiles[12][1], _tiles[12][2], _tiles[12][3] };
        _nests[4] = new GameObject[] { _tiles[12][12], _tiles[9][9], _tiles[10][9], _tiles[10][10], _tiles[11][9], _tiles[11][10], _tiles[11][11], _tiles[12][9], _tiles[12][10], _tiles[12][11] };

        for (int i = 0; i < _nests.Length; i++)
        {
            for (int j = 0; j < _nests[i].Length; j++)
            {
                _nests[i][j].GetComponent<Renderer>().material.color = colors[i];
            }
        }
    }
}
