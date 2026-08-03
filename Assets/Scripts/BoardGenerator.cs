using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
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
        //03 08 передать в Board
        EventManager.BoardIsGeneratedEvent?.Invoke();
    }

    void GenerateCheckers()
    {

    }
}
