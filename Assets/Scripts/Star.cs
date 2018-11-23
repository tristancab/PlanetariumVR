using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
    public string name;
    public Vector3 coord;
    public float brightness;
    public string consstellationName;
    public Star neighboors;
	// Use this for initialization
	void Start () {
        Debug.Log("Nom : " + name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
