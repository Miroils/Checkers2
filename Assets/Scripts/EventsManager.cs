using UnityEngine;
using UnityEngine.Events;

public class EventsManager
{
    public class Cell2dEvent : UnityEvent<Cell2D> { }
    public class CellEvent : UnityEvent<Cell> { }
    public class CheckerEvent : UnityEvent<Checker> { }
    public class IntIntEvent : UnityEvent<int, int> { }
    public static UnityEvent BoardIsGeneratedEvent { get; set; } = new UnityEvent();
    public static UnityEvent RightClickEvent { get; set; } = new UnityEvent();
    public static Cell2dEvent LeftClickOnCellEvent { get; set; } = new Cell2dEvent();
    public static UnityEvent UnchouseAllCellsEvent { get; set; } = new UnityEvent();
    public static IntIntEvent ClearingCellEvent { get; set; } = new IntIntEvent();
    public static CheckerEvent TransitCheckerEvent { get; set; } = new CheckerEvent();
    public static CheckerEvent AnimateCheckerTranstionEvent { get; set; } = new CheckerEvent();
    public static UnityEvent ResetCellsMoveableEvent { get; set; } = new UnityEvent();
    public static UnityEvent ResetCellsPurifyEvent { get; set; } = new UnityEvent();
    public static CellEvent CellPurifyEvent { get; set; } = new CellEvent();
    public static CheckerEvent DestroyCheckerEvent { get; set; } = new CheckerEvent();
}
