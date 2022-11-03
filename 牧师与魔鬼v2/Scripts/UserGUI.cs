using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using conInterface;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
	public int sign = 0;

	bool isShow = false;

    // Start is called before the first frame update
	void Start()
	{
		action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
	}
	void OnGUI()
	{
		GUIStyle text_style;
		GUIStyle button_style;
		text_style = new GUIStyle() {
			fontSize = 30
		};
		button_style = new GUIStyle("button") {
			fontSize = 15
		};
		if (GUI.Button(new Rect(10, 10, 60, 30), "规则", button_style)) {
			if (isShow)
				isShow = false;
			else
				isShow = true;
		}
		if(isShow) {
			GUI.Label(new Rect(Screen.width / 2 - 85, 10, 200, 50), "红黄色的为牧师，暗蓝色的为恶魔");
			GUI.Label(new Rect(Screen.width / 2 - 85, 30, 200, 50), "让全部牧师和恶魔都渡河");
			GUI.Label(new Rect(Screen.width / 2 - 85, 50, 250, 50), "每一边恶魔数量都不能多于牧师数量");
			GUI.Label(new Rect(Screen.width / 2 - 85, 70, 250, 50), "点击牧师、恶魔、船移动");
		}
		if (sign == 1) {
			GUI.Label(new Rect(Screen.width / 2-90, Screen.height / 2-120, 100, 50), "游戏结束!", text_style);
			if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 100, 50), "重新开始", button_style))
			{
				action.Restart();
				sign = 0;
			}
		}
		else if (sign == 2) {
			GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 120, 100, 50), "获胜!", text_style);
			if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 100, 50), "重新开始", button_style))
			{
				action.Restart();
				sign = 0;
			}
		}
	}
}
