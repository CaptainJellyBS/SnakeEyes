using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public static GameController Instance { get; private set; }
    public Color[] availableColors;

    private void Awake()
    {
        Instance = this;
    }

    public Color GetRandomColor()
    {
        return availableColors[Random.Range(0, availableColors.Length)];
    }

}
