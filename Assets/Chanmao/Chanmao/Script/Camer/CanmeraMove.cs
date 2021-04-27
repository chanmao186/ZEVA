using UnityEngine;

public class CanmeraMove : MonoBehaviour
{
    [Header("摄像机要跟踪的目标")]
    public Transform target;

    public float OffX = 0;
    
    public float OffY = 0;
    
    [Header("摄像机向左移动的最大距离")]
    public float Left;

    [Header("摄像机向右移动的最大距离")]
    public float Right;

    [Header("摄像机向下移动的最大距离")]
    public float Botton = 0f;

    [Header("摄像机向上移动的最大距离")]
    public float Weiling = 0.1f;
    private Vector2 targetPos;
    private Vector3 NewPos;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        targetPos = target.position;
        NewPos = new Vector3(0, 0, -10);
    }

    private void UpdateTarget()
    {
        if (target == null)
        {
            target = GameObject.FindObjectOfType<zPlayer>().transform;
        }
    }
    private void Move()
    {
        UpdateTarget();
        targetPos = target.position;
        targetPos.x += OffX;
        targetPos.y += OffY;
        //检测相机是否在游戏场景内
        if (targetPos.x <= Left) targetPos.x = Left;
        else if (targetPos.x >= Right) targetPos.x = Right;

        if (targetPos.y >= Weiling) targetPos.y = Weiling;
        else if (targetPos.y <= Botton) targetPos.y = Botton;
        //在生命一个变量的原因是要让相机的z坐标保持在-10;
        NewPos.x = targetPos.x;
        NewPos.y = targetPos.y;
        transform.position = NewPos;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
