using UnityEngine;

public class GameEventsController : MonoBehaviour
{
    private Cell2D _currentCell2d;//20 08 от 2д Уйти?
    private Cell2D _chousedCell2d;//20 08 выбранная ранее
    private Cell _currentCell;//20 08 от 2д Уйти?
    private Checker _checkerOncell;
    //20 08 спорно название, предполагается ренейм и может разделение на сабклассы
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.RightClickEvent.AddListener(HandleRightClick);
        EventsManager.LeftClickOnCellEvent.AddListener(HandleLeftClick);
        Debug.Log("CONECCT");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleRightClick()
    {
        UnchouseAllCells();
    }

    void HandleLeftClick(Cell2D cell2d)
    {
        _currentCell2d = cell2d;
        _currentCell = _currentCell2d.GetCellData();
        //20 08 принимать не 2д, с просто селл?            
        UnchouseAllCells();
        //20 08 еще нужны условия, чей сейчас ход            
        if (_currentCell2d.IsChouseable()) //20 08 все черные
        {
            if (_currentCell.GetCheckerOnCell() != null)//не пустая
            {
                ChouseNewCell();
            }
        }
        
        if (_chousedCell2d != null)
        {
            TryToAction();
        }
    }

    void ChouseNewCell()
    {
        Debug.Log("ChouseNewCell");
        _currentCell2d.ChouseCell();
        _chousedCell2d = _currentCell2d;
        _checkerOncell = _chousedCell2d.GetCellData().GetCheckerOnCell();
    }

    void TryToAction()
    {
        Debug.Log("TryToAction");
        if (_currentCell2d.IsChouseable()) //20 08 все черные
        {
            //Checker checkerOncell = _chousedCell2d.GetCellData().GetCheckerOnCell();
            if (_currentCell.GetCheckerOnCell() == null)//пустая
            {
                Debug.Log("ПУСТАЯ");
                //21 08 пересобрать в другие методы или даже скрипты
                if (_checkerOncell.GetCheckerColor() == CheckerColorEnum.greenChecker)
                {
                    GreenCheckerAction();
                }
                else //red Checker
                {
                    RedCheckerAction();
                }
            }
        }
                //20 08 пытаемся передвинуть выбранную шашку
                //20 08 если не получилось - сбросить выделение (в перспективе)
    }

    //22 08 нужна оптимазация кода
    void GreenCheckerAction()
    {
        //22 08 соседняя клетка = перемещение
        if (_checkerOncell.GetVerticalPosition() - _currentCell.GetVerticalPostion() == -1)
        {
            if (_checkerOncell.GetHorizontalPosition() - _currentCell.GetHorizontalPostion() == 1 || _checkerOncell.GetHorizontalPosition() - _currentCell.GetHorizontalPostion() == -1)
            {
                _checkerOncell.MoveToNewPosition(_currentCell.GetVerticalPostion(), _currentCell.GetHorizontalPostion());
                ClearPreviousChoused();
            }
        }
        else if (_checkerOncell.GetVerticalPosition() - _currentCell.GetVerticalPostion() == -2 || _checkerOncell.GetVerticalPosition() - _currentCell.GetVerticalPostion() == 2)//22 08 через одну
        {
            //проверка ближайшей на врага
            //перемещение со срубом
        }
    }

    void RedCheckerAction()
    {
        //21 08 частичное дублирование?
        //22 08 соседняя клетка = перемещение
        if (_checkerOncell.GetVerticalPosition() - _currentCell.GetVerticalPostion() == 1)
        {
            if (_checkerOncell.GetHorizontalPosition() - _currentCell.GetHorizontalPostion() == 1 || _checkerOncell.GetHorizontalPosition() - _currentCell.GetHorizontalPostion() == -1)
            {
                _checkerOncell.MoveToNewPosition(_currentCell.GetVerticalPostion(), _currentCell.GetHorizontalPostion());
                ClearPreviousChoused();
            }
        }
    }

    void UnchouseAllCells()
    {
        //в BoardVisualiser нужно передать
        EventsManager.UnchouseAllCellsEvent?.Invoke();
        //_chousedCell2d = null;//20 08 прокинуть внутри ивента и связать эти действия в другом скрипте?
    }

    void ClearPreviousChoused()
    {
        _chousedCell2d = null;
    }
}
