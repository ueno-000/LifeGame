using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour, IPointerClickHandler
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

 
    [SerializeField]
    public float _speed = 0.5f;

    [SerializeField]
    public float _maxSpeed = 1f;

    private bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;

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

    private void OnValidate()
    {
        _rows = Mathf.Clamp(_rows, 1, 50);
        _columns = Mathf.Clamp(_columns, 1, 50);
        _speed = Mathf.Clamp(_speed, 0.01f, _maxSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        // 開始  
        if (isStart)
        {
            StartCoroutine(LifeGameCoroutine());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    public void OnStart()
    {
        if (!isStart) { isStart = true; }
        else { isStart = false; }
    }

    /// <summary>
    /// クリック処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;

        // クリックされたオブジェクトが Cell を持つかどうか
        var cell = go.GetComponent<Cell>();

        if (cell != null)
        {
            Debug.Log(cell.isAlive);

            if (!cell.isAlive)
            {
                cell.Alive();
            }
            else
            {
                cell.Ded();
            }
        }
    }

    /// <summary>  
    /// ライフゲームの更新コルーチン  
    /// </summary>  
    /// <returns>The game coroutine.</returns>  
    private IEnumerator LifeGameCoroutine()
    {
        while (true)
        {
            // 全セルを更新  
            for (int x = 0; x < _rows; x++)
            {
                for (int z = 0; z < _columns; z++)
                {
                    Check(_cells, x, z);
                }
            }

            yield return new WaitForSeconds(_speed);
        }
    }

  

    /// <summary>
    /// 判定
    /// </summary>
    /// <param name="_cells"></param>
    /// <param name="r"></param>
    /// <param name="c"></param>
    private void Check(Cell[,] _cells, int r, int c)
    {
        int _livingCellNum = 0;

        if (r != 0 && c != 0)
            Debug.Log(_cells[r - 1, c - 1]._cellCondition);

        //左上
        if (r != 0 && c != 0 && _cells[r - 1, c - 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //上
        if (r != 0 && _cells[r - 1, c]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //右上
        if (r != 0 && c != _columns - 1 && _cells[r - 1, c + 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //左
        if (c != 0 && _cells[r, c - 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //右
        if (c != _columns - 1 && _cells[r, c + 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //左下
        if (c != 0 && r != _rows - 1 && _cells[r + 1, c - 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //下
        if (r != _rows - 1 && _cells[r + 1, c]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //右下
        if (c != _columns - 1 && r != _rows - 1 && _cells[r + 1, c + 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }


        if (_livingCellNum >= 4)//周囲の生存cellが４つ以上なら死滅
        {
            _cells[r, c].Ded();
        }
        else if (_livingCellNum <= 1)//周囲の生存cellが２未満なら死滅
        {
            _cells[r, c].Ded();
        }



        if (_cells[r, c]._cellCondition != Condition.Ded && _livingCellNum == 3)//自分がdedCellの場合周囲の生存cellが３なら生
        {
            _cells[r, c].Alive();
        }
    }
}

