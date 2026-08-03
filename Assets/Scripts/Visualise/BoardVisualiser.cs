using UnityEngine;

public class BoardVisualiser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.BoardIsGeneratedEvent.AddListener(VisualiseBoard);   
    }

    private void VisualiseBoard()
    {
        //03 08 взять из Board
    }

    private void VisualiseCheckers()
    {

    }
}
