using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    private FirstController ctrl;
    public GameObject follow;
    public float smothing = 5f;
    Vector3 offset;   
    public float sensitivityMouse = 2f;
    public float sensitivetyMouseWheel = 10f;
 
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            this.GetComponent<Camera>().fieldOfView = this.GetComponent<Camera>().fieldOfView - Input.GetAxis("Mouse ScrollWheel") * sensitivetyMouseWheel;
        }
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivityMouse, Input.GetAxis("Mouse X") * sensitivityMouse, 0);
        }

        Vector3 target = follow.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, target, smothing * Time.deltaTime);
    }
    void Start()
    {
        this.GetComponent<Camera>().transform.position = new Vector3(0, 50, 0);
        this.GetComponent<Camera>().transform.Rotate(45, 0, 0);
        ctrl = (FirstController)Director.getInstance().currentSceneController;
        follow = ctrl.player;
        offset = transform.position - follow.transform.position;
    }
}
 