using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public delegate void checkGameOver();
    public static event checkGameOver gameover;
    public delegate void ScoreChange();
    public static event ScoreChange scoreChange;
    public delegate void ReduceTreasure();
    public static event ReduceTreasure treasureChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setGameOver()
    {
        if(gameover != null) {
            gameover();
        }
    }

    public void addScore()
    {
        if(scoreChange != null) {
            scoreChange();
        }
    }

    public void reduceTreasure()
    {
        if(treasureChange != null) {
            treasureChange();
        }
    }
}
