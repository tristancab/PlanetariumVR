using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
    public float magnitude;
    public float brightness;
    public string constellationName;
    public List<GameObject> neighbors = new List<GameObject>();

    public void setConstellationName(string cN)
    {
        constellationName = cN;
    }

    public void setMagnitude(float m)
    {
        magnitude = m;
        brightness = 5 - m;
        Light light = GetComponent<Light>();
    } 

    // Use this for initialization
    void Start () {
        Light light = gameObject.GetComponentInChildren<Light>();
        light.intensity = brightness * 100;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
