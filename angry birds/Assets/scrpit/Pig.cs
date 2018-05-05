using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite Hurt;
    public GameObject boom;
    public GameObject score;
    public bool isPig=false;

    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdcollision;

    public void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);

        if(collision.gameObject.tag=="Player")
        {
            AudioPlay(birdcollision);
        }
        if(collision.relativeVelocity.magnitude>maxSpeed)//直接死亡
        {
            Dead();
        }
        else if(collision.relativeVelocity.magnitude<maxSpeed&&collision.relativeVelocity.magnitude>minSpeed)
         {
            render.sprite = Hurt;
            AudioPlay(hurtClip);
        }
    }
    void Dead()
    {
        if(isPig)
        {
            gameManager._instance.pig.Remove(this);
        }
       
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject go = Instantiate(score, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(go, 1.5f);
        AudioPlay(dead);//播放音乐
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
