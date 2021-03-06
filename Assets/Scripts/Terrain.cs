﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Terrain : MonoBehaviour {
    public GameObject star;
    public GameObject link;
    public int multDistance;
    public float linkSize;
    public GameObject panelInterface;
    public GameObject starInformationInterface;
    public GameObject starConstellationNameInterface;
    public GameObject starMagnitudeInterface;

    private GameObject starDisplayed = null;
    private bool isStarsDisplay = false;
    private string keyToDisplayStars = "s";
    private string keyToDisplayConst = "c";


    // Use this for initialization
    void Start() {
        Initialise();
        Destroy(star);
        isStarsDisplay = true;

    }


    // Update is called once per frame
    void Update() {

    }


    // Init stars from the file test.txt
    public void Initialise() {
        //Get File
        string path = @"Assets\Data\test.txt";
        if (File.Exists(path)) {
            int cpt = 0;
            string[] lines = File.ReadAllLines(path);
            char separator = ' ';
            char separatorNeighbors = ',';
            float x, y, z, magnitude;
            string constellation;
            string previousConstellation = "";
            string neighbors;
            //Setup each star
            foreach (string line in lines) {
                string[] args = line.Split(separator);
                x = float.Parse(args[0]) * multDistance;
                y = float.Parse(args[1]) * multDistance;
                z = float.Parse(args[2]) * multDistance;
                magnitude = float.Parse(args[3]);
                constellation = args[4];
                if(cpt == 0) {
                    previousConstellation = constellation;
                }
                if(previousConstellation != constellation) {
                    previousConstellation = constellation;
                    cpt = 0;
                }
                //Instantiate each star as a gameObject
                GameObject myStar = Instantiate(star, new Vector3(x, y, z), Quaternion.identity);
                Star componentStar = myStar.GetComponent<Star>();
                componentStar.setConstellationName(constellation);
                componentStar.setMagnitude(magnitude);
                myStar.name = constellation + "_" + cpt;
                cpt++;
            }
            cpt = 0;
            //Find neighbors
            foreach (string line in lines) {
                string[] args = line.Split(separator);
                constellation = args[4];
                neighbors = args[5];
                if (cpt == 0) {
                    previousConstellation = constellation;
                }
                if (previousConstellation != constellation) {
                    previousConstellation = constellation;
                    cpt = 0;
                }
                GameObject myStar = GameObject.Find(constellation + "_" + cpt);
                Star componentStar = myStar.GetComponent<Star>();
                String[] NeighborsIndex = neighbors.Split(separatorNeighbors);
                foreach (String n in NeighborsIndex) {
                    componentStar.neighbors.Add(GameObject.Find(constellation + "_" + n));
                }
                cpt++;
            }
        } else {
            Debug.Log("Pas de fichier");
        }

        
    }


    // Use to display links between Star from the same constellation
    public void DisplayLink(String ConstellationName) {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        int nbStars = 0;
        foreach(GameObject star in stars) {
            if (star.GetComponent<Star>().isFromConstellation(ConstellationName)) {
                nbStars++;
            }
        }
        List<String[]> drawnCouples = new List<String[]>();
        bool drawn = false;
        for (int i = 0; i < nbStars; i++) {
            String firstStarName = ConstellationName + "_" + i;
            GameObject currentStar = GameObject.Find(firstStarName);
            List<GameObject> neighbors = currentStar.GetComponent<Star>().neighbors;
            foreach (GameObject s in neighbors) {
                drawn = false;
                foreach (String[] couple in drawnCouples) {
                    if (couple[0].Equals(currentStar.name) && couple[1].Equals(s.name)) {
                        drawn = true;
                    }
                }
                if (!drawn) {
                    DrawLink(currentStar, s);
                    drawnCouples.Add(new String[] { currentStar.name, s.name });
                    drawnCouples.Add(new String[] { s.name, currentStar.name });
                }
            }
        }
    }


    // Use to draw the link between two stars
    public void DrawLink(GameObject s1, GameObject s2) {
        // Les coordonnées du lien sont les coordonnées moyennes des deux étoiles
        float x = s1.transform.position.x/2 + s2.transform.position.x/2;
        float y = s1.transform.position.y/2 + s2.transform.position.y/2;
        float z = s1.transform.position.z/2 + s2.transform.position.z/2;
        // La scale en y du lien est la distances entre les deux étoiles divisée par 2
        float scaleY = (s1.transform.position - s2.transform.position).magnitude / 2 ;
        // On rotate en suivant le vecteur reliant les deux étoiles
        Vector3 rota = (s1.transform.position - s2.transform.position).normalized ;
        GameObject link1 = Instantiate(link, new Vector3(x,y,z), Quaternion.LookRotation(rota));
        link1.tag = "Link";
        link1.transform.Rotate(90, 0, 0);
        link1.transform.localScale = new Vector3(linkSize, scaleY, linkSize);
    }



    // Use to destroy links between Star from the same constellation
    public void DestroyLink() {
        GameObject[] links = GameObject.FindGameObjectsWithTag("Link");
        foreach (GameObject link in links) {
            Destroy(link);
        }
    }


    // Use to display information of a star selected in the UI
    public void DisplayInformation(GameObject starToCast) {
        panelInterface.SetActive(true);
        starInformationInterface.GetComponent<Text>().text = starToCast.name;
        starConstellationNameInterface.GetComponent<Text>().text = "Constellation : " + starToCast.GetComponent<Star>().constellationName;
        starMagnitudeInterface.GetComponent<Text>().text = "Magnitude : " + starToCast.GetComponent<Star>().magnitude;

    }


    // Use to upgrade the star selected
    public void UpgradeStar(GameObject starCast) {
        if (starDisplayed != starCast)
        {
            starCast.GetComponent<Star>().upgradeSize();
            if (starDisplayed != null)
            {
                starDisplayed.GetComponent<Star>().resize();
            }
        }
        starDisplayed = starCast;
    }


    // Make change in the scene when you cast a star
    public void castStar(GameObject starToCast) {
        if (isStarsDisplay) {
            // Display links
            string constToDisplay = starToCast.GetComponent<Star>().constellationName;
            DestroyLink();
            DisplayLink(constToDisplay);
            // Display star information in the UI
            DisplayInformation(starToCast);
            // Upgrade star brightness
            UpgradeStar(starToCast);
        }
        
    }
}