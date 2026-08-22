using UnityEngine;
using UnityEngine.Events;

public class EventsManager
{
    public class CellEvent : UnityEvent<Cell2D> { }
    public class CheckerEvent : UnityEvent<Checker> { }
    public class IntIntEvent : UnityEvent<int, int> { }
    public static UnityEvent BoardIsGeneratedEvent { get; set; } = new UnityEvent();
    public static UnityEvent RightClickEvent { get; set; } = new UnityEvent();
    public static CellEvent LeftClickOnCellEvent { get; set; } = new CellEvent();
    public static UnityEvent UnchouseAllCellsEvent { get; set; } = new UnityEvent();
    public static IntIntEvent ClearingCellEvent { get; set; } = new IntIntEvent();
    public static CheckerEvent TransitCheckerEvent { get; set; } = new CheckerEvent();
    public static CheckerEvent AnimateCheckerTranstion { get; set; } = new CheckerEvent();
}
