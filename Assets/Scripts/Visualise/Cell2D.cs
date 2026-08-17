using UnityEngine;

public class Cell2D : MonoBehaviour
{
    private Color whiteCell = new Color(0.86f, 0.807f, 0.807f);
    private Color blackCell = new Color(0.302f, 0.231f, 0.157f);
    [SerializeField] private SpriteRenderer cellSprite;
    private Cell _cell;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateSet2DColor()
    {
        if (_cell.GetCellColor() == CellColorEnum.blackCell)
        {
            cellSprite.color = Color.black;
        }
    }

    public void UpdateCellData(Cell cell)
    {
        _cell = cell;
        UpdateSet2DColor();
    }

    public void Coloring(CellColorEnum cellColorEnum)
    {
        if (cellColorEnum == CellColorEnum.blackCell)
        {
            ColoringToBlack();
        }
        else
        {
            ColoringToWhite();
        }
    }

    private void ColoringToBlack()
    {
        cellSprite.color = blackCell;
    }

    private void ColoringToWhite()
    {
        cellSprite.color = whiteCell;
    }
}
