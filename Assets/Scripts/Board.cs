using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Board : MonoBehaviour
{
    private BoardData _boardData;

    public BoardData GetBoardData()
    {
        return _boardData;
    }

    public void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }

    public Cell GetCellData(int verticalPostion, int horizontalPostion)
    {
        return _boardData.GetAnCellParametrs(verticalPostion, horizontalPostion);
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
        _boardData.ClearCell(verticalPostion, horizontalPostion);
    }

    private void SetCheckerOnCell(Checker checker)
    {
        _boardData.SetCheckerOnCell(checker);
    }

    private void TransitChecker(Checker checker)
    {
        _boardData.SetCheckerOnCell(checker);    
        EventsManager.AnimateCheckerTranstionEvent?.Invoke(checker);
    }

    private void ResetAllCellMoveable()
    {
        _boardData.ResetAllCellMoveable();
    }

    private void ResetAllCellsPurify()
    {
        _boardData.ResetAllCellPurify();
    }

    private void CellPurify(Cell cell)
    {
        cell.PurifyCell();
    }

    private void DestroyChecker(Checker checker)
    {
        _boardData.DestroyChecker(checker);
    }
}
