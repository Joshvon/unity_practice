using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using baseCode;
public class UserGUI : MonoBehaviour
{
    roundController roundCtrl;
    bool ifShowWin = false;
    bool ifShowRule = false;

    // Start is called before the first frame update
    void Start()
    {
        roundCtrl = (roundController)Director.getInstance ().currentSceneController;
    }

    // Update is called once per frame
    void Update()
    {
        if (roundCtrl.status == "gameover" || roundCtrl.status == "pause") {
            return;
        }
        checkClick();
    }

    void OnGUI()
    {
        string showButtonText;
        GUI.Box (new Rect (15, 15, 120, 50) ,"");
        GUI.Label (new Rect (15, 15, 120, 25), "status: " + roundCtrl.status);
        GUI.Label (new Rect (15, 40, 120, 25), "score: " + roundCtrl.scoreCtrl.getScore());
        if (roundCtrl.status == "running") {
            showButtonText = "pause";
        } 
        else if(roundCtrl.status == "gameover") {
            GUI.Box(new Rect(Screen.width / 2 - 225, 50, 100, 25), "GAME OVER!");
            showButtonText = "start";
        }
        else {
            showButtonText = "go on";
        }

        if (GUI.Button (new Rect(15, 70, 120, 30), showButtonText)) {
            if (showButtonText == "go on") {
                roundCtrl.status = "running";
            }
            else if (showButtonText == "start") {
                roundCtrl.status = "running";
                roundCtrl.reset();
            }
            else {
                roundCtrl.status = "pause";
            }
        }

        if (GUI.Button(new Rect (15, 110, 120, 30), "reset")) {
            roundCtrl.status = "running";
            roundCtrl.reset();
        }

        if (GUI.Button(new Rect(15, 150, 120, 30), "rule")) {
			if (ifShowRule)
				ifShowRule = false;
			else
				ifShowRule = true;
		}
		if(ifShowRule) {
			GUI.Label(new Rect(Screen.width / 2 - 85, 10, 400, 50), "打飞碟游戏，通过鼠标点击飞碟得分");
			GUI.Label(new Rect(Screen.width / 2 - 85, 30, 400, 50), "击中红色飞碟得1分，击中黄色飞碟得2分，集中蓝色飞碟得3分");
			GUI.Label(new Rect(Screen.width / 2 - 85, 50, 350, 50), "游戏会出现三轮，每一轮出现十个飞碟");
			GUI.Label(new Rect(Screen.width / 2 - 85, 70, 450, 50), "第一轮只会出现红色飞碟，第二轮增加黄色飞碟，第三轮再增加蓝色飞碟");
            GUI.Label(new Rect(Screen.width / 2 - 85, 90, 350, 50), "享受游戏!");
		}
    }

    void checkClick()
    {
        if (Input.GetButtonDown("Fire1")) {
            Vector3 mp = Input.mousePosition;
            Camera ca = Camera.main;
            Ray ray = ca.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                ((roundController)Director.getInstance().currentSceneController).hitUFO(hit.transform.gameObject);
            }
        }
    }
}
