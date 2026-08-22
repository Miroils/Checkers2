using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        //19 08 омтеняем выделение
        EventsManager.RightClickEvent?.Invoke();
    }

    private Cell2D GenerateRayCast()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));//Physics2D.Raycast(transform.position, Vector2.right);
        if (hit)
        {            
            return hit.collider.gameObject.GetComponent<Cell2D>();//19 08 проверять на комопнент прежде чем его возвращать? но нет объектов которые с колаедром но без Cell2D
        }
        return null;
    }
}
