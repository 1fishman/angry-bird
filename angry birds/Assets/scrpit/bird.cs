using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour {
    private bool isClick=false;
    public Transform rightPosition;
    public float maxDis = 1.5F;
    [HideInInspector]
    public SpringJoint2D sp;
    private Rigidbody2D rd;
    private TestMytrail myTrail;


    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;

    private bool canMove=true;

    public float smooth = 3;

    public AudioClip select;
    public AudioClip fly;

    private void Awake()
    {
        sp=GetComponent<SpringJoint2D>();
        rd = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMytrail>();
    }
    /// <summary>
    /// 鼠标按下
    /// </summary>
    private void OnMouseDown()
    {
        if(canMove)
        {
            AudioPlay(select);
        isClick = true;
        rd.isKinematic = true;
        }
    }
    /// <summary>
    /// 鼠标抬起
    /// </summary>
    private void OnMouseUp()
    {
        if(canMove)
        {
        isClick = false;
        rd.isKinematic=false;
        Invoke("Fly", 0.2f);
        //禁用画线
        right.enabled = false;
        left.enabled = false;
            canMove = false;
        }


    }
    private void Update()
    {
        if (isClick)//鼠标按下一直跟随
        {
            transform.position =Camera.main.ScreenToWorldPoint( Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);

            if(Vector3.Distance(transform.position,rightPosition.position)>maxDis)//进行位置限定
            {
                Vector3 pos = (transform.position - rightPosition.transform.position).normalized;//向量单位化
                pos *= maxDis;
                transform.position = rightPosition.transform.position + pos;
            }
            Line();
        }

        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Mathf.Clamp(posX,0,11), Camera.main.transform.position.y,
                Camera.main.transform.position.z),smooth*Time.deltaTime);
    }
    void Fly()
    {
        AudioPlay(fly);
        myTrail.StartTrails();
        sp.enabled = false;
        Invoke("Next", 5f);
    }

    void Line()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, leftPos.position);
    }
    /// <summary>
    ///下一只小鸟的飞出
    /// </summary>
    public void Next()
    {
        gameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position,Quaternion.identity);
        gameManager._instance.Nextbird();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        myTrail.ClearTrails();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
