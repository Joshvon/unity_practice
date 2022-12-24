using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player") {
            PatrolFactory patrolFactory = Singleton<PatrolFactory>.Instance;
            foreach(PatrolInfo info in patrolFactory.GetPatrols()) {
                if(this.gameObject.Equals(info.patrol)) {
                    info.player = other.gameObject;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player") {
            PatrolFactory patrolFactory = Singleton<PatrolFactory>.Instance;
            foreach(PatrolInfo info in patrolFactory.GetPatrols()) {
                if(this.gameObject.Equals(info.patrol)) {
                    info.ifReturn = true;
                    info.player = null;
                    Singleton<GameEventManager>.Instance.addScore();
                }
            }
        }
    }
}
