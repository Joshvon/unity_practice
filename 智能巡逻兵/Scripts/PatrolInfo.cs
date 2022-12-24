using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolInfo : MonoBehaviour
{
    public GameObject patrol;
    public GameObject player;
    public Vector3 position;
    public Vector3 home;
    public float dis = 10f;
    public bool ifReturn = false;
    public PatrolInfo(){}
}
