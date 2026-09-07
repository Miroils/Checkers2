using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveFinder : MonoBehaviour
{
    private Board _board;
    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }
    private void Start()
    {
        EventsManager.FindMovesEvent.AddListener(FindMoveableCells);
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
        else 
        {
            if (checker.GetCheckerColor() == CheckerColorEnum.GreenChecker)
            {
                int cheсkingVerticalPosition = verticalPostion + 1;
                int cheсkingHorizontalPosition = horizontalPostion - 1;
                TryToSetCellMoveable(cheсkingVerticalPosition, cheсkingHorizontalPosition);
                cheсkingHorizontalPosition = horizontalPostion + 1;
                TryToSetCellMoveable(cheсkingVerticalPosition, cheсkingHorizontalPosition);                
            }
            else//22 08 red
            {
                int cheсkingVerticalPosion = verticalPostion - 1;
                int cheсkingHorizontalPosion = horizontalPostion - 1;                    
                TryToSetCellMoveable(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                cheсkingHorizontalPosion = horizontalPostion + 1;
                TryToSetCellMoveable(cheсkingVerticalPosion, cheсkingHorizontalPosion);                
            }
            FindAttackMoveForChecker(checker, -1, -1);//лево низ
            FindAttackMoveForChecker(checker, -1, 1);//право низ
            FindAttackMoveForChecker(checker, 1, -1);//лево верх
            FindAttackMoveForChecker(checker, 1, 1);//право верх  
        }
    }

    private void TryToSetCellMoveable(int cheсkingVerticalPosition, int cheсkingHorizontalPosition)
    {
        if (cheсkingVerticalPosition >= 0 && cheсkingVerticalPosition < GlobalGameParametrs.VerticalCells &&
            cheсkingHorizontalPosition >= 0 && cheсkingHorizontalPosition < GlobalGameParametrs.HorizontalCells)
        {
            Cell cell = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
            if (CheckerOnConreteCell(cell) == null)
            {
                cell.SetMoveable();
            }
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
            if (CheckCellExist(cheсkingVerticalPosition, cheсkingHorizontalPosition))
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
        if (CheckCellExist(cheсkingVerticalPosition, cheсkingHorizontalPosition))
        {
            Cell cellThisEnemy = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
            List<Cell> cellsForPurify = new List<Cell>();
            Checker checkerOnCell = CheckerOnConreteCell(cellThisEnemy);
            if (checkerOnCell != null)
            {
                if (checkerOnCell.GetCheckerColor() == enemyColor)
                {
                    cellsForPurify.Add(cellThisEnemy);
                    cheсkingVerticalPosition = verticalPosition + 2 * deltaVerticalPostion;
                    cheсkingHorizontalPosition = horizontalPosition + 2 * deltaHorizontalPosition;
                    if (CheckCellExist(cheсkingVerticalPosition, cheсkingHorizontalPosition))
                    {
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
        }
    }
    private bool CheckCellExist(int verticalPosition, int horizontalPosition)
    {
        if (verticalPosition >= 0 && horizontalPosition >= 0 && verticalPosition < GlobalGameParametrs.VerticalCells
                                                             && horizontalPosition < GlobalGameParametrs.HorizontalCells)
        {
            return true;
        }
        return false;
    }
    private Checker CheckerOnConreteCell(Cell cell)
    {
        Checker checker = cell.GetCheckerOnCell();
        return checker;
    }
}
