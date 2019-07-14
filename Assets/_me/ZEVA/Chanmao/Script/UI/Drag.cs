using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public Camera cam;
    //被拖动的目标物体
    private Transform DragNode;
    private Vector2 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        
        //被拖动的物体默认为空
        DragNode = null;
    }

    private void OnMouseDrag()
    {
        Debug.Log("哈哈");
        currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //currentPos.x = Input.mousePosition.x;
        //currentPos.y = Input.mousePosition.y;
        transform.position = currentPos;

    }
    private void cDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (hit)
            {
                if (hit.collider.gameObject.tag.Equals("DragNode"))
                {

                    DragNode = hit.collider.transform;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DragNode = null;
        }


    }
    // Update is called once per frame
    void Update()
    {
        //cDrag();
    }
}
