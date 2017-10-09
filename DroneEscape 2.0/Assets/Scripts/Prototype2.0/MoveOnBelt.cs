using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnBelt : MonoBehaviour {

    public Coroutine currentCoroutine;

    public GameObject[] beltParts;
    public int currentPart = 0;
    GameObject nextPart;
    [SerializeField] float speed;
    bool move;
    public bool sent;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (move && currentPart <= beltParts.Length - 2) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, beltParts[currentPart + 1].transform.position, step);
        }
    }

    public IEnumerator MoveObject() { 
        move = true;
        yield return new WaitForSeconds(2.0f);
        move = false;
        currentPart++;
        if (currentPart < beltParts.Length - 1) {
            currentCoroutine = StartCoroutine(MoveObject());
        }

        

    }
}
