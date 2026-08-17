using UnityEngine;

public class BoardData
{
    private int _horizontalCells;
    private int _verticalCells;
    private Cell[,] _cells;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BoardData (int horizontalCells, int verticalCells)
    {
        _horizontalCells = horizontalCells;
        _verticalCells = verticalCells;
        CellsInit();
    }
    //03 08 массив из Cells, покраска cells в белый/черный

    private void CellsInit()
    {
        _cells = new Cell[_horizontalCells, _verticalCells];
        
        CreatingBlackWhiteCells();
        
    }

    void CreatingBlackWhiteCells()
    {
        for (int i = 0; i < _verticalCells; i++)
        {
            for (int j = 0; j < _horizontalCells; j++)
            {
                _cells[i, j] = new Cell();
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                    {
                        _cells[i, j].SetCellColor(CellColorEnum.blackCell);
                    }
                }
                else
                {
                    if (j % 2 == 1)
                    {
                        _cells[i, j].SetCellColor(CellColorEnum.blackCell);
                    }
                }
            }
        }
    }

    public Cell[,] GetCellsParametrs()
    {
        return _cells;
    }
}
