using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class MoveCommand : ICommand
{
    //previous position?
    //восстанавление срубленных?
    //откат промушена?
    private Checker _checker;
    private int _verticalPostionStart;
    private int _horizontalPositionStart;
    private int _verticalPostionEnd;
    private int _horizontalPositionEnd;
    private List<Checker> _purifedList;
    public void Execute()
    {
        _checker.MoveToNewPosition(_verticalPostionEnd, _horizontalPositionEnd);
    }

    public void Undue()
    {
        _checker.MoveToNewPosition(_verticalPostionStart, _horizontalPositionStart);
        RestoreFromPurifed();
    }

    public MoveCommand(Checker checker, int verticalPostion, int horizontalPosition)//добавить килинг чекер? добавить промотион?
    {
        _checker = checker;
        _verticalPostionStart = verticalPostion;
        _horizontalPositionStart = horizontalPosition;
    }

    public void SetPostionEnd(int verticalPostionNew, int horizontalPostionNew)
    {
        _verticalPostionEnd = verticalPostionNew;
        _horizontalPositionEnd = horizontalPostionNew;
    }

    public void SetPurifiedList(List <Checker> checkersList)
    {        
        _purifedList = checkersList;
    }

    private void RestoreFromPurifed()
    {
        //03 09 возвращаем срубленных
        foreach (Checker checker in _purifedList)
        {
            checker.ReturnChecker();
        }
    }

    private void RestoreNoQueenState()
    {
        //03 09 сбрасывать статус королевы
    }
}
