using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UFO
{
    public class UFOInfo
    {
        public GameObject ufo;
        public int ufoid;
        public int level;
        public UFOInfo(int id, int l)
        {
            this.ufoid = id;
            this.level = l;
            ufo = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UFO" ), Vector3.zero , Quaternion.identity);
            ufo.name = "ufo" + id.ToString();
            reset(l);
        }
        public void reset(int l)
        {
            ufo.transform.position = new Vector3 (Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(0f, 2f));
            this.level = l;
            switch(l) {
            case 1:
                ufo.GetComponent<Renderer> ().material.color = Color.red;
                ufo.transform.localScale = new Vector3 (3f, 0.3f, 3f);
                break;
            case 2:
                ufo.GetComponent<Renderer> ().material.color = Color.yellow;
                ufo.transform.localScale = new Vector3 (2f, 0.2f, 2f);
                break;
            default:
                ufo.GetComponent<Renderer> ().material.color = Color.blue;
                ufo.transform.localScale = new Vector3 (1f, 0.1f, 1f);
                break;
            }
            ufo.SetActive(true);
        }
    }
}

