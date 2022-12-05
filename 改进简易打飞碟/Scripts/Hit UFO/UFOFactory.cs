using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UFO;
public class UFOFactory : MonoBehaviour
{
    private int UFONum = 0;
    private List<UFOInfo> used = new List<UFOInfo>();
    private List<UFOInfo> free = new List<UFOInfo>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public UFOInfo getUFO(int level, bool ifPhysicManager)
    {
        UFOInfo ufo = null;
        if(free.Count > 0) {
            ufo = free[0];
            ufo.reset(level);
            used.Add(ufo);
            free.Remove(free[0]);
        }
        else {
            UFONum++;
            ufo = new UFOInfo(UFONum, level, ifPhysicManager);
            used.Add(ufo);
        }
        return ufo;
    }

    public void freeUFO(UFOInfo free_ufo)
    {
        if(used.Contains(free_ufo)) {
            free_ufo.ufo.SetActive(false);
            free.Add(free_ufo);
            used.Remove(free_ufo);
        }
    }

    public void reset()
    {
        foreach (UFOInfo tmp in used) {
            tmp.ufo.SetActive(false);
            free.Add(tmp);
        }
        used.Clear();
    }

    public int getUFONum()
    {
        return this.UFONum;
    }

    public bool finish()
    {
        if(used.Count == 0) return true;
        else return false;
    }

    public UFOInfo getUFO(GameObject ufo)
    {
        foreach(UFOInfo tmp in used) {
            if(tmp.ufo == ufo) {
                return tmp;
            }
        }
        return null;
    }
}
