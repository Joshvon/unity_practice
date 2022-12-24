using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            Singleton<GameEventManager>.Instance.addScore();
            Singleton<GameEventManager>.Instance.reduceTreasure();
        }
    }
}
