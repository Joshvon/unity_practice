using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject
{
    public bool enable = true;
    public bool destroy = false;

    public GameObject gameobject;                   
    public Transform transform;                    
    public ISSActionCallback callback;             

	protected SSAction() { }        //该类为虚类，防止直接new一个该对象
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
