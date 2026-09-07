using UnityEngine.Events;

public class EventsManager
{
    public class CellEvent : UnityEvent<Cell> { }
    public class CheckerEvent : UnityEvent<Checker> { }
    public class IntIntEvent : UnityEvent<int, int> { }
    public static UnityEvent BoardIsGeneratedEvent { get; set; } = new UnityEvent();
    public static UnityEvent BoardDataSetEvent { get; set; } = new UnityEvent();
    public static UnityEvent RightClickEvent { get; set; } = new UnityEvent();
    public static CellEvent LeftClickOnCellEvent { get; set; } = new CellEvent();
    public static UnityEvent UnchouseAllCellsEvent { get; set; } = new UnityEvent();
    public static IntIntEvent ClearingCellEvent { get; set; } = new IntIntEvent();
    public static CheckerEvent TransitCheckerEvent { get; set; } = new CheckerEvent();
    public static CheckerEvent AnimateCheckerTranstionEvent { get; set; } = new CheckerEvent();
    public static UnityEvent ResetCellsMoveableEvent { get; set; } = new UnityEvent();
    public static UnityEvent ResetCellsPurifyEvent { get; set; } = new UnityEvent();
    public static CellEvent CellPurifyEvent { get; set; } = new CellEvent();
    public static CheckerEvent DestroyCheckerEvent { get; set; } = new CheckerEvent();
    public static UnityEvent NextActionEvent { get; set; } = new UnityEvent();
    public static UnityEvent PreviousActionEvent { get; set; } = new UnityEvent();
    public static CheckerEvent SetCheckerOnCellEvent { get; set; } = new CheckerEvent();
    public static CheckerEvent FindMovesEvent { get; set; } = new CheckerEvent();
}
