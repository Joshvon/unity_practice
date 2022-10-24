using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall3 : MonoBehaviour
{
    private float g = 9.8F;
    private float speed;
    private float falling_speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        falling_speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        falling_speed += g * Time.deltaTime;
        float target_y = Mathf.MoveTowards(this.transform.position.y, this.transform.position.y - falling_speed * Time.deltaTime, speed * Time.deltaTime);
        float target_x = Mathf.MoveTowards(this.transform.position.x, this.transform.position.x + speed * Time.deltaTime, falling_speed * Time.deltaTime);
        this.transform.position = new Vector3(target_x, target_y, this.transform.position.z);
    }
}
