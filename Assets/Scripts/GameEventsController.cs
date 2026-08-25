using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class GameEventsController : MonoBehaviour
{
    //22 08 нужно связать с BoardData или Board, через Zenject?


    private Cell2D _currentCell2d;//20 08 от 2д Уйти?
    private Cell2D _chousedCell2d;//20 08 выбранная ранее
    private Cell _currentCell;//20 08 от 2д Уйти?
    private Checker _checkerOncell;
    //20 08 спорно название, предполагается ренейм и может разделение на сабклассы
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Board _board;

    private Cell _enemyCellOnQueenLine;//24 08 для просчета пути королевы
    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }
    void Start()
    {
        EventsManager.RightClickEvent.AddListener(HandleRightClick);
        EventsManager.LeftClickOnCellEvent.AddListener(HandleLeftClick);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                _currentCell.PurifingFromList();
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
        _checkerOncell = _chousedCell2d.GetCellData().GetCheckerOnCell();
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
            if (checker.GetCheckerColor() == CheckerColorEnum.greenChecker)
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
        CheckerColorEnum enemyColor;
        List<Cell> cellsForPurify = new List<Cell>();
        bool enemyOnLine = false;
        if (checker.GetCheckerColor() == CheckerColorEnum.greenChecker)
        {
            enemyColor = CheckerColorEnum.redChecker;
        }
        else
        {
            enemyColor = CheckerColorEnum.greenChecker;
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
        if (checker.GetCheckerColor() == CheckerColorEnum.greenChecker)
        {
            enemyColor = CheckerColorEnum.redChecker;
        }
        else
        {
            enemyColor = CheckerColorEnum.greenChecker;
        }
        int verticalPosition = checker.GetVerticalPosition();//23 08 до этого тоже определяли, перед методом, нужно оптимизировать
        int horizontalPosition = checker.GetHorizontalPosition();
        int cheсkingVerticalPosition = verticalPosition + deltaVerticalPostion;
        int cheсkingHorizontalPosition = horizontalPosition + deltaHorizontalPosition;
        Cell cellThisEnemy = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
        Checker checkerOnCell = CheckerOnConreteCell(cellThisEnemy);
        if (checkerOnCell != null)
        {
            if (checkerOnCell.GetCheckerColor() == enemyColor) //22 08 враг
            {
                cheсkingVerticalPosition = verticalPosition + 2 * deltaVerticalPostion;
                cheсkingHorizontalPosition = horizontalPosition + 2 * deltaHorizontalPosition;
                Cell cell = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
                if (CheckerOnConreteCell(cell) == null)
                {
                    cell.SetMoveable();
                    cell.AddCellToPurifyList(cellThisEnemy);
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
            TransitToCell();
        }
    }

    private void TransitToCell()
    {
        _checkerOncell.MoveToNewPosition(_currentCell.GetVerticalPostion(), _currentCell.GetHorizontalPostion());
        ClearPreviousChoused();
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
