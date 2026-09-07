using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardCreator : MonoBehaviour
{
    private Board _board;
    private int _verticalCells = GlobalGameParametrs.VerticalCells;
    private int _horizontalCells = GlobalGameParametrs.HorizontalCells;
    private int _checkersAmount = GlobalGameParametrs.CheckersAmount;
    private Cell[,] _cells;
    private List<Checker> _redCheckers;
    private List<Checker> _greenCheckers;

    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }

    private void Start() //07 09 в перспективе можно убрать из старта и сделать по запросу
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        BoardData boardData = new BoardData();
        boardData.Cells = CellsInit();
        CheckersInit();
        boardData.RedCheckers = _redCheckers;
        boardData.GreenCheckers = _greenCheckers;
        _board.SetBoardData(boardData);
        EventsManager.BoardIsGeneratedEvent?.Invoke();
    }
    
    private Cell[,] CellsInit()
    {
        _cells = new Cell[_verticalCells, _horizontalCells];
        CreatingBlackWhiteCells();
        return _cells;
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
                if (_cells[i, j].GetCellColor() == CellColorEnum.BlackCell)
                {
                    Checker newChecker = new Checker(CheckerColorEnum.GreenChecker, i, j);
                    _greenCheckers.Add(newChecker);
                    _cells[i, j].SetCheckerOnCell(newChecker);
                }
            }
        }
    }
    private void CreateRedCheckers()
    {
        for (int i = _verticalCells - 1; i > _verticalCells - 4; i--)
        {
            for (int j = 0; j < _horizontalCells; j++)
            {
                if (_cells[i, j].GetCellColor() == CellColorEnum.BlackCell)
                {
                    Checker newChecker = new Checker(CheckerColorEnum.RedChecker, i, j);
                    _redCheckers.Add(newChecker);
                    _cells[i, j].SetCheckerOnCell(newChecker);
                }
            }
        }
    }
}
