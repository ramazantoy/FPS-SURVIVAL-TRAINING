using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healtBar;
    float maxHp = 100;
    float nowHp = 100;
    public GameObject gameover;

    public void healtChange(float hp)
    {
        nowHp += hp;
        healtBar.fillAmount = nowHp / maxHp;
        if (nowHp <= 0.0f)
        {
            death();
        }
    }
    void death()
    {
        gameover.SetActive(true);
        Time.timeScale = 0.0f;
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
