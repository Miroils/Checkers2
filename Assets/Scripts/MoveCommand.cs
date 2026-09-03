using UnityEngine;

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
    public void Execute()
    {
        _checker.MoveToNewPosition(_verticalPostionEnd, _horizontalPositionEnd);
    }

    public void Undue()
    {
        _checker.MoveToNewPosition(_verticalPostionStart, _horizontalPositionStart);
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
}
