using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Board : MonoBehaviour
{
    private BoardData _boardData;
    
    public void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }
    
    public Cell GetCellData(int verticalPostion, int horizontalPostion)
    {
        return _boardData.Cells[verticalPostion, horizontalPostion];
    }

    public Cell[,] GetCellsParametrs()
    {
        return _boardData.Cells;
    }
    public List<Checker> GetCheckersList(CheckerColorEnum checkerColorEnum)
    {
        if (checkerColorEnum == CheckerColorEnum.RedChecker)
        {
            return _boardData.RedCheckers;
        }
        return _boardData.GreenCheckers;
    }

    private void Start()
    {
        EventsManager.ClearingCellEvent.AddListener(ClearingCell);
        EventsManager.SetCheckerOnCellEvent.AddListener(SetCheckerOnCell);
        EventsManager.TransitCheckerEvent.AddListener(TransitChecker);
        EventsManager.ResetCellsMoveableEvent.AddListener(ResetAllCellMoveable);
        EventsManager.ResetCellsPurifyEvent.AddListener(ResetAllCellsPurify);
        EventsManager.CellPurifyEvent.AddListener(CellPurify);
        EventsManager.DestroyCheckerEvent.AddListener(DestroyChecker);
    }

    private void ClearingCell(int verticalPostion, int horizontalPostion)
    {
        _boardData.Cells[verticalPostion, horizontalPostion].RemoveCheckerFromCell();
    }

    private void SetCheckerOnCell(Checker checker)
    {
        _boardData.Cells[checker.GetVerticalPosition(), checker.GetHorizontalPosition()].SetCheckerOnCell(checker);
    }

    private void TransitChecker(Checker checker)
    {
        _boardData.Cells[checker.GetVerticalPosition(), checker.GetHorizontalPosition()].SetCheckerOnCell(checker);
        EventsManager.AnimateCheckerTranstionEvent?.Invoke(checker);
    }

    private void ResetAllCellMoveable()
    {
        foreach (Cell cell in _boardData.Cells)
        {
            cell.ResetMoveable();
        }
    }

    private void ResetAllCellsPurify()
    {
        foreach (Cell cell in _boardData.Cells)
        {
            cell.ClearPurifyList();
        }
    }

    private void CellPurify(Cell cell)
    {
        cell.PurifyCell();
    }

    private void DestroyChecker(Checker checker)
    {
        _boardData.GreenCheckers.Remove(checker);
        _boardData.RedCheckers.Remove(checker);
        checker.DestroyChecker();
    }
}
