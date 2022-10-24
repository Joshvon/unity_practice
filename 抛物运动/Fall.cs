using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private float g = 9.8F;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speed += g * Time.deltaTime;
        this.transform.position += Vector3.down * Time.deltaTime * speed;
    }
}
