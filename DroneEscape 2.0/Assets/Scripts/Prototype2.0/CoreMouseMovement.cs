using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMouseMovement : MonoBehaviour {

    private Vector2 mouseLook;
    private Vector2 smoothV;
    [SerializeField] private float minClamp;
    [SerializeField] private float maxClamp;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    private Ray ray;
    private RaycastHit hit;

    GameObject character;

    // Use this for initialization
    void Start() {
        character = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update() {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minClamp, maxClamp);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, out hit, 100.0f)) {
                if(hit.collider.tag == "Drone") {
                    hit.collider.gameObject.GetComponent<EmptyDrone>().WalkToPlayer(gameObject.transform);
                }
            }
        }
    }
}
