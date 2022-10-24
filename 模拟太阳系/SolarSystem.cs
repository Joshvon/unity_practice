using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public Transform Sun;
    public Transform Mercury;
    public Transform Venus;
    public Transform Earth;
    public Transform Moon;
    public Transform Mars;
    public Transform Jupiter;
    public Transform Saturn;
    public Transform Uranus;
    public Transform Neptune;
    // Start is called before the first frame update
    void Start()
    {
        init_planetScale();
        init_planetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Mercury.RotateAround(Sun.position, new Vector3(0.3f, 1, 0), 10 * 365 / 87.7f * Time.deltaTime);
        Venus.RotateAround(Sun.position, new Vector3(0.2f, 1, 0), 10 * 365 / 224.7f * Time.deltaTime);
        Earth.RotateAround(Sun.position, Vector3.up, 10 * Time.deltaTime);
        Earth.Rotate(Vector3.up * 30 * Time.deltaTime);
        Moon.RotateAround(Earth.position, Vector3.up, 365 * Time.deltaTime);
        Mars.RotateAround(Sun.position, new Vector3(0.5f, 1, 0), 10 * 365 / 686.98f * Time.deltaTime);
        Jupiter.RotateAround(Sun.position, new Vector3(0.5f, 1, 0), 10 * 1 / 11.8f * Time.deltaTime);
        Saturn.RotateAround(Sun.position, new Vector3(0.6f, 1, 0), 10 * 1 / 29.5f * Time.deltaTime);
        Uranus.RotateAround(Sun.position, new Vector3(0.23f, 1, 0), 10 * 1 / 80.4f * Time.deltaTime);
        Neptune.RotateAround(Sun.position, new Vector3(0.17f, 1, 0), 10 * 1 / 164.8f * Time.deltaTime);
    }

    void init_planetScale()
    {
        this.Sun.localScale = new Vector3(20, 20, 20);
        this.Mercury.localScale = new Vector3(1.5F, 1.5F, 1.5F);
        this.Venus.localScale = new Vector3(3.6F, 3.6F, 3.6F);
        this.Earth.localScale = new Vector3(3.6F, 3.6F, 3.6F);
        this.Moon.localScale = new Vector3(0.9F, 0.9F, 0.9F);
        this.Mars.localScale = new Vector3(2.1F, 2.1F, 2.1F);
        this.Jupiter.localScale = new Vector3(14, 14, 14);
        this.Saturn.localScale = new Vector3(12, 12, 12);
        this.Uranus.localScale = new Vector3(5, 5, 5);
        this.Neptune.localScale = new Vector3(5, 5, 5);
    }
    void init_planetPosition()
    {
        this.Sun.position = Vector3.zero;
        this.Mercury.position = new Vector3(24, 0.2f, 0.02f);
        this.Venus.position = new Vector3(32, 0.03f, 0.3f);
        this.Earth.position = new Vector3(40, 0.07f, 0.7f);
        this.Moon.position = new Vector3(45, 0.07f, 0.7f);
        this.Mars.position = new Vector3(60, 0.09f, 0.9f);
        this.Jupiter.position = new Vector3(80, 0.8f, 0.08f);
        this.Saturn.position = new Vector3(100, 0.06f, 0.6f);
        this.Uranus.position = new Vector3(120, 0.3f, 0.39f);
        this.Neptune.position = new Vector3(140, 0.7f, 0.79f);
    }
}
