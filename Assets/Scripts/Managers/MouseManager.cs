using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private void Update()
    {
        Cell currentCell = GenerateRayCast();
        if (Input.GetMouseButtonDown(0))
        {
            if (currentCell != null)
            {
                LeftClickOnCell(currentCell);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RightClick();
        }
     }

    private void LeftClickOnCell(Cell cell)
    {
        EventsManager.LeftClickOnCellEvent?.Invoke(cell);
    }

    private void RightClick()
    {
        EventsManager.RightClickEvent?.Invoke();
    }

    private Cell GenerateRayCast()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit)
        {            
            return hit.collider.gameObject.GetComponent<Cell2D>().GetCellData();
        }
        return null;
    }
}
