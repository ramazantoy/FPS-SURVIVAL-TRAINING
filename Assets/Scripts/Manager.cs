using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject wellPanel;
    public void wellDone()
    {
        wellPanel.SetActive(true);
        Time.timeScale = 0.0f;

    }
}
