using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback
{
    protected Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    protected List<SSAction> waitingAdd = new List<SSAction>();
    protected List<int> waitingDelete = new List<int>();
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SSActionManager(){}

    // Update is called once per frame
    void Update()
    {
        foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if(ac.destroy) {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if(ac.enable) {
                ac.Update();
            }
        }

        foreach (int key in waitingDelete) {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void reset()
    {
        actions.Clear();
    }

    public void SSActionEvent(SSAction source, int intParam, PatrolInfo objectParam)
	{
        if(intParam == 0)
        {
            ChaseAction chase = new ChaseAction();
            chase.setPatrolInfo(objectParam);
            this.RunAction(objectParam.patrol, chase, this);
        }
        else
        {
            PatrolAction move = new PatrolAction(objectParam);
            this.RunAction(objectParam.patrol, move, this);
        }
	}

    public void Stop()
    {
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            ac.destroy = true;
        }
    }
}
