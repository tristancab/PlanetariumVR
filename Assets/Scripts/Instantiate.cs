using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour {
    public GameObject star;
    public int nbStars;
    public int distanceFromOrigin;
	// Use this for initialization
	void Start () {
        Initialise();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialise()
    {
        star = GameObject.Find("Star");

        for (int i = 0; i < nbStars; i++)
        {
            int phi = Random.Range(-90, 90);
            int teta = Random.Range(-180, 180);
            float x = distanceFromOrigin * Mathf.Sin(phi) * Mathf.Cos(teta);
            float z = distanceFromOrigin * Mathf.Cos(phi);
            float y = distanceFromOrigin * Mathf.Sin(phi) * Mathf.Sin(teta);
            if(y < 0)
            {
                y = -y;
            }
            Instantiate(star, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
