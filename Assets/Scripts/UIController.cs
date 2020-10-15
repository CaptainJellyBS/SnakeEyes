using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] lives;
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
}
