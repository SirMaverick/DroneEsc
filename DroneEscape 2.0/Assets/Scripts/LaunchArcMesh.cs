using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArcMesh : MonoBehaviour {



    Mesh mesh;
    GameObject camera;
    public float meshWidth;

    public float velocity;
    public float angle;
    public int resolution;

    float g;
    float radianAngle;

    private void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
        camera = transform.parent.GetComponentInChildren<Camera>().gameObject;
    }

    private void OnValidate() {
        if (mesh != null && Application.isPlaying) {

        }
    }

    // Use this for initialization
    void Start() {
    }

    public void Enable()
    {
        angle = camera.transform.localRotation.eulerAngles.x * -1;
        MakeArcMesh(CalculateArcArray());
    }

    public void Disable()
    {
        TurnOffArc();
    }

    /*private void Update() {

        if(Input.GetButton("Fire2")) {

        } 

        if(Input.GetButtonUp("Fire2")) {
            
        }


    }*/

    void MakeArcMesh(Vector3[] arcVerts) {
        mesh.Clear();
        Vector3[] vertices = new Vector3[(resolution + 1) * 2] ;
        int[] triangles = new int[resolution * 6 * 2];

        for (int i = 0; i <= resolution; i++) {
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);


            if (i != resolution) {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }


        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;



    }

    void TurnOffArc() {
        mesh.vertices = new Vector3[(resolution + 1) * 2];
        mesh.triangles = new int[resolution * 6 * 2];
    }

    Vector3[] CalculateArcArray() {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++) {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance) {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);

    }

}
