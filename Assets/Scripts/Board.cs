using UnityEngine;
using Zenject;

public class Board : MonoBehaviour
{
    private BoardData _boardData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestMetod()
    {
        
    }

    public void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }

    public BoardData GetBoardData()
    {
        return _boardData;
    }
}
