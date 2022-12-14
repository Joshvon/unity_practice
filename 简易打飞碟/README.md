### 简易打飞碟

游戏代码: [游戏代码](https://github.com/Joshvon/unity_practice)
游戏演示视频: [演示视频](https://www.bilibili.com/video/BV19Y411o7HX/)
#### 目录结构
`Resources` 项目用到的天空盒和预设等
`Scripts` 项目的脚本

#### 编程内容
编写一个简单的打飞碟（Hit UFO）游戏
+ 游戏内容要求:
  1. 游戏有n个round，每个round都包括10次trail；
  2. 每个trail的飞碟的色彩、大小、发射位置、速度、角度、同时出现的个数都可能不同。它们由该round的ruler控制；
  3. 每个trail的飞碟有随机性，总体难度随round上升；
  4. 鼠标击中得分，得分规则按色彩、大小、速度不同计算，规则可自由设定
+ 游戏的要求
  1. 使用带缓存的工厂模式管理不同飞碟的生产和回收，该工厂必须是场景单实例的！具体实现见参考资源Singleton模板类
  2. 尽可能使用前面MVC结构实现人机交互与游戏模型分离

#### 实现思路
+ 组织游戏资源
在 `Assets/Resources/Prefabs` 目录下储存预制好的游戏对象
+ 工厂模式和MVC结构设计
1. 使用带缓存的工厂模式管理不同飞碟的生产和回收，该工厂是场景单实例的
2. 设计游戏对象飞碟，以便对不同飞碟的大小和颜色进行调整，一个飞碟由工厂生产出来后就会不停的使用以节省对象生成和销毁的开销
3. 使用动作管理器来管理飞碟的动作，Controller通过调用动作管理器来执行飞碟动作
  + Director
  利用单例模式创建导演，一个游戏导演只能有一个，这里继承于System.Object，保持导演类一直存在，不被Unity内存管理而管理，导演类类似于生活中的导演，安排场景，场景切换，都是靠它来指挥。
  + ISSActionCallback
  接口作为接收通知对象的抽象类型。事件类型定义，使用了枚举变量；定义了事件处理接口，所有事件管理者都必须实现这个接口，来实现事件调度。所以，组合事件需要实现它，事件管理器也必须实现它。
  + roundController
  这是一个控制器，对场景中的具体对象进行操作，控制游戏状态和飞碟的发送，并暴露一个`public void hitUFO(GameObject ufo)`接口给`UserGUI`来处理击中飞碟的处理
  + ScoreController
  控制得分的增加与重置
  + Singleton
  运用模板，可以为每个MonoBehavior子类创建一个对象的实例
  + SSAction
  SSAction类是所有动作的基类，SSAction继承了`ScriptableObject`，`ScriptableObject` 是不需要绑定 GameObject 对象的可编程基类。这些对象受 Unity 引擎场景管理。根据门面模式将其功能抽象为`SSActionManager`，通过protected 防止用户自己 new 对象；使用 virtual 申明虚方法，通过重写实现多态。利用接口（ISSACtionCallback）实现消息通知，避免与动作管理者直接依赖
  + SSActionManager
  这是动作对象管理器的基类，实现了所有动作的基本管理，执行动作。实现游戏对象与动作的绑定，确定回调函数消息的接收对象。管理动作之间的切换。
  + UFOAction
  实现具体动作，将一个飞碟移动到目标位置，并根据飞碟等级控制不同速度
  + UFOFactory
  飞碟工厂控制飞碟游戏对象的制作与回收，used队列是存放在使用的飞碟，free是队列是存放没有在飞的飞碟，场记找飞碟工厂要飞碟的时候，飞碟工厂就看free队列有没有飞碟，没有才新建，尽可能减少对象的新建
  + UFOInfo
  该类有一个私有的游戏对象飞碟，以便直接对飞碟的颜色，大小等等进行调整，由于一个飞碟由工厂生产出来后就会不停的使用以节省对象生成和销毁的开销，因此飞碟重新使用时会有一个`reset()`方法来重新设置飞碟的参数，`reset()`方法通过传递该飞碟的等级，来进行大小、速度等等调整
  + UserGUI
  建立用户的交互界面，比如按钮和标签；检测玩家是否击中飞碟
+ 游戏演示
![演示1](/简易打飞碟/img/游戏演示1.png "演示1")
![演示2](/简易打飞碟/img/游戏演示2.png "演示2")
![演示3](/简易打飞碟/img/游戏演示3.png "演示3")

**最后感谢**[参考博客](https://blog.csdn.net/DDghsot/article/details/79964701)