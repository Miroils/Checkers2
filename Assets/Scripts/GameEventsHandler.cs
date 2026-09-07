using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class GameEventsHandler : MonoBehaviour
{
    //private Cell2D _currentCell2d;//20 08 от 2д Уйти?
    //private Cell2D _chousedCell2d;//20 08 выбранная ранее
    private Cell _currentCell;//20 08 от 2д Уйти?
    private Cell _chosedCell;//20 08 от 2д Уйти?
    private Checker _checkerOncell;
    private Board _board;
    private List<ICommand> _commandBuffer = new List<ICommand>();
    private MoveCommand _moveCommand;

    private int _commandCounter = 0;
    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }
    private void Start()
    {
        EventsManager.RightClickEvent.AddListener(HandleRightClick);
        EventsManager.LeftClickOnCellEvent.AddListener(HandleLeftClick);
        EventsManager.NextActionEvent.AddListener(NextActionHandler);
        EventsManager.PreviousActionEvent.AddListener(PreviousActionHandler);
    }

    private void NextActionHandler()
    {
        ExecuteCommand();//03 09 сразу этот метод привязать к евенту?, пока нет
    }
    private void PreviousActionHandler()
    {
        UndueCommand();//03 09 сразу этот метод привязать к евенту?, пока нет
    }    

    void HandleRightClick()
    {
        UnchoseAllCells();
        ResetMoveAbleCells();
        ResetPurifyCellsList();
    }

    void HandleLeftClick(Cell cell)
    {
        _currentCell = cell;
        //20 08 принимать не 2д, с просто селл?            
        UnchoseAllCells();
        //20 08 еще нужны условия, чей сейчас ход            
        if (cell.Chouseable) //20 08 все черные
        {
            if (_currentCell.GetCheckerOnCell() != null)//не пустая
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
            AddCommandeAndExecute();//03 09 ренейминг? на норм название?
        }
    }

    private void AddCommandeAndExecute()//30 08 нужно название связать с комманд
    {
        //30 09 нужно делать в другом методе
        _moveCommand = new MoveCommand(_checkerOncell, _chosedCell.GetVerticalPostion(), _chosedCell.GetHorizontalPostion());
        _moveCommand.SetPostionEnd(_currentCell.GetVerticalPostion(), _currentCell.GetHorizontalPostion());
        _moveCommand.SetPurifiedList(GeneratePuryfiedCheckersList());
        _moveCommand.QueenPromouted = CheckQueenPromoution();
        ClearOldCommandLine();
        _commandBuffer.Add(_moveCommand);
        ExecuteCommand();
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
            ClearPreviousChoused();//03 09 вынести из команды?            
        }        
    }

    private void UndueCommand()
    {
        if (_commandCounter > 0)//03 09 уже и так в начало вернулись
        {
            _commandCounter--;
            _commandBuffer[_commandCounter].Undue();
            ClearPreviousChoused();//03 09 вынести из команды?
        }
    }


    void UnchoseAllCells()
    {
        EventsManager.UnchouseAllCellsEvent?.Invoke();
    }

    void ClearPreviousChoused()
    {
        _chosedCell = null;
    }
}
