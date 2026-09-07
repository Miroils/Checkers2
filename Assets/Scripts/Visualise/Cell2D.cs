using UnityEngine;

public class Cell2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _cellSprite;
    [SerializeField] private SpriteRenderer _cellHighlightSprite;
    [SerializeField] private SpriteRenderer _cellHighlightMoveableSprite;
    private Cell _cell;
    private bool _chouseable;
    private Color _whiteCell = new Color(0.86f, 0.807f, 0.807f);
    private Color _blackCell = new Color(0.302f, 0.231f, 0.157f);

    public void Coloring(CellColorEnum cellColorEnum)
    {
        if (cellColorEnum == CellColorEnum.BlackCell)
        {
            ColoringToBlack();
        }
        else
        {
            ColoringToWhite();
        }
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
        _cell = cell;
        _cell.MoveableChangedAction += HighLightMoveableCell;
        if (cell.GetCellColor() == CellColorEnum.BlackCell)
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

    private void ColoringToBlack()
    {
        _cellSprite.color = _blackCell;
    }

    private void ColoringToWhite()
    {
        _cellSprite.color = _whiteCell;
    }
    private void HighLightMoveableCell(bool isActive)
    {
        _cellHighlightMoveableSprite.gameObject.SetActive(isActive);
    }
}
