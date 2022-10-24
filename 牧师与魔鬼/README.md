### 牧师与魔鬼

游戏代码: [游戏代码](https://github.com/Joshvon/unity_practice)
游戏演示视频: [演示视频](https://www.bilibili.com/video/BV1GG4y1h7wn/?vd_source=b0546020831d7e3b6408f8496120ad52)
#### 目录结构
`Resources` 项目用到的天空盒和预设等
`Scripts` 项目的脚本

#### 编程内容
![编程内容](/牧师与魔鬼/img/编程内容.png "编程内容")
+ 将游戏对象做成预制
+ 使用MVC结构

#### 实现思路
+ 组织游戏资源
在 `Assets/Resources/Prefabs` 目录下储存预制好的游戏对象
![游戏对象](/牧师与魔鬼/img/Prefabs.png "游戏对象")
+ MVC框架设计
![游戏框架](/牧师与魔鬼/img/MVC.png "游戏框架")
1. 场景中的所有GameObject就是Model，它们受到Controller的控制，比如说牧师和魔鬼受到MyCharacterController类的控制，船受到BoatController类的控制，河岸受到CoastController类的控制。
2. View就是UserGUI和ClickGUI，它们展示游戏结果，并提供用户交互的渠道（点击物体和按钮）。
3. 除了刚才说的MyCharacterController、BoatController、CoastController以外，还有更高一层的Controller：FirstController（场景控制器），FirstController控制着这个场景中的所有对象，包括其加载、通信、用户输入。
最高层的Controller是Director类，一个游戏中只能有一个实例，它控制着场景的创建、切换、销毁、游戏暂停、游戏退出等等最高层次的功能。
+ SSDirector
  利用单例模式创建导演，一个游戏导演只能有一个，这里继承于System.Object，保持导演类一直存在，不被Unity内存管理而管理，导演类类似于生活中的导演，安排场景，场景切换，都是靠它来指挥。
+ UserGUI
  建立用户的交互界面，比如按钮和标签
+ ClickGUI
  检测船和角色是否被点击
  点击则触发接口中的动作。然后进入控制器进行对应的函数操作。
+ conInterface
  这是将所有的接口放在同一命名空间下，同样方便了其他模块调用此命名空间。分别是场景控制器的接口，利用这个接口，得知当前场景是由哪个控制，然后向场景控制器传达要求，以及用户动作的接口，用户通过键盘、鼠标等对游戏发出指令，这个指令会触发游戏中的一些行为，由IUserAction来声明。
+ LandModel
  用于控制与河岸有关的动作，比如角色上下岸，船的离开和停靠。
  陆地的属性：陆地有两块，一个标志位来记录是开始的陆地还是结束陆地，陆地的位置，以及陆地上的角色，每个角色的位置
+ RoleModel
  用于控制6个角色的动作，比如上船，上岸等。
  一个角色的属性：标志角色是牧师还是恶魔，标志是否在船上
  角色模型的函数：去到陆地/船上(其实就是把哪个作为父节点，并且修改是否在船上标志)，其他就是基本的get/set函数。
+ BoatModel
  用于控制船的运动以及角色的上下船绑定。
  船的属性：船在开始/结束陆地旁的位置，在开始/结束陆地旁船上可以载客的两个位置(用Vector3的数组表示)，船上载有的角色(用角色模型的数组来记录)，标记船在开始陆地还是结束陆地的旁边。
+ Controller
  这是一个控制器，对场景中的具体对象进行操作，可以看到这个控制器继承了两个接口类并实现了它们的方法，控制器是场景中各游戏对象行为改变的核心。他需要引用模型控制器以及接口的命名空间来调用其中实现的函数来达到控制的目的。
+ 游戏演示
在Unity创建空对象并将Controller搭载上去并运行
![演示1](/牧师与魔鬼/img/游戏演示1.png "演示1")
![演示2](/牧师与魔鬼/img/游戏演示2.png "演示2")
![演示3](/牧师与魔鬼/img/游戏演示3.png "演示3")

**最后感谢**[参考博客](https://blog.csdn.net/TempterCyn/article/details/101110379)