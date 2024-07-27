using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score include;
    public int ScoreMax, ScoreTotal, Elite, Grunt;
    public TMP_Text _scoreMax, _scoreMaxGame, _scoreTotal, _elite, _grunt;

    private void Awake()
    {
        if (include != null && include != this)
        {
            Destroy(gameObject);
            return;
        }
        include = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTotal = Elite + Grunt;

        _scoreMax.text = ScoreMax.ToString();
        _scoreMaxGame.text = ScoreMax.ToString();
        _scoreTotal.text = ScoreTotal.ToString();
        _elite.text = Elite.ToString();
        _grunt.text = Grunt.ToString();
    }
}
