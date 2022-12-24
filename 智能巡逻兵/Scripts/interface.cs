using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface sceneController
{
    //加载场景资源
    void LoadResources();
}

public interface IUserAction                          
{
    void MovePlayer(float translationX, float translationZ);
    //得到分数
    int GetScore();
    //得到游戏结束标志
    bool GetGameover();
    //重新开始
    void Reset();
}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,int intParam = 0, PatrolInfo objectParam = null);
}