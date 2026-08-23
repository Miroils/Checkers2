using UnityEngine;

public class Cell2D : MonoBehaviour
{
    private Color _whiteCell = new Color(0.86f, 0.807f, 0.807f);
    private Color _blackCell = new Color(0.302f, 0.231f, 0.157f);
    [SerializeField] private SpriteRenderer _cellSprite;
    [SerializeField] private SpriteRenderer _cellHighlightSprite;
    [SerializeField] private SpriteRenderer _cellHighlightMoveableSprite;
    private Cell _cell;//19 08 эти данные не получаю, крашу снаружи, может правильней принимать данные и тут уже красить???
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool _chouseable;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HighLightMoveableCell(bool isActive)
    {
        _cellHighlightMoveableSprite.gameObject.SetActive(isActive);
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

    public void ChouseCell()
    {
        _cellHighlightSprite.gameObject.SetActive(true);
    }

    public void UnchouseCell()
    {
        _cellHighlightSprite.gameObject.SetActive(false);
    }

    public void Initialization(Cell cell)
    {
        //20 08 вызывать перекраску тут а не снаружи
        _cell = cell;
        _cell.MoveableChangedAction += HighLightMoveableCell;
        if (cell.GetCellColor() == CellColorEnum.blackCell)
        {
            _chouseable = true;
        }
    }

    public bool IsChouseable()
    {
        return _chouseable;
    }

    public Cell GetCellData()
    {
        return _cell;
    }
}
