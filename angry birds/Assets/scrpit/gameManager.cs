using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

    public List<bird> birds;
    public List<Pig> pig;
    public static gameManager _instance;
    private Vector3 oriGinPos;

    public GameObject Win;
    public GameObject Lose;

    public GameObject[] stars;

    private void Awake()
    {
        _instance = this;
        if(birds.Count>0)
        oriGinPos = birds[0].transform.position;
    }
    private void Start()
    {
        Initialized();

    }
    public void Initialized()
    {
        for( int i=0;i<birds.Count;i++)
        {
            if(i==0)
            {
                birds[i].transform.position= oriGinPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

   public void Nextbird()
    {
        if(pig.Count>0)
        {
            if(birds.Count>0)
            {
                Initialized(); //下一只飞出
            }
            else
            {
                Lose.SetActive(true);//输了
            }
        }
        else
        {
            Win.SetActive(true);//win
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for(int i=0;i<birds.Count+1;i++)
        {
            yield return new WaitForSeconds(0.2f);
            stars[i].SetActive(true);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }
}
