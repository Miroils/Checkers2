using UnityEngine;
using Zenject;

public class BoardDataGenerator : MonoBehaviour
{
    //18 08 размеры поля и количество шашек пока жестко фиксировано
    private const int VERTICAL_CELLS = 8;//18 08 пока нельзя меньше 6
    private const int HORIZONTAL_CELLS = 8;
    private const int CHECKERS_AMOUNT = 12;//for each color //18 08 пока бесполезный параметр

    private Board _board;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateBoard();//03 08 из старт убрать
        GenerateCheckers();
    }

    void GenerateBoard()
    {
        BoardData boardData = new BoardData(HORIZONTAL_CELLS, VERTICAL_CELLS, CHECKERS_AMOUNT);
        _board.SetBoardData(boardData);
        //03 08 передать в Board
        //Board board = Instantiate(boardData);
        EventManager.BoardIsGeneratedEvent?.Invoke();
    }

    
    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }
    

    void GenerateCheckers()
    {

    }
}
