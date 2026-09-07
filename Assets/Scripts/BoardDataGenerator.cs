using UnityEngine;
using Zenject;

public class BoardDataGenerator : MonoBehaviour
{
    private Board _board;

    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }

    private void Start()
    {
        GenerateBoard();//03 08 из старт убрать
    }

    private void GenerateBoard()
    {
        BoardData boardData = new BoardData(GlobalGameParametrs.HorizontalCells, GlobalGameParametrs.VerticalCells, GlobalGameParametrs.CheckersAmount);
        _board.SetBoardData(boardData);
        EventsManager.BoardIsGeneratedEvent?.Invoke();
    }
}
