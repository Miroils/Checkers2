using UnityEngine;

public class Cell2D : MonoBehaviour
{
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
}
