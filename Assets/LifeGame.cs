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
        // �J�n  
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
    /// �N���b�N����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;

        // �N���b�N���ꂽ�I�u�W�F�N�g�� Cell �������ǂ���
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
    /// ���C�t�Q�[���̍X�V�R���[�`��  
    /// </summary>  
    /// <returns>The game coroutine.</returns>  
    private IEnumerator LifeGameCoroutine()
    {
        while (true)
        {
            // �S�Z�����X�V  
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
    /// ����
    /// </summary>
    /// <param name="_cells"></param>
    /// <param name="r"></param>
    /// <param name="c"></param>
    private void Check(Cell[,] _cells, int r, int c)
    {
        int _livingCellNum = 0;

        if (r != 0 && c != 0)
            Debug.Log(_cells[r - 1, c - 1]._cellCondition);

        //����
        if (r != 0 && c != 0 && _cells[r - 1, c - 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //��
        if (r != 0 && _cells[r - 1, c]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //�E��
        if (r != 0 && c != _columns - 1 && _cells[r - 1, c + 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //��
        if (c != 0 && _cells[r, c - 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //�E
        if (c != _columns - 1 && _cells[r, c + 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //����
        if (c != 0 && r != _rows - 1 && _cells[r + 1, c - 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //��
        if (r != _rows - 1 && _cells[r + 1, c]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }
        //�E��
        if (c != _columns - 1 && r != _rows - 1 && _cells[r + 1, c + 1]._cellCondition == Condition.Alive)
        {
            _livingCellNum++;
        }


        if (_livingCellNum >= 4)//���͂̐���cell���S�ȏ�Ȃ玀��
        {
            _cells[r, c].Ded();
        }
        else if (_livingCellNum <= 1)//���͂̐���cell���Q�����Ȃ玀��
        {
            _cells[r, c].Ded();
        }



        if (_cells[r, c]._cellCondition != Condition.Ded && _livingCellNum == 3)//������dedCell�̏ꍇ���͂̐���cell���R�Ȃ琶
        {
            _cells[r, c].Alive();
        }
    }
}

