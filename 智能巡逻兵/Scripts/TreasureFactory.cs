using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureFactory : MonoBehaviour
{
    private List<GameObject> treasure = new List<GameObject>();
    int[] pos_x = {-18, 8, 10, 12, -12};
    int[] pos_z = {-8, -20, 0, 20, 16};
    int cnt = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        for(int i = 0; i < cnt; i++) {
            Vector3 pos = new Vector3(pos_x[i], 0, pos_z[i]);
            GameObject treasure_ = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Treasure" ), pos, Quaternion.identity);
            treasure.Add(treasure_);
        }
    }
    public int getCnt()
    {
        return cnt;
    }
    public void reduceTreasure()
    {
        cnt--;
    }
    public void OnEnable()
    {
        GameEventManager.treasureChange += reduceTreasure;
    }
    public void OnDisable()
    {
        GameEventManager.treasureChange -= reduceTreasure;
    }
}
