using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private void Update()
    {
        Cell2D currentCell = GenerateRayCast();
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

    private void LeftClickOnCell(Cell2D cell2D)
    {
        //20 08 может принимать не cell2d, a cell?
        //19 08 вызываем евент, что кликнули на некий селл и отрабатываем если можем
        EventsManager.LeftClickOnCellEvent?.Invoke(cell2D);
    }

    private void RightClick()
    {
        EventsManager.RightClickEvent?.Invoke();
    }

    private Cell2D GenerateRayCast()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit)
        {            
            return hit.collider.gameObject.GetComponent<Cell2D>();
        }
        return null;
    }
}
