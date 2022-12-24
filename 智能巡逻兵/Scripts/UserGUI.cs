using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private FirstController ctrl;
    private bool ifShowRule = false;
    private GUIStyle textStyle = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {
        ctrl = (FirstController)Director.getInstance().currentSceneController;
        textStyle.normal.textColor = Color.red;
        textStyle.fontSize = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //获取方向键的偏移量
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        //移动玩家
        ctrl.MovePlayer(translationX, translationZ);
    }
    void OnGUI()
    {
        GUI.Label (new Rect (15, 15, 120, 25), "score: " + ctrl.GetScore());
        if(ctrl.GetGameover()) {
            GUI.Box(new Rect(15, 50, 100, 25), "GAME OVER!", textStyle);
        }
        if(GUI.Button(new Rect(15, 65, 120, 30), "reset")) {
            ctrl.Reset();
        }
        if (GUI.Button(new Rect(15, 95, 120, 30), "rule")) {
			if (ifShowRule)
				ifShowRule = false;
			else
				ifShowRule = true;
		}
		if(ifShowRule) {
			GUI.Label(new Rect(15, 140, 400, 50), "躲避巡逻兵", textStyle);
			GUI.Label(new Rect(15, 160, 400, 50), "取得红色的宝藏", textStyle);
			GUI.Label(new Rect(15, 180, 350, 50), "取得所有宝藏游戏胜利", textStyle);
            GUI.Label(new Rect(15, 200, 350, 50), "被巡逻兵抓到游戏失败", textStyle);
            GUI.Label(new Rect(15, 220, 350, 50), "享受游戏!", textStyle);
		}
    }
}
