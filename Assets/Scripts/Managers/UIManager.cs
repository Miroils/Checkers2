using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _nextActionButton;
    [SerializeField] private Button _previousActionButton;

    private void Start()
    {
        _nextActionButton.onClick.AddListener(NextButtonPressed);
        _previousActionButton.onClick.AddListener(PreviousButtonPressed);
    }

    private void NextButtonPressed()
    {
        EventsManager.NextActionEvent?.Invoke();
    }
    private void PreviousButtonPressed()
    {
        EventsManager.PreviousActionEvent?.Invoke();
    }
}
