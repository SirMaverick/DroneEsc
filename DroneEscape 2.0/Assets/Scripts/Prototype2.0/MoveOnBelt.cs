using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnBelt : MonoBehaviour {

    public Coroutine currentCoroutine;

    public GameObject[] beltParts;
    public int currentPart = 0;
    public Vector3 movement;
    GameObject nextPart;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject cameraCore;
    bool move;
    public bool sent;
    public bool flying;
    public bool pickedUp;
    public bool start;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (move && !flying && !pickedUp) {
            float step = speed * Time.deltaTime;
            transform.position += movement * Time.deltaTime * speed;
            //transform.Translate(Vector3.left * Time.deltaTime, Space.Self);
            cameraCore.transform.position = transform.position;
        }

    }

    public void StopMoving() {
        StopCoroutine("MoveObject");
        move = false;
    }

    public IEnumerator MoveObject() {
        move = true;
        yield return new WaitForSeconds(0.1f);
        if (start == false) {
            StopMoving();
        } else {
            currentCoroutine = StartCoroutine("MoveObject");
        }
        /*move = true;
        yield return new WaitForSeconds(2.0f);
        move = false;
        currentPart++;
        if (currentPart < beltParts.Length - 1 && !pickedUp) {
            currentCoroutine = StartCoroutine("MoveObject");
        } else {
            sent = false;
        }*/



    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "ConveyorTurn") {
            movement = other.transform.parent.GetComponent<BeltInfo>().movement;
            //movement = other.transform.parent.GetComponent<BeltInfo>().movement;
            //nextPart = beltParts[currentPart + 1];
            //currentPart++;
        }
    }
}
