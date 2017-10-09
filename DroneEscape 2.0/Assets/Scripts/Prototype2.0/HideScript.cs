using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideScript : MonoBehaviour {

    public GameObject anObject;
    public Collider anObjCollider;
    private Camera cam;
    private Plane[] planes;
    void Start() {
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        anObjCollider = GetComponent<Collider>();
    }
    void Update() {
        if (GeometryUtility.TestPlanesAABB(planes, anObjCollider.bounds))
            Debug.Log(anObject.name + " has been detected!");
        else
            Debug.Log("Nothing has been detected");
    }
}
