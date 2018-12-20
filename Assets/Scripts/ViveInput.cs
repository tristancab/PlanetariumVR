using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour {

    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Boolean grabAction;
    public GameObject Laser ;
    public GameObject Terrain;


    void Start()
    {
        GameObject rightHand = GameObject.Find("RightHand");
        Instantiate(Laser, rightHand.transform.position, rightHand.transform.rotation);
    }

    void FixedUpdate () {
        GameObject rightHand = GameObject.Find("RightHand");
        GameObject laser = GameObject.Find("LaserPrefab(Clone)");
        laser.transform.rotation = rightHand.transform.rotation.normalized;
        laser.transform.Rotate(90, 0, 0);
        laser.transform.position = new Vector3(rightHand.transform.position.x , rightHand.transform.position.y, rightHand.transform.position.z);

        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            //Vector3 rotation = new Vector3(rightHand.transform.rotation.x, rightHand.transform.rotation.y, rightHand.transform.rotation.z);
            Vector3 rotation = rightHand.transform.TransformDirection(transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(rightHand.transform.position, rotation, Mathf.Infinity) ;
            if (hits.Length > 0) {
                Debug.Log("HitSomething");
                RaycastHit bestHit = hits[0];
                float distanceMin = Vector3.Distance(bestHit.transform.position, rightHand.transform.position);
                foreach (RaycastHit h in hits) {
                    //Debug.Log("Did Hit " + h.transform.name);
                    if (Vector3.Distance(h.transform.position, rightHand.transform.position) < distanceMin) {
                        bestHit = h;
                        distanceMin = Vector3.Distance(h.transform.position, rightHand.transform.position);
                    }
                }
                Terrain.GetComponent<Terrain>().castStar(bestHit.transform.gameObject);
                Debug.Log("BestHit : " + bestHit.transform.name);
            }
        }
	}

}
