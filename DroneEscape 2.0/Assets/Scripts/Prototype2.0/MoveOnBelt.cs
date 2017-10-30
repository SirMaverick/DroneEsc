using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnBelt : MonoBehaviour {

    public Coroutine currentCoroutine;

    public GameObject[] beltParts;
    public int currentPart = 0;
    GameObject nextPart;
    [SerializeField] float speed;
    [SerializeField] GameObject cameraCore;
    bool move;
    public bool sent;
    public bool flying;
    public bool pickedUp;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (move && currentPart <= beltParts.Length - 2 && !flying && !pickedUp) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, beltParts[currentPart + 1].transform.position + new Vector3 (0, beltParts[currentPart + 1].transform.localScale.y / 2 + gameObject.transform.localScale.y / 2 * gameObject.GetComponent<BoxCollider>().bounds.size.y, 0), step);
            cameraCore.transform.position = transform.position;
        }
    }

    public void StopMoving() {
        StopCoroutine("MoveObject");
    }

    public IEnumerator MoveObject() { 
        move = true;
        yield return new WaitForSeconds(2.0f);
        move = false;
        currentPart++;
        if (currentPart < beltParts.Length - 1 && !pickedUp) {
            currentCoroutine = StartCoroutine("MoveObject");
        } else {
            sent = false;
        }

        

    }
}
