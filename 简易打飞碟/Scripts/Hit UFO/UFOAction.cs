using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UFO;
public class UFOAction : SSAction
{
    public Vector3 aim;
    public float speed;
    public UFOInfo ufo;
    public float time = 0;

    public static UFOAction getUFOAction(UFOInfo ufo, int level)
    {
        UFOAction action = ScriptableObject.CreateInstance<UFOAction> ();
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
        action.aim = new Vector3(Random.Range(-2f, 2f) , Random.Range(-2f, 2f) , Random.Range(4f, 10f) );
        return action;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        gameobject.transform.position = Vector3.MoveTowards(gameobject.transform.position, aim, speed * Time.deltaTime);
        if (this.transform.position == aim) {
            this.destroy = true;
            this.enable = false;
            Singleton<UFOFactory>.Instance.freeUFO(ufo);
            ufo.ufo.SetActive(false) ;
        }
    }
}
