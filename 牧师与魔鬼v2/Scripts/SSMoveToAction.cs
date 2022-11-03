using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
