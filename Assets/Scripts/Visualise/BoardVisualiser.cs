using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class BoardVisualiser : MonoBehaviour
{
    private const float DELTA_POSITION_FOR_CELL = 1f;

    //17 08 разделить создание 2д клеток и остальную логику с ними
    [SerializeField] private Cell2D _boardCellPrefab;
    [SerializeField] private Checker2D _checkerPrefab;
    [SerializeField] private GameObject _startPositionForSell;
    private Board _board;
    private BoardData _boardData;

    private Cell[,] _cells;
    private Cell2D[,] _cells2d;
    private int _horizontalCells;
    private int _verticalCells;
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
        _boardData = _board.GetBoardData();
        _cells = _boardData.GetCellsParametrs();
        _verticalCells = _cells.GetLength(0);
        _horizontalCells = _cells.GetLength(1);
        VisualiseCells();
        VisualiseCheckers();
    }

    private void VisualiseCells()
    {
        //17 08 разделить взятие параметров и их обработку
        //03 08 взять из Board
        float cellPostionX = 0f;
        float cellPostionY = 0f;
        _cells2d = new Cell2D[_verticalCells, _horizontalCells];
        for (int i = 0; i < _verticalCells; i++)
        {
            for (int j = 0; j < _horizontalCells; j++)
            {
                cellPostionX += DELTA_POSITION_FOR_CELL;
                Vector2 cellFinalPositon = new Vector2(cellPostionX, cellPostionY);
                //test Cell2D cell = Instantiate(_boardTilePrefab, cellFinalPositon, Quaternion.identity, _startPositionForSell.transform);
                Cell2D cell2D = Instantiate(_boardCellPrefab, _startPositionForSell.transform);
                cell2D.transform.localPosition = cellFinalPositon;
                //17 08 тут запрос, а создание в другом скрипте?
                //17 08 пока прям тут создаем
                cell2D.Coloring(_cells[i, j].GetCellColor());
                _cells2d[i,j] = cell2D;
            }
            cellPostionX = 0f;
            cellPostionY += DELTA_POSITION_FOR_CELL;
        }
    }
    private void VisualiseCheckers()
    {
        List<Checker> redCheckersList = _boardData.GetCheckersList(CheckerColorEnum.redChecker);        
        List<Checker> greenCheckersList = _boardData.GetCheckersList(CheckerColorEnum.greenChecker);    
        
        
        foreach (Checker checker in redCheckersList)
        {            
            int verticalPostion = checker.GetVerticalPosition();
            int horizontalPostion = checker.GetHorizontalPosition();
            Checker2D checker2D = Instantiate(_checkerPrefab, _cells2d[verticalPostion, horizontalPostion].transform);
            checker2D.Initialization(checker);
        }

        foreach (Checker checker in greenCheckersList)
        {
            int verticalPostion = checker.GetVerticalPosition();
            int horizontalPostion = checker.GetHorizontalPosition();
            Checker2D checker2D = Instantiate(_checkerPrefab, _cells2d[verticalPostion, horizontalPostion].transform);
            checker2D.Initialization(checker);
        }

        //пробегаемся по списку
        //генерируем шашку (по хорошему конструктор задействовать)
        //выставляем в позиции соотвествующей ячейки, номер ячейки в данных чекер
    }
}
