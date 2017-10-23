using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagonistArmMover : MonoBehaviour
{
    [SerializeField] public float moveDistance;
    private Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(startPos, transform.position) <= moveDistance)
        {
            this.transform.Translate(Vector3.down * 0.5f, Space.World);
        }
    }
}