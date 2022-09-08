using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour
{
    [SerializeField]
    private int _rows = 5;

    [SerializeField]
    private int _columns = 5;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    private Cell _cellPrefab = null;

    private Cell[,] _cells;
    // Start is called before the first frame update
    void Start()
    {
        var parent = _gridLayoutGroup.gameObject.transform;

        if (_columns < _rows)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _columns;
        }
        else
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridLayoutGroup.constraintCount = _rows;
        }


        _cells = new Cell[_rows, _columns];

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);

                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
