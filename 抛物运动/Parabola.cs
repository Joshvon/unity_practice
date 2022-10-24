using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    private float g = 9.8F;
    private float speed;
    private float fallingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        fallingSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fallingSpeed += g * Time.deltaTime;
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        this.transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);
    }
}
