using System.Collections.Generic;
using UnityEngine;

public class GameEventsHandler : MonoBehaviour
{
    private Cell _currentCell;
    private Cell _chosedCell;
    private Checker _checkerOncell;
    private List<ICommand> _commandBuffer = new List<ICommand>();
    private MoveCommand _moveCommand;

    private int _commandCounter = 0;
    private void Start()
    {
        EventsManager.RightClickEvent.AddListener(HandleRightClick);
        EventsManager.LeftClickOnCellEvent.AddListener(HandleLeftClick);
        EventsManager.NextActionEvent.AddListener(NextActionHandler);
        EventsManager.PreviousActionEvent.AddListener(PreviousActionHandler);
    }

    private void NextActionHandler()
    {
        ExecuteCommand();
    }
    private void PreviousActionHandler()
    {
        UndueCommand();
    }    

    private void HandleRightClick()
    {
        UnchoseAllCells();
        ResetMoveAbleCells();
        ResetPurifyCellsList();
    }

    private void HandleLeftClick(Cell cell)
    {
        _currentCell = cell;        
        UnchoseAllCells();         
        if (cell.Chouseable) //20 08 все черные
        {
            if (_currentCell.GetCheckerOnCell() != null)
            {
                ResetMoveAbleCells();
                ResetPurifyCellsList();
                ChouseNewCell();
            }

            else if (_chosedCell != null)
            {
                TryToAction();
                ResetMoveAbleCells();
                ResetPurifyCellsList();
            }
        }
        else
        {
            ResetMoveAbleCells();
        }
    }

    private void ResetMoveAbleCells()
    {
        EventsManager.ResetCellsMoveableEvent?.Invoke();
    }

    private void ResetPurifyCellsList()
    {
        EventsManager.ResetCellsPurifyEvent?.Invoke();
    }
    void ChouseNewCell()
    {
        _currentCell.ChouseCell();
        _chosedCell = _currentCell;
        _checkerOncell = _chosedCell.GetCheckerOnCell();
        EventsManager.FindMovesEvent?.Invoke(_checkerOncell);     
    }

    void TryToAction()
    {
        if (_currentCell.IsMoveable())
        {
            AddCommand();
            ExecuteCommand();
        }
    }

    private void AddCommand()
    {
        _moveCommand = new MoveCommand(_checkerOncell, _chosedCell.GetVerticalPostion(), _chosedCell.GetHorizontalPostion());
        _moveCommand.SetPostionEnd(_currentCell.GetVerticalPostion(), _currentCell.GetHorizontalPostion());
        _moveCommand.SetPurifiedList(GeneratePuryfiedCheckersList());
        _moveCommand.QueenPromouted = CheckQueenPromoution();
        ClearOldCommandLine();
        _commandBuffer.Add(_moveCommand);        
    }

    private void ClearOldCommandLine()
    {
        if (_commandBuffer.Count > 0)
        {
            _commandBuffer.RemoveRange(_commandCounter, _commandBuffer.Count - _commandCounter);
            _commandCounter = _commandBuffer.Count;
        }
    }

    private bool CheckQueenPromoution()
    {
        bool toQueenPromotion = false;
        if (!_checkerOncell.IsQueen())
        {
            if (_checkerOncell.GetCheckerColor() == CheckerColorEnum.GreenChecker)
            {
                if (_currentCell.GetVerticalPostion() == GlobalGameParametrs.VerticalCells - 1)
                {
                    toQueenPromotion = true;
                }
            }
            else
            {
                if (_currentCell.GetVerticalPostion() == 0)
                {
                    toQueenPromotion = true;
                }
            }
        }
        return toQueenPromotion;
    }

    private List<Checker> GeneratePuryfiedCheckersList()
    {
        List<Checker> checkersForPurify = new List<Checker>();
        List<Cell> cells = _currentCell.GetCellsForPurify();
        foreach (Cell cell in cells)
        {
            checkersForPurify.Add(cell.GetCheckerOnCell());
        }
        return checkersForPurify;
    }

    private void ExecuteCommand()
    {
        if (_commandCounter < _commandBuffer.Count)
        {
            _commandCounter++;
            _commandBuffer[_commandCounter - 1].Execute();
            ClearPreviousChoused();         
        }        
    }

    private void UndueCommand()
    {
        if (_commandCounter > 0)
        {
            _commandCounter--;
            _commandBuffer[_commandCounter].Undue();
            ClearPreviousChoused();
        }
    }

    private void UnchoseAllCells()
    {
        EventsManager.UnchouseAllCellsEvent?.Invoke();
    }

    private void ClearPreviousChoused()
    {
        _chosedCell = null;
    }
}
