using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {
    private Animator anima;
    public GameObject button;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// 点击pause按钮
    /// </summary>
    public void Pause()
    {
        
        anima.SetBool("isPause",true);
        button.SetActive(false);
    }


    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// 点击了继续按钮
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        anima.SetBool("isPause",false);
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()
    {
        button.SetActive(true);
    }
}
