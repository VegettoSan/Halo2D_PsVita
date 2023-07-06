using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TimeCounter Timer;

    public bool StartCounter;
    public GameObject Player;

    [Serializable]
    public class Map
    {
        public GameObject[] Maps;
    }

    [Header("Maps")]
    [SerializeField] Map Maps;

    [Serializable]
    public class Position
    {
        public Transform[] Positions;
    }

    [Header("Player Position")]
    [SerializeField] Position PositionPlayer;

    void Start()
    {
        Time.timeScale = 1;
        Timer = GetComponent<TimeCounter>();
        RandomMap();
        if (StartCounter)
        {
            CounterStart();
        }
        else if(!StartCounter)
        {
            CounterStop();
        }
        RandomPlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.tag == "null")
        {
            CounterStop();
        }
        else if (Player.tag != "null")
        {
            CounterStart();
        }
    }

    void CounterStart()
    {
        Timer.StartCounter = true;
    }
    void CounterStop()
    {
        Timer.StartCounter = false;
    }

    void RandomMap()
    {
        int i = UnityEngine.Random.Range(0, Maps.Maps.Length);
        Maps.Maps[i].SetActive(true);
    }

    public void RandomPlayerPosition()
    {
        int i = UnityEngine.Random.Range(0, PositionPlayer.Positions.Length);
        Player.transform.position = PositionPlayer.Positions[i].position;
    }
}
