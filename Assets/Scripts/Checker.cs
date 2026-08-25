
using System;
//using System.Diagnostics;
using UnityEngine;

public class Checker
{ 
    private CheckerColorEnum _checkerColor;
    private bool _isQueen;
    private int _verticalPosition;//18 08 спорно?
    private int _horizontalPosition;//18 08 спорно?

    public Action CheckerDestroyedAction;
    public Checker(CheckerColorEnum checkerColor, int verticalPosition, int horizontalPosition)
    {
        _checkerColor = checkerColor;
        _verticalPosition = verticalPosition;
        _horizontalPosition = horizontalPosition;
    }

    private void PromouteToQueen()
    {
        _isQueen = true;
    }

    public bool IsQueen()
    {
        return _isQueen;
    }

    public CheckerColorEnum GetCheckerColor()
    {
        return _checkerColor;
    }

    public int GetVerticalPosition()
    {
        return _verticalPosition;
    }

    public int GetHorizontalPosition()
    {
        return _horizontalPosition;
    }

    public void  MoveToNewPosition(int newVerticalPostion, int newHorizontalPostion)
    {
        EventsManager.ClearingCellEvent?.Invoke(_verticalPosition, _horizontalPosition);
        _verticalPosition = newVerticalPostion;
        _horizontalPosition = newHorizontalPostion;
        EventsManager.TransitCheckerEvent?.Invoke(this);
        //20 08 занятие новой клетки
        //21 08 нужно обоновить параметры Cell
        //21 08 нужно визуализировать перемещение
        CheackForQueenPromotion();
    }

    private void CheackForQueenPromotion()
    {
        if (_checkerColor == CheckerColorEnum.greenChecker)
        {
            if (_verticalPosition == GlobalGameParametrs.VerticalCells - 1)
            {
                PromouteToQueen();
            }
        }
        else
        {
            if (_verticalPosition == 0)
            {
                PromouteToQueen();
            }
        }
    }
    public void DestroyChecker()
    {        
        CheckerDestroyedAction?.Invoke();
    }
}
