public class Cell 
{
    //03 08 позиция?
    //03 08 или то, что она на краю?
    private CellColorEnum _cellColor;
    private Checker _checkerOnCell;

    private int _verticalPostion;
    private int _horizontalPostion;

    public void SetCellColor(CellColorEnum newCellColor) //18 08 в конструктор закинуть?
    {
        _cellColor = newCellColor;
    }

    public CellColorEnum GetCellColor()
    {
        return _cellColor;
    }

    public void SetCheckerOnCell(Checker checker)
    {
        _checkerOnCell = checker;
    }

    public void RemoveCheckerFromCell()
    {
        _checkerOnCell = null;
    }

    public Checker GetCheckerOnCell()
    {
        return _checkerOnCell;
    }

    public void SetCellPostion(int verticalPostion, int horizontalPostion)
    {
        _verticalPostion = verticalPostion;
        _horizontalPostion = horizontalPostion;
    }

    public int GetVerticalPostion()
    {
        return _verticalPostion;
    }

    public int GetHorizontalPostion()
    {
        return _horizontalPostion;
    }
}
