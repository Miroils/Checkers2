using UnityEngine;

public class Cell 
{
    //03 08 позиция?
    //03 08 или то, что она на краю?
    private CellColorEnum _cellColor;

    public void SetCellColor(CellColorEnum newCellColor)
    {
        _cellColor = newCellColor;
    }

    public CellColorEnum GetCellColor()
    {
        return _cellColor;
    }
}
