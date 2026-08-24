using UnityEngine;
using Zenject;

public class BoardDataGenerator : MonoBehaviour
{

    private Board _board;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateBoard();//03 08 из старт убрать
    }

    void GenerateBoard()
    {
        BoardData boardData = new BoardData(GlobalGameParametrs.HorizontalCells, GlobalGameParametrs.VerticalCells, GlobalGameParametrs.CheckersAmount);
        _board.SetBoardData(boardData);
        //03 08 передать в Board
        //Board board = Instantiate(boardData);
        EventsManager.BoardIsGeneratedEvent?.Invoke();
    }
    
    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }
}
