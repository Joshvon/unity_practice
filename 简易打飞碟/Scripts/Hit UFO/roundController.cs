using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using baseCode;
using UFO;
public class roundController : MonoBehaviour, sceneController, UserAction
{
    public string status;
    UFOFactory ufofactory;
    int round = 1;
    int numOfUFOSend = 0;
    int score = 0;
    float time = 0;
    float sendUFOTime = 1;
    SSActionManager actionManager;
    public ScoreController scoreCtrl = new ScoreController();
    
    void Awake()
    {
        status = "pause";
        Director director = Director.getInstance();
        director.currentSceneController = this;
        this.gameObject.AddComponent<UFOFactory>();
        this.gameObject.AddComponent<SSActionManager>();
        this.gameObject.AddComponent<UserGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ufofactory = Singleton<UFOFactory>.Instance;
        actionManager = Singleton<SSActionManager>.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "pause" || status == "gameover") {
            return;
        } 
        else if (round > 3) {
            status = "gameover";
            return;
        }
        else if (numOfUFOSend >= 10) {
            numOfUFOSend = 0;
            round++;
        }
        time += Time.deltaTime;
        checkIfSendUFO();
    }

    public void hitUFO(GameObject ufo)
    {
        UFOInfo temp = ufofactory.getUFO(ufo);
        if(temp == null) {
            Debug.Log ("the UFO of clicked is null");
        }
        else {
            scoreCtrl.addScore(temp.level);
            ufofactory.freeUFO(temp);
        }
    }

    private void checkIfSendUFO()
    {
        if(time > sendUFOTime) {
            float randomNumOfSend = Random.Range(0f, round);
            int num;
            if(randomNumOfSend <= 1) {
                num = 1;
            }
            else if (randomNumOfSend <= 2) {
                num = 2;
            }
            else {
                num = 3;
            }
            sendUFO(num);
            time = 0;
        }
    }
    
    private void sendUFO(int n)
    {
        for(int i = 0; i < n; i++) {
            float randomLevelOfSend = Random.Range(0f, round);
            int level;
            if(randomLevelOfSend <= 1) {
                level = 1;
            }
            else if(randomLevelOfSend <= 2) {
                level = 2;
            }
            else {
                level = 3;
            }
            numOfUFOSend++;
            UFOInfo ufoInfo = ufofactory.getUFO(level);
            UFOAction ac = UFOAction.getUFOAction(ufoInfo, level);
            actionManager.RunAction(ufoInfo.ufo, ac, null);
        }
    }

    public void reset()
    {
        actionManager.reset();
        ufofactory.reset();
        scoreCtrl.reset();
        time = 0;
        sendUFOTime = 1;
        round = 1;
        numOfUFOSend = 0;
        status = "pause";
    }

    public void loadResources()
    {

    }
}
