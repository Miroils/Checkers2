using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class GameEventsController : MonoBehaviour
{
    //22 08 нужно связать с BoardData или Board, через Zenject?

    private Cell2D _currentCell2d;//20 08 от 2д Уйти?
    private Cell2D _chousedCell2d;//20 08 выбранная ранее
    private Cell _currentCell;//20 08 от 2д Уйти?
    private Cell _chosedCell;//20 08 от 2д Уйти?
    private Checker _checkerOncell;
    //20 08 спорно название, предполагается ренейм и может разделение на сабклассы
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Board _board;
    //30 08 попытка воткнуть интерфейсы
    private List<ICommand> _commandBuffer = new List<ICommand>();
    private MoveCommand _moveCommand;

    private int _commandCounter = 0;
    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }
    void Start()
    {
        EventsManager.RightClickEvent.AddListener(HandleRightClick);
        EventsManager.LeftClickOnCellEvent.AddListener(HandleLeftClick);
        EventsManager.NextActionEvent.AddListener(NextActionHandler);
        EventsManager.PreviousActionEvent.AddListener(PreviousActionHandler);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        UnchouseAllCells();
        ResetMoveAbleCells();
        ResetPurifyCellsList();
    }

    void HandleLeftClick(Cell2D cell2d)
    {
        _currentCell2d = cell2d;
        _currentCell = _currentCell2d.GetCellData();
        //20 08 принимать не 2д, с просто селл?            
        UnchouseAllCells();
        //20 08 еще нужны условия, чей сейчас ход            
        if (_currentCell2d.IsChouseable()) //20 08 все черные
        {
            if (_currentCell.GetCheckerOnCell() != null)//не пустая
            {
                ResetMoveAbleCells();
                ResetPurifyCellsList();
                ChouseNewCell();
            }

            else if (_chousedCell2d != null)
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
        _currentCell2d.ChouseCell();
        _chousedCell2d = _currentCell2d;
        _chosedCell = _chousedCell2d.GetCellData();
        _checkerOncell = _chosedCell.GetCheckerOnCell();
        FindMoveableCells(_checkerOncell);
    }

    private void FindMoveableCells(Checker checker)
    {
        int verticalPostion = checker.GetVerticalPosition();
        int horizontalPostion = checker.GetHorizontalPosition();
        //24 08 перемещение
        if (checker.IsQueen())
        {
            FindAttackMoveForQueen(checker, -1, -1);//лево низ
            FindAttackMoveForQueen(checker, -1, 1);//право низ
            FindAttackMoveForQueen(checker, 1, -1);//лево верх
            FindAttackMoveForQueen(checker, 1, 1);//право верх
        }
        else //25 08 тоже свернуть?
        {
            if (checker.GetCheckerColor() == CheckerColorEnum.GreenChecker)
            {
                int cheсkingVerticalPosion = verticalPostion + 1;
                int cheсkingHorizontalPosion = 0;
                if (cheсkingVerticalPosion < GlobalGameParametrs.VerticalCells)
                {
                    cheсkingHorizontalPosion = horizontalPostion - 1;
                    if (cheсkingHorizontalPosion >= 0)
                    {
                        TryToSetCellMoveable(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                    }

                    cheсkingHorizontalPosion = horizontalPostion + 1;
                    if (cheсkingHorizontalPosion < GlobalGameParametrs.HorizontalCells)
                    {
                        TryToSetCellMoveable(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                    }
                }
            }

            else//22 08 red
            {
                int cheсkingVerticalPosion = verticalPostion - 1;
                if (cheсkingVerticalPosion >= 0)
                {
                    int cheсkingHorizontalPosion = horizontalPostion - 1;
                    if (cheсkingHorizontalPosion >= 0)
                    {
                        TryToSetCellMoveable(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                    }

                    cheсkingHorizontalPosion = horizontalPostion + 1;
                    if (cheсkingHorizontalPosion < GlobalGameParametrs.HorizontalCells)
                    {
                        TryToSetCellMoveable(cheсkingVerticalPosion,cheсkingHorizontalPosion);
                    }
                }
            }
        }

        //24 08 перемещение + сруб
        if (!checker.IsQueen())
        {
            if (verticalPostion >= 2 && horizontalPostion >= 2)//лево низ
            {
                FindAttackMoveForChecker(checker, -1, -1);
            }

            if (verticalPostion >= 2 && horizontalPostion < GlobalGameParametrs.HorizontalCells - 2)//право низ
            {
                FindAttackMoveForChecker(checker, -1, 1);
            }

            if (verticalPostion < GlobalGameParametrs.VerticalCells - 2 && horizontalPostion >= 2)//лево верх
            {
                FindAttackMoveForChecker(checker, 1, -1);
            }

            if (verticalPostion < GlobalGameParametrs.VerticalCells - 2 && horizontalPostion < GlobalGameParametrs.HorizontalCells - 2)//право верх
            {
                FindAttackMoveForChecker(checker, 1, 1);
            }
        }
    }

    private void TryToSetCellMoveable(int cheсkingVerticalPosion, int cheсkingHorizontalPosion)
    {
        Cell cell = _board.GetCellData(cheсkingVerticalPosion, cheсkingHorizontalPosion);
        if (CheckerOnConreteCell(cell) == null)
        {
            cell.SetMoveable();
        }
    }

    private void FindAttackMoveForQueen(Checker checker, int deltaVerticalPostion, int deltaHorizontalPosition)
    {
        int verticalPostion = checker.GetVerticalPosition();//23 08 до этого тоже определяли, перед методом, нужно оптимизировать
        int horizontalPostion = checker.GetHorizontalPosition();
        List<Cell> cellsForPurify = new List<Cell>();
        CheckerColorEnum enemyColor;
        bool enemyOnLine = false;
        if (checker.GetCheckerColor() == CheckerColorEnum.GreenChecker)
        {
            enemyColor = CheckerColorEnum.RedChecker;
        }
        else
        {
            enemyColor = CheckerColorEnum.GreenChecker;
        }

        for (int i = 1; i < GlobalGameParametrs.HorizontalCells; i++)//25 08 можно и Verticals брать
        {
            int cheсkingVerticalPosition = verticalPostion + deltaVerticalPostion * i;
            int cheсkingHorizontalPosition = horizontalPostion + deltaHorizontalPosition * i;
            
            if (cheсkingVerticalPosition >= 0 && cheсkingHorizontalPosition >= 0 && cheсkingVerticalPosition < GlobalGameParametrs.VerticalCells
                                                                             && cheсkingHorizontalPosition < GlobalGameParametrs.HorizontalCells)//25 08 проверка на границы поля
            {
                Cell chekingCell = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
                Checker checkerOnCell = CheckerOnConreteCell(chekingCell);
                if (checkerOnCell != null)
                {
                    if (enemyOnLine)//25 08 был враг, а теперь линия заблочена
                    {  
                        return;
                    }
                    else
                    {
                        if (checkerOnCell.GetCheckerColor() == enemyColor) //22 08 враг
                        {
                            if (cellsForPurify.Count == 0)//25 08 за раз рубим одну шашку
                            {
                                enemyOnLine = true;
                                Cell cell = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
                                cellsForPurify.Add(chekingCell);
                                cell.AddCellToPurifyList(chekingCell);
                            }
                        }
                        else//22 08 Друг, линия заблокирвоана дальше не ищем
                        {
                            return;
                        }                        
                    }
                }
                
                else//22 08 пустая клетка = маркаем
                {
                    chekingCell.SetMoveable();
                    if (cellsForPurify.Count > 0)
                    {
                        foreach (var cellForPurify in cellsForPurify)
                        {
                            chekingCell.AddCellToPurifyList(cellForPurify);
                        }                            
                    }                    
                }
            }
        }
    }
    private void FindAttackMoveForChecker(Checker checker, int deltaVerticalPostion, int deltaHorizontalPosition)
    {
        CheckerColorEnum enemyColor;
        if (checker.GetCheckerColor() == CheckerColorEnum.GreenChecker)
        {
            enemyColor = CheckerColorEnum.RedChecker;
        }
        else
        {
            enemyColor = CheckerColorEnum.GreenChecker;
        }
        int verticalPosition = checker.GetVerticalPosition();//23 08 до этого тоже определяли, перед методом, нужно оптимизировать
        int horizontalPosition = checker.GetHorizontalPosition();
        int cheсkingVerticalPosition = verticalPosition + deltaVerticalPostion;
        int cheсkingHorizontalPosition = horizontalPosition + deltaHorizontalPosition;
        Cell cellThisEnemy = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
        List<Cell> cellsForPurify = new List<Cell>();
        Checker checkerOnCell = CheckerOnConreteCell(cellThisEnemy);
        if (checkerOnCell != null)
        {
            if (checkerOnCell.GetCheckerColor() == enemyColor) //22 08 враг
            {
                cellsForPurify.Add(cellThisEnemy);
                cheсkingVerticalPosition = verticalPosition + 2 * deltaVerticalPostion;
                cheсkingHorizontalPosition = horizontalPosition + 2 * deltaHorizontalPosition;
                Cell cell = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
                if (CheckerOnConreteCell(cell) == null)
                {
                    cell.SetMoveable();
                    foreach (var cellForPurify in cellsForPurify)//03 09 пока за раз рубим лишь одну
                    {
                        cell.AddCellToPurifyList(cellForPurify);
                    }
                }
            }
        }
    }

    private Checker CheckerOnConreteCell(Cell cell)
    {
        Checker checker = cell.GetCheckerOnCell();
        return checker;
    }


    void TryToAction()
    {
        if (_currentCell2d.GetCellData().IsMoveable())
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


    void UnchouseAllCells()
    {
        EventsManager.UnchouseAllCellsEvent?.Invoke();
    }

    void ClearPreviousChoused()
    {
        _chousedCell2d = null;
    }
}
