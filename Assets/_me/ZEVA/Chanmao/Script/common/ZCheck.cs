using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 2d碰撞体检测
    /// </summary>
    /// <param name="pos">射线的开始坐标</param>
    /// <param name="direction">方向</param>
    /// <param name="distance">距离</param>
    /// <param name="tag">要检测物体的标签</param>
    /// <returns></returns>
    public bool Collider2DCheck(Vector2 pos, Vector2 direction, float distance, string tag)
    {

        RaycastHit2D[] hit = Physics2D.RaycastAll(pos, direction, distance);
        foreach (RaycastHit2D e in hit)
        {
            if (e.collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 检测是否有碰撞体
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public bool Collider2DCheck(Vector2 pos, Vector2 direction, float distance)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(pos, direction, distance);
        foreach (RaycastHit2D e in hit)
        {
            if (e.collider)
            {
                return true;
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
