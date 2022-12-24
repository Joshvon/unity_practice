using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void reset()
    {
        score = 0;
    }
    void addScore()
    {
        score++;
    }
    public int getScore()
    {
        return score;
    }
    public void OnEnable()
    {
        GameEventManager.scoreChange += addScore;
    }
    public void OnDisable()
    {
        GameEventManager.scoreChange -= addScore;
    }
}
