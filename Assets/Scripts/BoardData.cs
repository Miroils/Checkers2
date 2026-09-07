using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BoardData
{
    //18 08 перегруженный скрипт?, сами данные и процесс создания вероятно стоит как-то разделить
    private int _horizontalCells;
    private int _verticalCells;
    private int _checkersAmount;//18 08 Сейчас заданное количество значения, создается исходя из размеров поля, в 3 ряда
    private Cell[,] _cells;
    private List<Checker> _redCheckers;
    private List<Checker> _greenCheckers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BoardData (int horizontalCells, int verticalCells, int checkersAmount)
    {
        _horizontalCells = horizontalCells;
        _verticalCells = verticalCells;
        _checkersAmount = checkersAmount;
        CellsInit();
        CheckersInit();
    }
    //03 08 массив из Cells, покраска cells в белый/черный

    private void CellsInit()
    {
        //18 08 объеденить методы?
        _cells = new Cell[_verticalCells, _horizontalCells];
        CreatingBlackWhiteCells();        
    }

    private void CheckersInit()
    {
        ResetCheckersLists();
        CreateGreenCheckers();
        CreateRedCheckers();
    }

    private void ResetCheckersLists()
    {
        _redCheckers = new List<Checker>();
        _greenCheckers = new List<Checker>();        
    }

    private void CreateGreenCheckers()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < _horizontalCells; j++)
            {
                if (_cells[i,j].GetCellColor() == CellColorEnum.BlackCell)
                {
                    Checker newChecker = new Checker(CheckerColorEnum.GreenChecker, i, j);
                    _greenCheckers.Add(newChecker);//18 08 повторяемость с red?
                    _cells[i, j].SetCheckerOnCell(newChecker);//18 08 повторяемость с red?
                }
            }
        }
    }
    private void CreateRedCheckers()
    {
        for (int i = _verticalCells-1; i > _verticalCells - 4; i--)
        {
            for (int j = 0; j < _horizontalCells; j++)
            {
                if (_cells[i, j].GetCellColor() == CellColorEnum.BlackCell)
                {
                    Checker newChecker = new Checker(CheckerColorEnum.RedChecker, i, j);
                    _redCheckers.Add(newChecker);//18 08 повторяемость с green?
                    _cells[i, j].SetCheckerOnCell(newChecker);//18 08 повторяемость с green?
                }
            }
        }
    }

    private void CreatingBlackWhiteCells()
    {
        for (int i = 0; i < _verticalCells; i++)
        {
            for (int j = 0; j < _horizontalCells; j++)
            {
                _cells[i, j] = new Cell();
                _cells[i, j].SetCellPostion(i, j);
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                    {
                        _cells[i, j].SetCellColor(CellColorEnum.BlackCell);
                    }
                }
                else
                {
                    if (j % 2 == 1)
                    {
                        _cells[i, j].SetCellColor(CellColorEnum.BlackCell);
                    }
                }
            }
        }
    }

    public Cell[,] GetCellsParametrs()
    {
        return _cells;
    }

    public Cell GetAnCellParametrs(int verticalPostion, int horizontalPostion)
    {
        return _cells[verticalPostion, horizontalPostion];
    }

    public List<Checker> GetCheckersList(CheckerColorEnum checkerColorEnum)
    {
        if (checkerColorEnum == CheckerColorEnum.RedChecker)
        {
            return _redCheckers;
        }
        return _greenCheckers;
    }

    public void ClearCell(int verticalPostion, int horizontalPostion)
    {
        _cells[verticalPostion, horizontalPostion].RemoveCheckerFromCell();
    }

    public void SetCheckerOnCell(Checker checker)
    {
        _cells[checker.GetVerticalPosition(), checker.GetHorizontalPosition()].SetCheckerOnCell(checker);
    }

    public void ResetAllCellMoveable()
    {
        foreach (Cell cell in _cells)
        {
            cell.ResetMoveable();
        }
    }
    public void ResetAllCellPurify()
    {
        foreach (Cell cell in _cells)
        {
            cell.ClearPurifyList();
        }
    }
    
    public void DestroyChecker(Checker checker)
    {
        checker.DestroyChecker();
        _greenCheckers.Remove(checker);
        _redCheckers.Remove(checker);
    }
}
