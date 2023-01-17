using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField]
    private Text KillCountText;
    private int KillCount;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    public void EnemyKilled() 
    {
        KillCount++;
        KillCountText.text = "Kills: " + KillCount;
    }
}