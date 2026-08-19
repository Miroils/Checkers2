using UnityEngine;

public class Cell2D : MonoBehaviour
{
    private Color _whiteCell = new Color(0.86f, 0.807f, 0.807f);
    private Color _blackCell = new Color(0.302f, 0.231f, 0.157f);
    [SerializeField] private SpriteRenderer _cellSprite;
    private Cell _cell;//19 08 эти данные не получаю, крашу снаружи, может правильней принимать данные и тут уже красить???
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        _cellSprite.color = _blackCell;
    }

    private void ColoringToWhite()
    {
        _cellSprite.color = _whiteCell;
    }
}
