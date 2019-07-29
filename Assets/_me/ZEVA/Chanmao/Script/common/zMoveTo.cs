using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMoveTo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    /// <summary>
    /// 将物体移动至目标节点的位置
    /// </summary>
    /// <param name="targetNode">目标节点</param>
    /// <param name="Node">要移动的节点</param>
    public Coroutine MoveToNode(Transform targetNode, Transform Node)
    {
        return MoveToPosition(targetNode.position, 1, Node);
    }

    /// <summary>
    /// 将物体移动至目标节点的位置
    /// </summary>
    /// <param name="targetNode">目标节点</param>
    /// <param name="moveSpeed">移动速度</param>
    /// <param name="Node">要移动的节点</param>
    public Coroutine MoveToNode(Transform targetNode, float moveSpeed, Transform Node)
    {
        return MoveToPosition(targetNode.position, moveSpeed, null, Node);
    }

    /// <summary>
    /// 将物体移动至目标节点的位置
    /// </summary>
    /// <param name="targetNode">目标节点</param>
    /// <param name="moveSpeed">移动速度</param>
    /// <param name="CallBack">回调函数</param>
    /// <param name="Node">要移动的节点</param>
    public Coroutine MoveToNode(Transform targetNode, float moveSpeed, Func CallBack, Transform Node)
    {
        return MoveToPosition(targetNode.position, moveSpeed, CallBack, Node);
    }


    /// <summary>
    /// 将物体移动到指定的位置
    /// </summary>
    /// <param name="pos">要移动的位置</param>
    /// <param name="Node">要移动的节点</param>
    public Coroutine MoveToPosition(Vector3 pos, Transform Node)
    {
        return MoveToPosition(pos, 1, Node);
    }

    /// <summary>
    /// 将物体移动到指定的位置
    /// </summary>
    /// <param name="pos">要移动的位置</param>
    /// <param name="moveSpeed">移动速度</param>
    /// <param name="Node">要移动的节点</param>
    public Coroutine MoveToPosition(Vector3 pos, float moveSpeed, Transform Node)
    {
        return MoveToPosition(pos, moveSpeed, null, Node);
    }   

    /// <summary>
    /// 将物体移动到指定的位置
    /// </summary>
    /// <param name="pos">要移动的位置</param>
    /// <param name="moveSpeed">移动速度</param>
    /// <param name="CallBack">回调函数</param>
    /// <param name="Node">要移动的节点</param>
    public Coroutine MoveToPosition(Vector3 pos, float moveSpeed, Func CallBack, Transform Node)
    {
        return StartCoroutine(MoveToPosition1(pos, moveSpeed, CallBack,Node));
    }

    IEnumerator MoveToPosition1(Vector3 pos,float moveSpeed,Func CallBack,Transform Node)
    {
        //Debug.Log(Node.gameObject.name);
        while (Vector3.Distance(Node.position,pos)>0.1)
        {
            //Debug.Log(transform.position);
            Node.position = Vector3.MoveTowards(Node.position, pos, moveSpeed * Time.deltaTime);
            yield return 0;
        }
        CallBack?.Invoke();
    }
    //public Coroutine JumpToPosition(Vector3 pos, float moveSpeed, Func CallBack, Transform Node)
    //{
    //    Vector3 d =pos - Node.position;
    //}
    //IEnumerator JumpToPosition1(Vector3 pos, float moveSpeed, Func CallBack, Transform Node,Vector3 origin,float h,float t)
    //{
    //    float x = pos.x - Node.position.x;
    //    float z = pos.z - Node.position.z;
    //    float dn = Vector2.Distance(new Vector2(pos.x,pos.z),new Vector2 (Node.position.x,Node.position.y));
    //    dn = (pos.x - Node.position.x) ^ 2;
    //    float dor = Vector2.Distance(pos, origin);
    //    //Debug.Log(Node.gameObject.name);
    //    while (Vector3.Distance(Node.position, pos) > 0.1)
    //    {
    //        //Debug.Log(transform.position);
    //        Node.position = new Vector3(origin.x+d.x*(1-t), origin.y+h*Mathf.Sin(Mathf.PI*(1-dn/dor)), origin.z);
    //        yield return 0;
    //    }
    //    CallBack?.Invoke();
    //}

}
