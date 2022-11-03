### 牧师与魔鬼（动作分离版）

游戏代码: [游戏代码](https://github.com/Joshvon/unity_practice)
游戏演示视频: [演示视频](https://www.bilibili.com/video/BV1GG4y1h7wn/?vd_source=b0546020831d7e3b6408f8496120ad52)

之前实现了[牧师与魔鬼普通版](https://www.cnblogs.com/joshf/p/16817539.html)
本次只改动和增加了部分脚本，沿用了上次的预制游戏对象

#### 实现思路
+ 设计一个抽象类作为游戏对象动作的基类
+ 设计一个动作管理器类管理一组游戏动作的实现类
+ 通过回调，实现动作完成时的通知
+ 设计一个裁判类，当游戏达到结束条件时，通知场景控制器游戏结束

将移动动作的执行从每一个游戏对象中提取出来，建立一个动作管理器来管理移动方法，游戏对象通过将目标位置传给动作管理器，让动作管理器来移动游戏对象
将游戏的判定机制抽象成一个类，通过调用该类的方法来判定游戏是否结束
这使得：
1. 程序的解耦合程度提高
2. 更多对象可以复用
3. 程序更容易维护

+ SSAction
SSAction类是所有动作的基类，SSAction继承了`ScriptableObject`，`ScriptableObject` 是不需要绑定 GameObject 对象的可编程基类。这些对象受 Unity 引擎场景管理。根据门面模式将其功能抽象为`SSActionManager`，通过protected 防止用户自己 new 对象；使用 virtual 申明虚方法，通过重写实现多态。利用接口（ISSACtionCallback）实现消息通知，避免与动作管理者直接依赖

+ SSMoveToAction
实现具体动作，将一个物体移动到目标位置，并通知任务完成。让 Unity 创建动作类，确保内存正确回收。动作完成，则期望管理程序自动回收运行对象，并发出事件通知管理者。
```csharp
public class SSMoveToAction : SSAction
{

    public Vector3 target;       
	public float speed;       

    private SSMoveToAction() { }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        if (this.transform.position == target)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);     
        }
    }

    
    public static SSMoveToAction GetSSAction(Vector3 target, float speed)
    {
        SSMoveToAction action = ScriptableObject.CreateInstance<SSMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

}

```
+ SequenceAction
实现一个动作组合序列，顺序播放动作让动作组合继承抽象动作，能够被进一步组合；实现回调接受，能接收被组合动作的事件创建一个动作顺序执行序列。Start 执行动作前，为每个动作注入当前动作游戏对象，并将自己作为动作事件的接收者，Update方法执行执行当前动作。
```csharp
public class SequenceAction : SSAction, ISSActionCallback
{
    public List<SSAction> sequence;    
    public int repeat = -1;           
    public int start = 0;           

    public static SequenceAction GetSSAcition(int repeat, int start, List<SSAction> sequence)
    {
        SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }

    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();     
        }
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
        source.destroy = false;         
        this.start++;
        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.destroy = true;               
                this.callback.SSActionEvent(this);
            }
        }
    }

    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameobject = this.gameobject;
            action.transform = this.transform;
            action.callback = this;               
            action.Start();
        }
    }

    void OnDestroy()
    {

    }
}
```
+ ISSActionCallback
接口作为接收通知对象的抽象类型。事件类型定义，使用了枚举变量；定义了事件处理接口，所有事件管理者都必须实现这个接口，来实现事件调度。所以，组合事件需要实现它，事件管理器也必须实现它。
+ SSActionManager
这是动作对象管理器的基类，实现了所有动作的基本管理，执行动作，管理动作组合序列。实现游戏对象与动作的绑定，确定回调函数消息的接收对象。管理动作之间的切换。
+ MySceneActionManager
当前场景下的动作管理的具体实现，在场景控制类中调用它的方法，实现对当前场景的动作管理。其中需要管理两个动作：一个是场景中船的移动，这是一个单独的移动，我们只需要知道船的当前位置和需要移动到的位置就可以调用上面动作管理积累的函数实现船的移动动作了；还有一个动作是人物的移动，这是一系列的动作，需要先将人平移到船上方的位置，再将人移动到陆地/船上。这需要两个动作，也就是需要调用顺序动作的函数来实现这个动作，我们定义好这两个动作，然后在调用动作管理基类的函数就可以实现人的移动动作了。
+ Judge
实现一个裁判类，控制器初始化时将两个陆地和一个小船三个游戏对象注入到裁判类中，裁判类通过游戏规则判断游戏是否结束。而通过调用裁判类来通知场景控制器，而场景控制器又通知UI，在UI中查看游戏状态判断是否结束游戏即可
```csharp
public class Judge {
	LandModel start_land;
	LandModel end_land;
	BoatModel boat;
	public Judge(LandModel start_,LandModel end_,BoatModel boat_)
	{
		start_land = start_;
		end_land = end_;
		boat = boat_;
	}
	public int Check()
	{
		int start_priest = (start_land.GetRoleNum())[0];
		int start_devil = (start_land.GetRoleNum())[1];
		int end_priest = (end_land.GetRoleNum())[0];
		int end_devil = (end_land.GetRoleNum())[1];

		if (end_priest + end_devil == 6)     
			return 2;

		int[] boat_role_num = boat.GetRoleNumber();
		if (boat.GetBoatSign() == 1)         
		{
			start_priest += boat_role_num[0];
			start_devil += boat_role_num[1];
		}
		else                                  
		{
			end_priest += boat_role_num[0];
			end_devil += boat_role_num[1];
		}
		if (start_priest > 0 && start_priest < start_devil) 
		{      
			return 1;
		}
		if (end_priest > 0 && end_priest < end_devil)        
		{
			return 1;
		}
		return 0;                                             
	}
}
```
+ 模型控制的修改
实现了动作管理器，而游戏对象的运动交给动作管理器，船的移动只需将移动位置返回给动作管理器即可；人物移动还是先判断在船上还是在陆地，再获取相应的空位置给动作管理器
因为游戏对象使用的预制与之前版本一样，游戏演示效果也一样，在这里就不展示效果了

**最后感谢**[参考博客](https://blog.csdn.net/TempterCyn/article/details/101617728)