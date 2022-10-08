using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    private int [,] matrix;
    private int result;
    private int player;
    private int cnt;
    private static int Bwidth = 150;

    void OnGUI()
    {   
        int w = Screen.width / 2;
        GUI.Box(new Rect(w - 225, 50, 450, 500), "井字棋");
        if(GUI.Button(new Rect(w - 325, 75, 100, 25), "重新开始")) Reset();
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                if(matrix[i, j] == 1) GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "X");
                else if(matrix[i, j] == 2) GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "O");
                else if(result != 0) GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "");
                else {
                    if(GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "")) {
                        matrix[i, j] = 1 + player % 2;
                        result = Check();
                        player++;
                        cnt++;
                    }
                }
            }
        }
        if(result == 1) {
            GUI.Box(new Rect(w - 325, 50, 100, 25), "玩家1(X)获胜");
        }
        else if(result == 2) {
            GUI.Box(new Rect(w - 325, 50, 100, 25), "玩家2(O)获胜");
        }
        else if(cnt == 9) {
            GUI.Box(new Rect(w - 325, 50, 100, 25), "平局");
        }
        else {
            GUI.Box(new Rect(w - 325, 50, 100, 25), "正在游戏");
        }
    }

    void Reset()
    {
        matrix = new int [3,3]{
			{0,0,0},
			{0,0,0},
			{0,0,0}
		};
        result = 0;
        player = 0;
        cnt = 0;
    }
    int Check()
    {
        for(int i = 0; i < 3; i++) {
			if(matrix[i,0] == matrix[i,1] && matrix[i,1]== matrix[i,2] && matrix[i,0] != 0) return matrix[i,0];
			if(matrix[0,i] == matrix[1,i] && matrix[1,i]== matrix[2,i] && matrix[0,i] != 0) return matrix[0,i];
		}
		if(matrix[0,0] == matrix[1,1] && matrix[1,1] ==matrix[2,2]) return matrix[1,1];
		if(matrix[0,2] == matrix[1,1] && matrix[1,1] ==matrix[2,0]) return matrix[1,1];
		return 0;
    }

    void Start()
    {
		Reset();
    }
    void Update()
    {
        
    }
}