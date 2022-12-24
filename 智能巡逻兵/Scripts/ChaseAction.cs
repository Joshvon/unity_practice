using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : SSAction
{
    private Vector3 aim;
    private float speed;
    private PatrolInfo patrolInfo;
    // Start is called before the first frame update
    public ChaseAction(){}
    public override void Start()
    {
        speed = 0.1f;
    }

    // Update is called once per frame
    public override void Update()
    {
        if(patrolInfo.player != null) {
            aim = patrolInfo.player.transform.position;
            gameobject.transform.position = Vector3.MoveTowards(gameobject.transform.position, aim, speed * Time.deltaTime);
            this.transform.LookAt(aim);
            if(Vector3.Distance(this.transform.position, patrolInfo.home) > patrolInfo.dis) {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this, 1, patrolInfo);
            }
        }
    }
    public void setPatrolInfo(PatrolInfo info)
    {
        patrolInfo = info;
    }
    public PatrolInfo getPatrol()
    {
        return patrolInfo;
    }
}
