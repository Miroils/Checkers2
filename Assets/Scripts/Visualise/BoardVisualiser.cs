using UnityEngine;
using Zenject;

public class BoardVisualiser : MonoBehaviour
{
    private const float DELTA_POSITION_FOR_CELL = 1f;

    //17 08 разделить создание 2д клеток и остальную логику с ними
    [SerializeField] private Cell2D _boardTilePrefab;
    [SerializeField] private GameObject _startPositionForSell;
    private Board _board;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {        
        EventManager.BoardIsGeneratedEvent.AddListener(VisualiseBoard);   
    }

    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }

    private void VisualiseBoard()
    {
        //17 08 разделить взятие параметров и их обработку
        //03 08 взять из Board
        BoardData boardData = _board.GetBoardData();
        Cell[,] cells = boardData.GetCellsParametrs();
        int horizontalCells = cells.GetLength(0);
        int verticalCells = cells.GetLength(1);
        float cellPostionX = 0f;
        float cellPostionY = 0f;
        for (int i = 0; i < horizontalCells; i++)
        {            
            for (int j = 0; j < verticalCells; j++)
            {
                cellPostionX += DELTA_POSITION_FOR_CELL;                
                Vector2 cellFinalPositon = new Vector2(cellPostionX, cellPostionY);
                //test Cell2D cell = Instantiate(_boardTilePrefab, cellFinalPositon, Quaternion.identity, _startPositionForSell.transform);
                Cell2D cell2D = Instantiate(_boardTilePrefab, _startPositionForSell.transform);
                cell2D.transform.localPosition = cellFinalPositon;
                //17 08 тут запрос, а создание в другом скрипте?
                //17 08 пока прям тут создаем
                cell2D.Coloring(cells[i,j].GetCellColor());
            }
            cellPostionX = 0f;
            cellPostionY += DELTA_POSITION_FOR_CELL;
        }
    }

    private void VisualiseCheckers()
    {

    }
}
