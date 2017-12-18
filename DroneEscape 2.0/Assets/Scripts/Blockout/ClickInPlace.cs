using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInPlace : MonoBehaviour {

    [SerializeField]
    private List<GameObject> objectList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> showList = new List<GameObject>();
    [SerializeField]
    private MagnetMove magnetMove;
    [SerializeField] private GuardFOV guardFOV;
    [SerializeField] private bool left;
    [SerializeField] private bool right;
    private bool hasBeenSet;

    private void Start() {
        hasBeenSet = false;
        foreach(GameObject go in showList) {
            go.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {

        
    if ( objectList.Contains(other.gameObject) && hasBeenSet == false) {
            magnetMove.ReleaseOnConveyorClick(other.gameObject);
            other.gameObject.tag = "Untagged";
            other.gameObject.SetActive(false);
            foreach(GameObject go in showList) {
                go.SetActive(true);
            }
            hasBeenSet = true;
            if(guardFOV != null)
            {
                if (left)
                {
                    guardFOV.BlockLeft();
                }else if (right)
                {
                    guardFOV.BlockRight();
                }
            }
            gameObject.SetActive(false);
        }
    }

    public bool HasBeenSet() {
        return hasBeenSet;
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Magnetic" && hasBeenSet == true)
        {
            rb.isKinematic = false;
            hasBeenSet = false;
        }
    }
    */
}
