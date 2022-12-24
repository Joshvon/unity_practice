using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("collision");
            other.gameObject.GetComponent<Animator>().SetTrigger("die");
            Singleton<GameEventManager>.Instance.setGameOver();
        }
    }
}
