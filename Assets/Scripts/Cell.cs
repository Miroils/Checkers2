public class Cell 
{
    //03 08 позиция?
    //03 08 или то, что она на краю?
    private CellColorEnum _cellColor;
    private Checker _checkerOnCell;

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
}
