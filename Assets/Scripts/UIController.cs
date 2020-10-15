using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] lives;
    public GameObject deathPanel;
    public static UIController Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public void SetLives(int lifeAmount)
    {
        for (int i = 0; i<lives.Length; i++)
        {
            lives[i].SetActive(i<lifeAmount);
        }
    }

    public void Die()
    {
        Time.timeScale = 0.0f;
        deathPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f; //Make sure time is flowing again when restarting the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
