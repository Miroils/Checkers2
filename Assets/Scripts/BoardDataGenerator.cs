using UnityEngine;
using Zenject;

public class BoardDataGenerator : MonoBehaviour
{
    //06 08 [Inject]
    private Board _board;

    private const int VERTICAL_CELLS = 8;
    private const int HORIZONTAL_CELLS = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateBoard();//03 08 из старт убрать
    }

    void GenerateBoard()
    {
        BoardData boardData = new BoardData(HORIZONTAL_CELLS, VERTICAL_CELLS);
        _board.SetBoardData(boardData);
        //03 08 передать в Board
        //Board board = Instantiate(boardData);
        EventManager.BoardIsGeneratedEvent?.Invoke();
    }

    
    [Inject]
    private void Construct(Board board)
    {
        _board = board;
        board.TestMetod();
    }
    

    void GenerateCheckers()
    {

    }
}
