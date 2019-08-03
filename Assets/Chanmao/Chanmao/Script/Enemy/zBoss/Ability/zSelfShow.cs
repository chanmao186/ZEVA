using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zSelfShow : MonoBehaviour
{
    public ZController zc;
    public GameObject Shade;
    public GameObject Boss;

    public GameObject Boss1;
    public zPlayer Player;

    private Animator Ani;

    public CanmeraMove _camera;
    //private Color Target;
    // Start is called before the first frame update
    void Start()
    {
        
        Ani = GetComponent<Animator>();
        Player = GameObject.FindObjectOfType<zPlayer>();
        //Target = Boss.GetComponent<SpriteRenderer>().color;
        //Boss.SetActive(false);
        zc = GameObject.FindObjectOfType<ZController>();
        Boss1.SetActive(false);
        
    }

    public void BossShow()
    {
        _camera.target = transform;
        Player.ResetAni();
        Player.SetOperationState(false);
        zc.zTime.ScheduleOnce(() => {
            GetComponent<zSelf_Shade>().enabled = false;

            //确保影子走路的方向正确
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            
            //播放走路的动画
            Ani.SetFloat("Speed", 0.5f);
            zc.zMove.MoveToNode(Boss.transform, Player.WalkSpeed, () => {

                //停止播放走路的动画
                Ani.SetFloat("Speed", 0);
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;

                //开始颜色渐变
                zc.zColor.ColorOut(Shade.GetComponent<SpriteRenderer>(), 3, Boss.GetComponent<SpriteRenderer>().color, () => {

                    //销毁Boss,启动控制
                    Boss1.SetActive(true);
                    _camera.target = Boss.transform;
                    //Player.SetOperationState(true);

                    zc.zTime.ScheduleOnce(() => {
                        _camera.target = Player.transform;
                        Player.SetOperationState(true);
                    }, 1);
                    //销毁该节点
                    Destroy(gameObject);
                });
            }, transform);
        }, 2);
    }
    // Update is called once per frame
}
