using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalActionManager : SSActionManager, ISSActionCallback
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if(roundCtrl.status == "pause") {
            foreach (KeyValuePair<int, SSAction> kv in actions) {
                PhysicalAction ac = (PhysicalAction)kv.Value;
                ac.Pause();
            }
            return;
        }

        foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions) {
            PhysicalAction ac = (PhysicalAction)kv.Value;
            if(ac.gameobject.active == false || ac.destroy) {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if(ac.enable) {
                ac.FixedUpdate();
            }
        }

        foreach (int key in waitingDelete) {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }

    public void reset()
    {
        actions.Clear();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null)
	{

	}
}
