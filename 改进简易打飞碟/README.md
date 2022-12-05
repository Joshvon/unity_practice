### 简易打飞碟--改进版

游戏代码: [游戏代码](https://github.com/Joshvon/unity_practice)
游戏演示视频: [演示视频](https://www.bilibili.com/video/BV19Y411o7HX/)

#### 编程内容
改进之前的简易打飞碟游戏-- [先前博客](https://www.cnblogs.com/joshf/p/16913635.html)
1. 按 *adapter* 模式设计图修改飞碟游戏
2. 使它同时支持物理运动与运动学（变换）运动

#### 实现思路
+ roundController增加一个 *public* 的 *bool* 类型的变量 `ifPhysicManager` 来表示是否使用物理运动
![ifPhysicManager](/改进简易打飞碟/img/roundController.png)
+ 增加一个物理运动的动作类 `PhysicalAction` ，继承 `SSAction`
+ 增加一个物理运动的管理类 `PhysicalActionManager` ，继承 `SSActionManager`
+ 改进飞碟信息类 `UFOInfo` ，增加对不同运动类型飞碟的支持

#### 改进代码
+ roundController
```csharp
if(ifPhysicManager)
    this.gameObject.AddComponent<PhysicalActionManager>();
else
    this.gameObject.AddComponent<SSActionManager>();
```
通过判断不同的运动实现使用不同的动作管理


```csharp
UFOInfo ufoInfo = ufofactory.getUFO(level, ifPhysicManager);
if(ifPhysicManager){
    PhysicalAction ac = PhysicalAction.getUFOAction(ufoInfo, level);
    actionManager.RunAction(ufoInfo.ufo, ac, null);
}
else {
    UFOAction ac = UFOAction.getUFOAction(ufoInfo, level);
    actionManager.RunAction(ufoInfo.ufo, ac, null);
}
```
通过判断不同的运动使用不同的飞碟（是否使用刚体）并调用不同的动作管理类

+ PhysicalAction
```csharp
Rigidbody rigidBody = action.ufo.ufo.GetComponent<Rigidbody>();
rigidBody.velocity = action.aim * action.speed;
rigidBody.useGravity = false;
return action;
```
当获取飞碟动作时，将飞碟的刚体组件赋予速度

```csharp
public void FixedUpdate()
{
    if(hasPaused) {         //若之前暂停游戏，则需将速度重置
        Rigidbody rigidBody = ufo.ufo.GetComponent<Rigidbody>();
        rigidBody.velocity = aim * speed;
        hasPaused = false;
    }
    time += Time.fixedDeltaTime;        //以时间度量飞碟出现时间
    if(time > 2) {
        time = 0;
        this.destroy = true;
        this.enable = false;
        Singleton<UFOFactory>.Instance.freeUFO(ufo);
        ufo.ufo.SetActive(false);
    }
}

public void Pause()
{
    hasPaused = true;
    Rigidbody rigidBody = ufo.ufo.GetComponent<Rigidbody>();
    rigidBody.velocity = Vector3.zero;
}
```
因为在运动学实现的游戏，暂停游戏时动作类停止 *Update* 此时飞碟不再运动；而物理学实现的游戏中，需将飞碟速度调整至0，所以增加 *Pause* 方法

+ PhysicalActionManager
```csharp
if(roundCtrl.status == "pause") {
    foreach (KeyValuePair<int, SSAction> kv in actions) {
        PhysicalAction ac = (PhysicalAction)kv.Value;
        ac.Pause();
    }
    return;
}
```
由之前所述，*Pause* 方法由动作管理类来调用

+ UFOInfo
```csharp
if(ifPhysicManager) ufo.AddComponent<Rigidbody>();
```
UFOInfo通过 `roundController` 的参数来判断是否要添加刚体组件

**因视频与动图与先前几乎无差别，在此不重复演示**
**最后感谢**[参考博客](https://blog.csdn.net/DDghsot/article/details/80053559)
