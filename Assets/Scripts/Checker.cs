
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
    public Action CheckerReturnedAction;
    public Action CheckerPromoutedAction;
    public Action CheckerDemoutedAction;
    public Checker(CheckerColorEnum checkerColor, int verticalPosition, int horizontalPosition)
    {
        _checkerColor = checkerColor;
        _verticalPosition = verticalPosition;
        _horizontalPosition = horizontalPosition;
    }

    public void PromouteToQueen()
    {
        _isQueen = true;
        CheckerPromoutedAction?.Invoke();
    }

    public void Demoute()
    {
        _isQueen = false;
        CheckerDemoutedAction?.Invoke();
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
    }

    public void DestroyChecker()
    {        
        CheckerDestroyedAction?.Invoke();
    }

    public void ReturnChecker()
    {
        CheckerReturnedAction?.Invoke();
    }
}
