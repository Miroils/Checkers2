using UnityEngine;

public class Checker2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _CheckerSprite;
    private Color _greenChecker = new Color(0.0f, 1.0f, 0.0f);//19 08 тестово дефолтные цвета
    private Color _redChecker = new Color(1.0f, 0.0f, 0.0f);//19 08 тестово дефолтные цвета

    private Checker _checker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //19 08 попытка через конструктор создавать
    public void Initialization(Checker checker)
    {
        _checker = checker;
        UpdateCheckerVisual();
    }

    void UpdateCheckerVisual()
    {
        if (_checker.IsQueen())
        {
            //19 08 загружаем ресурс королевы
        }
        else
        {
            //19 08 загружаем ресур пешки
        }
        if (_checker.GetCheckerColor() == CheckerColorEnum.redChecker)
        {
            ColoringToRed();
        }
        else
        {
            ColoringToGreen();
        }
    }

    private void ColoringToRed()
    {
        _CheckerSprite.color = _redChecker;
    }
    private void ColoringToGreen()
    {
        _CheckerSprite.color = _greenChecker;
    }
}
