using UnityEngine;
using Zenject;

public class GameEventsController : MonoBehaviour
{
    //22 08 нужно связать с BoardData или Board, через Zenject?


    private Cell2D _currentCell2d;//20 08 от 2д Уйти?
    private Cell2D _chousedCell2d;//20 08 выбранная ранее
    private Cell _currentCell;//20 08 от 2д Уйти?
    private Checker _checkerOncell;
    //20 08 спорно название, предполагается ренейм и может разделение на сабклассы
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Board _board;

    [Inject]
    private void Construct(Board board)
    {
        _board = board;
    }
    void Start()
    {
        EventsManager.RightClickEvent.AddListener(HandleRightClick);
        EventsManager.LeftClickOnCellEvent.AddListener(HandleLeftClick);
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
                ResetMoveAbleCells();
                ResetPurifyCellsList();
                ChouseNewCell();
            }

            else if (_chousedCell2d != null)
            {
                TryToAction();
                _currentCell.PurifingFromList();
                ResetMoveAbleCells();
                ResetPurifyCellsList();
            }
        }
    }

    private void ResetMoveAbleCells()
    {
        EventsManager.ResetCellsMoveableEvent?.Invoke();
    }

    private void ResetPurifyCellsList()
    {
        EventsManager.ResetCellsPurifyEvent?.Invoke();
    }
    void ChouseNewCell()
    {
        _currentCell2d.ChouseCell();
        _chousedCell2d = _currentCell2d;
        _checkerOncell = _chousedCell2d.GetCellData().GetCheckerOnCell();
        FindMoveableCells(_checkerOncell);
    }

    private void FindMoveableCells(Checker checker)
    {
        int verticalPostion = checker.GetVerticalPosition();
        int horizontalPostion = checker.GetHorizontalPosition();
        if (checker.IsQueen())
        {
            //22 08 перемещение для квины позже добавлю
        }
        else
        {
            //Движение вперед нужно подсобрать, DRY
            if (checker.GetCheckerColor() == CheckerColorEnum.greenChecker)
            {
                //22 08 собрать в один метод, а от передаваемого цвета изменять направление движения, или сразу передавать направление движения?
                //22 08 ДВИЖЕНИЕ ВПЕРЕД!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                int cheсkingVerticalPosion = verticalPostion + 1;
                int cheсkingHorizontalPosion = 0;
                if (cheсkingVerticalPosion < 8)//22 08 нужно не жестко а брать конст размер поля
                {
                    cheсkingHorizontalPosion = horizontalPostion - 1;
                    if (cheсkingHorizontalPosion >= 0)
                    {
                        //22 08 дублирование даже бзе ред
                        Cell cell = _board.GetCellData(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                        if (CheckerOnConreteCell(cell) == null)
                        {
                            cell.SetMoveable();
                        }
                    }

                    cheсkingHorizontalPosion = horizontalPostion + 1;
                    if (cheсkingHorizontalPosion < 8) //22 08 нужно не жестко задавать
                    {
                        //22 08 дублирование даже бзе ред
                        Cell cell = _board.GetCellData(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                        if (CheckerOnConreteCell(cell) == null)
                        {
                            cell.SetMoveable();
                        }
                    }
                }
               
            }

            else//22 08 red
            {
                int cheсkingVerticalPosion = verticalPostion - 1;
                if (cheсkingVerticalPosion >= 0)//22 08 нужно не жестко а брать конст размер поля
                {
                    int cheсkingHorizontalPosion = horizontalPostion - 1;
                    if (cheсkingHorizontalPosion >= 0)
                    {
                        Cell cell = _board.GetCellData(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                        if (CheckerOnConreteCell(cell) == null)
                        {
                            cell.SetMoveable();
                        }
                    }

                    cheсkingHorizontalPosion = horizontalPostion + 1;
                    if (cheсkingHorizontalPosion < 8) //22 08 нужно не жестко задавать
                    {
                        Cell cell = _board.GetCellData(cheсkingVerticalPosion, cheсkingHorizontalPosion);
                        if (CheckerOnConreteCell(cell) == null)
                        {
                            cell.SetMoveable();
                        }
                    }
                }
            }
            //22 08 ДВИЖЕНИЕ ВПЕРЕД!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //22 08 цифры нужно подвязть к размеру поля, а не магические
            if (verticalPostion >= 2 && horizontalPostion >= 2)
            {
                //лево низ
                FindAttackMoveForChecker(checker, -1, -1);                    
            }

            if (verticalPostion >= 2 && horizontalPostion < 6)
            {
                //право низ
                FindAttackMoveForChecker(checker, -1, 1);
            }

            if (verticalPostion < 6 && horizontalPostion >= 2)
            {
                //лево верх
                FindAttackMoveForChecker(checker, 1, -1);
            }

            if (verticalPostion < 6 && horizontalPostion < 6)
            {
                //право верх
                FindAttackMoveForChecker(checker, 1, 1);
            }
        }
    }

    private void FindAttackMoveForChecker(Checker checker, int deltaVerticalPostion, int deltaHorizontalPosition)
    {
        CheckerColorEnum enemyColor;
        if (checker.GetCheckerColor() == CheckerColorEnum.greenChecker)
        {
            enemyColor = CheckerColorEnum.redChecker;
        }
        else
        {
            enemyColor = CheckerColorEnum.greenChecker;
        }
        int verticalPosition = checker.GetVerticalPosition();//23 08 до этого тоже определяли, перед методом, нужно оптимизировать
        int horizontalPosition = checker.GetHorizontalPosition();
        int cheсkingVerticalPosition = verticalPosition + deltaVerticalPostion;
        int cheсkingHorizontalPosition = horizontalPosition + deltaHorizontalPosition;
        Cell cellThisEnemy = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
        Checker checkerOnCell = CheckerOnConreteCell(cellThisEnemy);
        if (checkerOnCell != null)
        {
            if (checkerOnCell.GetCheckerColor() == enemyColor) //22 08 враг
            {
                cheсkingVerticalPosition = verticalPosition + 2 * deltaVerticalPostion;
                cheсkingHorizontalPosition = horizontalPosition + 2 * deltaHorizontalPosition;
                Cell cell = _board.GetCellData(cheсkingVerticalPosition, cheсkingHorizontalPosition);
                if (CheckerOnConreteCell(cell) == null)
                {
                    cell.SetMoveable();
                    cell.AddCellToPurifyList(cellThisEnemy);
                }
            }
        }
    }

    private Checker CheckerOnConreteCell(Cell cell)
    {
        //22 08 обращение к boardData по координатам
        Checker checker = cell.GetCheckerOnCell();
        return checker;
    }


    void TryToAction()
    {
        if (_currentCell2d.GetCellData().IsMoveable())
        {
            TransitToCell();
        }
    }

    private void TransitToCell()
    {
        _checkerOncell.MoveToNewPosition(_currentCell.GetVerticalPostion(), _currentCell.GetHorizontalPostion());
        ClearPreviousChoused();
    }

    void UnchouseAllCells()
    {
        EventsManager.UnchouseAllCellsEvent?.Invoke();
    }

    void ClearPreviousChoused()
    {
        _chousedCell2d = null;
    }
}
