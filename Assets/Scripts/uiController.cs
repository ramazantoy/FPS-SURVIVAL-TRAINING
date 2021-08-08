using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiController : MonoBehaviour
{
    gun g;
    public GameObject pause_pnl;
    void Start()
    {
        g = GameObject.FindObjectOfType<gun>();
    }
    public void Fire_btn()
    {
        g.firee();

    }
    public void Reload_btn()
    {
        g.reload();
    }
    public void pause_btn()
    {
        pause_pnl.SetActive(true);
        Time.timeScale = 0.0f;

    }
    public void resumePausePnl()
    {
        Time.timeScale = 1.0f;
        pause_pnl.SetActive(false);
    }
    public void exitPausePnl()
    {
        Application.Quit();
    }
    public void nextLevelBtn()
    {
        Time.timeScale = 1.0f;
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }
    public void playAgainBtn()
    {
        Time.timeScale = 1.0f;
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);

    }
}
