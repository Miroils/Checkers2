using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Cell 
{
    //03 08 позиция?
    //03 08 или то, что она на краю?
    private CellColorEnum _cellColor;
    private Checker _checkerOnCell;

    private int _verticalPostion;
    private int _horizontalPostion;

    private bool _moveable;
    public Action <bool> MoveableChangedAction;

    private List<Cell> _cellsForPurify = new List<Cell>();//23 08 ячейки который нужно очистить при попадании в данную ячейку
    public void SetCellColor(CellColorEnum newCellColor) //18 08 в конструктор закинуть?
    {
        _cellColor = newCellColor;
    }

    public CellColorEnum GetCellColor()
    {
        return _cellColor;
    }

    public void SetCheckerOnCell(Checker checker)
    {
        _checkerOnCell = checker;
    }

    public void RemoveCheckerFromCell()
    {
        _checkerOnCell = null;
    }

    public Checker GetCheckerOnCell()
    {
        return _checkerOnCell;
    }

    public void SetCellPostion(int verticalPostion, int horizontalPostion)
    {
        _verticalPostion = verticalPostion;
        _horizontalPostion = horizontalPostion;
    }

    public int GetVerticalPostion()
    {
        return _verticalPostion;
    }

    public int GetHorizontalPostion()
    {
        return _horizontalPostion;
    }

    public void SetMoveable()
    {
        _moveable = true;
        MoveableChangedAction?.Invoke(_moveable);
    }

    public void ClearPurifyList()
    {
        _cellsForPurify.Clear();
    }

    public void AddCellToPurifyList(Cell cell)
    {
        _cellsForPurify.Add(cell);
    }

    public void PurifyCell()
    {
        EventsManager.DestroyCheckerEvent?.Invoke(_checkerOnCell);
        RemoveCheckerFromCell();
        //23 08 очистка данных о чекер
        //23 08 поиск и удаления чекера, евентом?
    }

    public void PurifingFromList()
    {
        foreach (var cell in _cellsForPurify)
        {
            EventsManager.CellPurifyEvent?.Invoke(cell);
        }
    }

    public void ResetMoveable()
    {
        _moveable = false;
        MoveableChangedAction?.Invoke(_moveable);
    }

    public bool IsMoveable()
    {

        return _moveable;
    }

}
