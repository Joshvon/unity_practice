using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : SSAction
{
    private float pos_x, pos_z;
    private float move_len;
    private float speed;
    private enum Dirction { EAST, NORTH, WEST, SOUTH };
    private Dirction dirction = Dirction.EAST;
    private bool isReach = true;
    private PatrolInfo patrolInfo;

    public PatrolAction(PatrolInfo patrolInfo)
    {
        Vector3 location = patrolInfo.home;
        this.pos_x = location.x;
        this.pos_z = location.z;
        speed = 0.1f;
        this.move_len = Random.Range(5, 8);
        this.patrolInfo = patrolInfo;
    }
    void Gopatrol()
    {
        if(isReach)
        {
            switch (dirction)
            {
                case Dirction.EAST:
                    pos_x -= move_len;
                    break;
                case Dirction.NORTH:
                    pos_z += move_len;
                    break;
                case Dirction.WEST:
                    pos_x += move_len;
                    break;
                case Dirction.SOUTH:
                    pos_z -= move_len;
                    break;
            }
            isReach = false;
        }
        patrolInfo.patrol.transform.LookAt(new Vector3(pos_x, 0, pos_z));
        float distance = Vector3.Distance(patrolInfo.patrol.transform.position, new Vector3(pos_x, 0, pos_z));
        if (distance > 0.9)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_x, 0, pos_z), speed * Time.deltaTime);
        }
        else
        {
            dirction = dirction + 1;
            if (dirction > Dirction.SOUTH)
            {
                dirction = Dirction.EAST;
            }
            isReach = true;
        }
    }
    // Start is called before the first frame update
    public override void Start()
    {
        patrolInfo.patrol.GetComponent<Animator>().SetBool("run", true);
    }

    // Update is called once per frame
    public override void Update()
    {
        if(patrolInfo.ifReturn) {
            goback();
        }
        else {
            Gopatrol();
            if(patrolInfo.player != null)
            {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this, 0, patrolInfo);
            }
        }
    }

    public void goback()
    {
        gameobject.transform.position = Vector3.MoveTowards(gameobject.transform.position, patrolInfo.home, speed * Time.deltaTime);
        this.transform.LookAt(patrolInfo.home);
        float distance = Vector3.Distance(patrolInfo.patrol.transform.position, patrolInfo.home);
        if(distance < 0.1) {
            patrolInfo.ifReturn = false;
            dirction = Dirction.EAST;
        }
    }
}
