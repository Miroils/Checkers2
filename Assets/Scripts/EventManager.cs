using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent BoardIsGeneratedEvent { get; set; } = new UnityEvent();
}
