### 智能巡逻兵

游戏代码: [游戏代码](https://github.com/Joshvon/unity_practice)
游戏演示视频: [演示视频](https://www.bilibili.com/video/BV1gv4y1z7rq/)

#### 编程内容
![](/智能巡逻兵/img/rule.png)

#### 实现思路
+ 组织游戏资源，将地图、巡逻兵和玩家做成预制，其中巡逻兵和玩家模型来自于AssetStore [玩家](https://assetstore.unity.com/packages/p/dog-knight-pbr-polyart-135227) [巡逻兵](https://assetstore.unity.com/packages/3d/characters/humanoids/battle-royale-duo-polyart-pbr-185080)
  ![](/智能巡逻兵/img/prefabs.png)
  巡逻兵添加了RigidBody组件和两个Collider组件，一个为自身大小范围，用于判断巡逻兵是否抓到玩家；另一个为大范围，并勾选Collider的 `is Trigger` 用于判断玩家是否接近巡逻兵
+ Animator制作
  巡逻兵的Animator，其中bool变量run来决定巡逻兵在静止和奔跑的动画切换
  ![](/智能巡逻兵/img/patrol_animator.png)
  玩家的Animator，其中bool变量run来决定玩家在静止和奔跑的动画切换，trigger触发器来触发玩家死亡动画
  ![](/智能巡逻兵/img/player_animator.png)
+ 脚本设计
  + ChaseCollide
    用于判断玩家是否接近巡逻兵，搭载在巡逻兵上，当玩家碰撞到较大的Collider时会将巡逻兵信息的玩家成员变量由空转为玩家，当玩家离开区域时，将其变回空并调用 `GameEventManager` 进行加分
  + PatrolCollide
    用于判断玩家是否与巡逻兵碰撞，搭载在巡逻兵上，当玩家碰撞到较小的Collider时巡逻兵会将玩家的Animator中 `die` 触发器触发，玩家进入死亡并调用 `GameEventManager` 使游戏结束
  + TreasureCollide
    用于判断玩家是否与宝藏碰撞，搭载在宝藏上，当玩家碰撞到宝藏时，宝藏消失并调用 `GameEventManager` 进行加分和减少宝藏数
  + TreasureFactory
    用于生成宝藏并注册减少宝藏数事件
  + CameraFlow
    用于控制相机跟随、滚轮实现拉近拉远，右键实现旋转视角
  + PatrolInfo
    用于记录巡逻兵基本信息，如起始位置，离开起始位置的最大距离
  + PatrolFactory
    巡逻兵工厂，用 `List` 保存生成的巡逻兵
  + SSAction
    SSAction类是所有动作的基类，SSAction继承了`ScriptableObject`，`ScriptableObject` 是不需要绑定 GameObject 对象的可编程基类。这些对象受 Unity 引擎场景管理。根据门面模式将其功能抽象为`SSActionManager`，通过protected 防止用户自己 new 对象；使用 virtual 申明虚方法，通过重写实现多态。利用接口（ISSACtionCallback）实现消息通知，避免与动作管理者直接依赖
  + PatrolAction
    巡逻兵按固定区间的随机的距离来进行矩形的巡逻，当发现玩家时便会通过回调调用 `ChaseAcion` 来追逐玩家，当重新进入巡逻时会先返回起点再进行巡逻
  + ChaseAction
    巡逻兵追逐玩家，当巡逻兵离起点太远时会通过回调调用 `PatrolAction` 来重新巡逻
  + SSActionManager
    这是动作对象管理器的基类，实现了所有动作的基本管理，执行动作。实现游戏对象与动作的绑定，确定回调函数消息的接收对象。管理动作之间的切换
  + CCActionManager
    使巡逻兵开始巡逻
  + Director
    利用单例模式创建导演，一个游戏导演只能有一个，这里继承于System.Object，保持导演类一直存在，不被Unity内存管理而管理，导演类类似于生活中的导演，安排场景，场景切换，都是靠它来指挥。
  + Singleton
    运用模板，可以为每个MonoBehavior子类创建一个对象的实例
  + ScoreController
    用于计分并注册加分事件
  + GameEventManager
    作为发布者负责游戏结束、加分、减少宝藏数三个动作
  + FirstController
    主控制模块，加载资源，玩家移动的逻辑，游戏结束和重置等等，并且注册了游戏结束事件
  + UserGUI
    显示分数，规则按钮，重置按钮和游戏结束提示，并接收键盘输入来移动玩家
+ 游戏演示
  ![](/智能巡逻兵/img/result1.png)
  ![](/智能巡逻兵/img/result2.png)
  ![](/智能巡逻兵/img/result3.gif)
