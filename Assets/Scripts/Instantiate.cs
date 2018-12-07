﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Instantiate : MonoBehaviour {
    public GameObject star;
    

	// Use this for initialization
	void Start () {
        Initialise();
        Destroy(star);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Initialise()
    {
        //Get File
        string path = @"Assets\Data\test.txt";
        if (File.Exists(path))
        {
            int cpt = 0;
            string[] lines = File.ReadAllLines(path);
            char separator = ' ';
            float x, y, z, magnitude;
            string constellation;
            foreach (string line in lines)
            {
                string[] args = line.Split(separator);
                x = float.Parse(args[0]);
                y = float.Parse(args[1]);
                z = float.Parse(args[2]);
                magnitude = float.Parse(args[3]);
                constellation = args[4];
                GameObject myStar = Instantiate(star, new Vector3(x, y, z), Quaternion.identity);
                Star componentStar = myStar.GetComponent<Star>();
                componentStar.setConstellationName(constellation);
                componentStar.setMagnitude(magnitude);
                myStar.name = constellation + "_" + cpt;
                cpt++;

            }
        } else {
            Debug.Log("Pas de fichier");
        }

    }

}
