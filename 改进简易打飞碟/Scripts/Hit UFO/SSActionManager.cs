using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using baseCode;
public class SSActionManager : MonoBehaviour
{
    protected Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    protected List<SSAction> waitingAdd = new List<SSAction>();
    protected List<int> waitingDelete = new List<int>();
    protected roundController roundCtrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SSActionManager()
    {
        roundCtrl = (roundController)Director.getInstance ().currentSceneController;
    }

    // Update is called once per frame
    void Update()
    {
        if (roundCtrl.status == "pause")
            return;
        foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if(ac.gameobject.active == false || ac.destroy) {
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

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null)
	{

	}
}
