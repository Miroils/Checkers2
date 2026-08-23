using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Board : MonoBehaviour
{
    private BoardData _boardData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.ClearingCellEvent.AddListener(ClearingCell);
        EventsManager.TransitCheckerEvent.AddListener(TransitChecker);
        EventsManager.ResetCellsMoveableEvent.AddListener(ResetAllCellMoveable);
        EventsManager.ResetCellsPurifyEvent.AddListener(ResetAllCellsPurify);
        EventsManager.CellPurifyEvent.AddListener(CellPurify);
        EventsManager.DestroyCheckerEvent.AddListener(DestroyChecker);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }

    public BoardData GetBoardData()
    {
        return _boardData;
    }

    private void ClearingCell(int verticalPostion, int horizontalPostion)
    {
        //21 08 немного перегружено?
        _boardData.ClearCell(verticalPostion, horizontalPostion);
    }

    private void TransitChecker(Checker checker)
    {
        //21 08 немного перегружено?
        _boardData.SetCheckerOnCell(checker);    
        EventsManager.AnimateCheckerTranstionEvent?.Invoke(checker);
    }

    public Cell GetCellData(int verticalPostion, int horizontalPostion)
    {
        return _boardData.GetAnCellParametrs(verticalPostion, horizontalPostion);
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
