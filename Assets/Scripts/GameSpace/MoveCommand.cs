using System.Collections.Generic;

public class MoveCommand : ICommand
{
    public bool QueenPromouted { get; set; } = false;
    private Checker _checker;
    private int _verticalPostionStart;
    private int _horizontalPositionStart;
    private int _verticalPostionEnd;
    private int _horizontalPositionEnd;
    private List<Checker> _purifedCheckersList;
    public void Execute()
    {
        _checker.MoveToNewPosition(_verticalPostionEnd, _horizontalPositionEnd);
        HandlePurifyCheckersList();
        CheckQueenState();        
    }

    public void Undue()
    {
        _checker.MoveToNewPosition(_verticalPostionStart, _horizontalPositionStart);
        RestoreFromPurifedCheckers();
        CheckNoQueenState();
    }

    public MoveCommand(Checker checker, int verticalPostion, int horizontalPosition)
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
        _purifedCheckersList = checkersList;
    }

    private void RestoreFromPurifedCheckers()
    {
        foreach (Checker checker in _purifedCheckersList)
        {
            checker.ReturnChecker();
            EventsManager.SetCheckerOnCellEvent?.Invoke(checker);
        }
    }

    private void HandlePurifyCheckersList()
    {
        foreach (Checker checker in _purifedCheckersList)
        {
            checker.DestroyChecker();
            EventsManager.ClearingCellEvent?.Invoke(checker.GetVerticalPosition(), checker.GetHorizontalPosition());
        }
    }

    private void CheckNoQueenState()
    {
        if (QueenPromouted)
        {
            _checker.Demoute();
        }
    }

    private void CheckQueenState()
    {
        if (QueenPromouted)
        {
            _checker.PromouteToQueen();
        }
    }
}
