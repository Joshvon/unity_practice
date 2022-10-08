#### 前言
中山大学软件工程学院 3D游戏编程与设计课程学习记录博客
游戏代码: [游戏代码](https://github.com/Joshvon/unity_practice)
游戏演示视频: [演示视频](https://www.bilibili.com/video/BV1GW4y1H7My/)

#### 编程内容
+ 了解OnGUI()事件，提升debug能力
+ 提升阅读API文档能力
+ 训练数据-控制分离的编程思想
+ 仅使用IMGUI构建UI编写井字棋小游戏
#### 实现思路
+ 确定屏幕中心位置
```csharp
    int w = Screen.width / 2;
```
+ 放置**游戏名称**和**重新开始**按钮
```csharp
    GUI.Box(new Rect(w - 225, 50, 450, 500), "井字棋");
    if(GUI.Button(new Rect(w - 325, 75, 100, 25), "重新开始")) Reset();
```
+ 根据一个二维数组`matrix`来确定玩家**下棋位置**及**下棋功能**
```csharp
    for(int i = 0; i < 3; i++) {
        for(int j = 0; j < 3; j++) {
            if(matrix[i, j] == 1) GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "X");
            else if(matrix[i, j] == 2) GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "O");
            //当游戏结束时点击button无效果
            else if(result != 0) GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "");  
            else {
                if(GUI.Button(new Rect(w - 225 + j*Bwidth, 100 + i*Bwidth, Bwidth, Bwidth), "")) {
                    matrix[i, j] = 1 + player % 2;
                    result = Check();
                    player++;       //辅助计算当前回合下棋的玩家
                    cnt++;
                }
            }
        }
    }
```
+ 根据下的棋子数量`cnt`和`Check()`函数返回的结果来判断游戏结果
```csharp
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
```
+ `Check()`函数先判断同行或同列是否达成获胜条件，再判断两条对角线是否达成获胜条件并返回获胜玩家编号；否则返回0
```csharp
    for(int i = 0; i < 3; i++) {
		if(matrix[i,0] == matrix[i,1] && matrix[i,1]== matrix[i,2] && matrix[i,0] != 0) return matrix[i,0];
		if(matrix[0,i] == matrix[1,i] && matrix[1,i]== matrix[2,i] && matrix[0,i] != 0) return matrix[0,i];
	}
	if(matrix[0,0] == matrix[1,1] && matrix[1,1] ==matrix[2,2]) return matrix[1,1];
	if(matrix[0,2] == matrix[1,1] && matrix[1,1] ==matrix[2,0]) return matrix[1,1];
	return 0;
```