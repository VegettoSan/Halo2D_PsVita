using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rounds : MonoBehaviour
{
    public Spawner Elite, EliteSword, Grunt;
    //public int Round = 1;
    public TMP_Text Counter;
    //public Animator Anim;
    Score EnemyTotal;
    void Start()
    {
        EnemyTotal = GetComponent<Score>();
        Elite.gameObject.SetActive(false);
        EliteSword.gameObject.SetActive(false);
    }
    void Update()
    {
        //Counter.text = Round.ToString();

        if (EnemyTotal.ScoreTotal == 3)
        {
            Grunt.Health = 100;
        }

        if (EnemyTotal.ScoreTotal == 5)
        {
            Grunt.Health = 110;
        }

        if (EnemyTotal.ScoreTotal == 8)
        {
            Elite.gameObject.SetActive(true);
            Grunt.Health = 120;
        }
        if (EnemyTotal.ScoreTotal == 12)
        {
            Elite.Health = 170;
            Grunt.Health = 130;
        }
        if (EnemyTotal.ScoreTotal == 15)
        {
            Elite.MaxBots = 3;
            Grunt.MaxBots = 4;
        }
        if (EnemyTotal.ScoreTotal == 18)
        {
            Elite.Health = 190;
            Grunt.Health = 150;
        }
        if (EnemyTotal.ScoreTotal == 21)
        {
            Elite.MaxBots = 4;
            Grunt.MaxBots = 5;
            Elite.Tiempo = 10;
            Grunt.Tiempo = 6;
        }
        if (EnemyTotal.ScoreTotal == 28)
        {
            Elite.Health = 210;
            Grunt.Health = 170;
        }
        if (EnemyTotal.ScoreTotal == 30)
        {
            Elite.MaxBots = 5;
            Grunt.MaxBots = 6;
        }
        if (EnemyTotal.ScoreTotal == 32)
        {
            Elite.Health = 230;
            Grunt.Health = 190;
        }
        if (EnemyTotal.ScoreTotal == 34)
        {
            Elite.Tiempo = 8;
            Grunt.Tiempo = 5;
        }
        if (EnemyTotal.ScoreTotal == 39)
        {
            Elite.Health = 250;
            Grunt.Health = 110;
        }
        if (EnemyTotal.ScoreTotal == 42)
        {
            Elite.MaxBots = 7;
            Grunt.MaxBots = 8;
        }
        if (EnemyTotal.ScoreTotal == 50)
        {
            EliteSword.gameObject.SetActive(true);
            Elite.Tiempo = 6;
            Grunt.Tiempo = 4;
        }
        if (EnemyTotal.ScoreTotal == 52)
        {
            Elite.MaxBots = 9;
            Grunt.MaxBots = 11;
        }
        if (EnemyTotal.ScoreTotal == 60)
        {
            Elite.MaxBots = 12;
            Grunt.MaxBots = 15;
        }
        if (EnemyTotal.ScoreTotal == 72)
        {
            Elite.MaxBots = 15;
            EliteSword.MaxBots = 4;
            Grunt.MaxBots = 18;
        }
    }

    /*void EnableText()
    {
        Anim.SetTrigger("Enable");
    }*/
}
