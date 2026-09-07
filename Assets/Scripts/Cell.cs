using System;
using System.Collections.Generic;

public class Cell 
{
    private CellColorEnum _cellColor;
    private Checker _checkerOnCell;
    private int _verticalPostion;
    private int _horizontalPostion;
    private bool _moveable;
    public Action <bool> MoveableChangedAction;
    private List<Cell> _cellsForPurify = new List<Cell>();

    public void SetCellColor(CellColorEnum newCellColor)
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
        RemoveCheckerFromCell();
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

    public List<Cell> GetCellsForPurify()
    {
        return _cellsForPurify;
    }
}
