using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UFO;
public class PhysicalAction : SSAction
{
    public Vector3 aim;
    public float speed;
    public UFOInfo ufo;
    public float time = 0;
    public bool hasPaused = true;
    
    public static PhysicalAction getUFOAction(UFOInfo ufo, int level)
    {
        PhysicalAction action = ScriptableObject.CreateInstance<PhysicalAction> ();
        switch (level) {
        case 1:
            action.speed = 6f;
            break;
        case 2:
            action.speed = 7f;
            break;
        case 3:
            action.speed = 8f;
            break;
        }
        action.ufo = ufo;
        action.aim = new Vector3(Random.Range(-0.4f, 0.2f) , Random.Range(-0.4f, 0.4f) , Random.Range(-0.4f, 0.4f));

        Rigidbody rigidBody = action.ufo.ufo.GetComponent<Rigidbody>();
        rigidBody.velocity = action.aim * action.speed;
        //rigidBody.AddForce(action.aim * action.speed, ForceMode.VelocityChange);
        rigidBody.useGravity = false;
        return action;
    }

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
        if(hasPaused) {
            Rigidbody rigidBody = ufo.ufo.GetComponent<Rigidbody>();
            rigidBody.velocity = aim * speed;
            hasPaused = false;
        }
        time += Time.fixedDeltaTime;
        if(time > 2) {
            time = 0;
            this.destroy = true;
            this.enable = false;
            Singleton<UFOFactory>.Instance.freeUFO(ufo);
            ufo.ufo.SetActive(false);
        }
    }

    public void Pause()
    {
        hasPaused = true;
        Rigidbody rigidBody = ufo.ufo.GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.zero;
    }
}
