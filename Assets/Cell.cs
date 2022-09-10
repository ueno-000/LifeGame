using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Condition
{
    Alive = 0,
    Ded = -1
}

public class Cell : MonoBehaviour
{
    [SerializeField]
    public Condition _cellCondition = Condition.Ded;

    [SerializeField]
    private Image _image;

    public bool isAlive = false;

    private void OnValidate()
    {
        CellStateChange();
    }

    /// <summary>
    /// cellÇÃê∂éÄèÛë‘Ç…ÇÊÇÈColorïœçX
    /// </summary>
    private void CellStateChange()
    {
        switch (_cellCondition)
        {
            case (Condition)(0):
                _image.color = Color.yellow;
                break;

            case (Condition)(-1):
                _image.color = Color.gray;
                break;
        }
    }

    public void Alive()
    {
        _image.color = Color.yellow;
        _cellCondition = Condition.Alive;
    }

    public void Ded()
    {
        _image.color = Color.gray;
        _cellCondition = Condition.Ded;
    }
}
