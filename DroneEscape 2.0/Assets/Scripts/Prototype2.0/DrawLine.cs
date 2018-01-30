using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {
    private LineRenderer line;
    private bool isMousePressed;
    private List<Vector3> pointsList;
    private Vector3 mousePos;
    private float lineLength;
    private float drawnLineLength;

    public Material material;

    // Structure for line points
    struct myLine {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };
    //	-----------------------------------	
    void Awake() {
        // Create line renderer component and set its property
        line = gameObject.AddComponent<LineRenderer>();
        //line.material = material;
        line.positionCount = 0;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.useWorldSpace = true;
        isMousePressed = false;
        pointsList = new List<Vector3>();
        //		renderer.material.SetTextureOffset(
    }
    //	-----------------------------------	
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            drawnLineLength = 0;
            isMousePressed = true;
            line.positionCount = 0;
            pointsList.RemoveRange(0, pointsList.Count);
            line.startColor = Color.green;
            line.endColor = Color.green;
        } else if (Input.GetMouseButtonUp(0)) {
           // line.Simplify(0.1f);
            isMousePressed = false;
            for (int i = 1; i < line.positionCount; i++) {
                drawnLineLength += (line.GetPosition(i) - line.GetPosition(i - 1)).magnitude;
            }
        }
        if (isMousePressed) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            if (!pointsList.Contains(mousePos)) {
                pointsList.Add(mousePos);
                line.positionCount = pointsList.Count;
                line.SetPosition(pointsList.Count - 1, pointsList[pointsList.Count - 1]);
                if (pointsList.Count > 1)
                AddColliderToLine(line, line.GetPosition(pointsList.Count - 1), line.GetPosition(pointsList.Count - 2));
            }
        }
    }
    private void AddColliderToLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint) {
        BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
        //lineCollider.gameObject.AddComponent<Rigidbody>().useGravity = false;
        lineCollider.gameObject.tag = "DrawnLine";
        lineCollider.transform.parent = line.transform;
        
        float lineWidth = line.endWidth;
        float lineLength = Vector3.Distance(startPoint, endPoint);
        lineCollider.size = new Vector3(lineLength, 0.1f, lineWidth);
        Vector3 midPoint = (startPoint + endPoint) / 2;
        lineCollider.transform.position = midPoint;
        float angle = Mathf.Atan2((endPoint.y - startPoint.y), (endPoint.x - startPoint.x));
        angle *= Mathf.Rad2Deg;
        //angle *= -1 ;
        lineCollider.transform.Rotate(0, 0, angle);
    }

    public void AtTriggerEnter() {
        line.Simplify(0.1f);
    }
}