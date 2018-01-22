using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


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
    [SerializeField] private Transform audioPosition;
    private bool hasBeenSet;

    public FMOD.Studio.EventInstance lockInSound;
    private FMOD.Studio.PLAYBACK_STATE playback;


    private void Start() {
        hasBeenSet = false;
        foreach(GameObject go in showList) {
            go.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {

        
    if ( objectList.Contains(other.gameObject) && hasBeenSet == false) {
            magnetMove.ReleaseOnConveyorClick(other.gameObject);
            StartSoundOnClick();
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
            //gameObject.SetActive(false);
        }
    }

    public bool HasBeenSet() {
        return hasBeenSet;
    }


    private void StartSoundOnClick() {
        print("check");
        lockInSound = RuntimeManager.CreateInstance("event:/SFX/Magnet/Magnet");
        RuntimeManager.AttachInstanceToGameObject(lockInSound, audioPosition, GetComponent<Rigidbody>());
        lockInSound.setParameterValue("MagnetOn", 0.0f);
        lockInSound.setParameterValue("MagnetMovement", 0.0f);
        lockInSound.setParameterValue("MagnetLock", 0.0f);
        lockInSound.setParameterValue("GrabSmall", 0.0f);
        lockInSound.setParameterValue("GrabMedium", 0.0f);
        lockInSound.setParameterValue("GrabBig", 0.0f);
        lockInSound.setParameterValue("DropSmall", 0.0f);
        lockInSound.setParameterValue("DropMedium", 0.0f);
        lockInSound.setParameterValue("magnetDrop", 1.0f);
        lockInSound.setParameterValue("DropBig", 1.0f);
        lockInSound.start();
        StartCoroutine(StopFMODSound(1.5f));
    }

    IEnumerator StopFMODSound(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        lockInSound.release();
        lockInSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
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
