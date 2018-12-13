using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
    public float magnitude;
    public float brightness;
    public string constellationName;
    public List<GameObject> neighbors = new List<GameObject>();


    // Use this for initialization
    void Start() {
        Light light = light = gameObject.GetComponent<Light>();
        light.intensity = brightness*1000;
    }


    // Update is called once per frame
    void Update() {

    }


    public void setConstellationName(string cN) {
        constellationName = cN;
    }


    public void setMagnitude(float m) {
        magnitude = m;
        brightness = 1/m;
        resize();
    }


    public void resize() {
        transform.localScale = new Vector3(brightness, brightness, brightness);
        gameObject.GetComponent<AudioSource>().Play();
    }


    public void upgradeSize() {
        transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
    }


    public bool isFromConstellation(string constName) {
        if(constellationName == constName) {
            return true;
        } else {
            return false;
        }
    }




}
