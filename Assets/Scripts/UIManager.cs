using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _nextActionButton;
    [SerializeField] private Button _previousActionButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _nextActionButton.onClick.AddListener(NextButtonPressed);
        _previousActionButton.onClick.AddListener(PreviousButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
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
