using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour
{
    private List<PatrolInfo> patrols = new List<PatrolInfo>();
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
        int[] pos_x = {-15, 20, 18, 20, -16};
        int[] pos_z = {-18, -24, -5, 16, 16};
        for(int i = 0; i < 5; i++) {
            PatrolInfo patrol_ = new PatrolInfo();
            Vector3 pos = new Vector3(pos_x[i], 0, pos_z[i]);
            patrol_.patrol = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Patrol" ), pos, Quaternion.identity);
            patrol_.patrol.name = "Patrol" + i.ToString();
            patrol_.position = pos;
            patrol_.home = pos;
            patrols.Add(patrol_);
        }
    }

    public void Stop()
    {
        int size = patrols.Count;
        for (int i = 0; i < size; i++)
        {
            patrols[i].patrol.GetComponent<Animator>().SetBool("run", false);
        }
    }

    public List<PatrolInfo> GetPatrols()
    {
        return patrols;
    }
}
