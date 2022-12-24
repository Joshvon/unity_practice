using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager
{
    public void GoPatrol(PatrolInfo patrol)
    {
        PatrolAction patrolAction = new PatrolAction(patrol);
        this.RunAction(patrol.patrol, patrolAction, this);
    }
}
