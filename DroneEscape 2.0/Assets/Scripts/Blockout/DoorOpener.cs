using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DoorOpener : MonoBehaviour {

    private Animator _animator;
    [SerializeField] private float timeBeforeClose;
    [SerializeField] private GameObject Door;

    private FMOD.Studio.EventDescription description;

    private FMOD.Studio.EventInstance doorSound;
    private FMOD.Studio.PLAYBACK_STATE playback;
    private int audioLength;
    bool startSound, stopSound;
        
    // Use this for initialization
    void Start () {
        doorSound = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
        RuntimeManager.AttachInstanceToGameObject(doorSound, transform, GetComponent<Rigidbody>());

        doorSound.getDescription(out description);
        description.getLength(out audioLength);
        _animator = Door.GetComponent<Animator>();
        _animator.SetBool("openDoor", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Drone" || other.tag == "NPCDrone")
        {
            _animator.SetBool("openDoor", true);
            StartCoroutine(OpenDoor(audioLength));

        }
    }

    private void Update() {
        doorSound.getPlaybackState(out playback);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Drone" || other.tag == "NPCDrone") {
            StartCoroutine(DoorCloser(audioLength));
        }
         
    }

    IEnumerator DoorCloser(float audioTime)
    {

        yield return new WaitForSeconds(timeBeforeClose);
        _animator.SetBool("openDoor", false);
        RuntimeManager.AttachInstanceToGameObject(doorSound, transform, GetComponent<Rigidbody>());
        doorSound.setParameterValue("MainDoorClose", 1.0f);
        doorSound.setParameterValue("MainDoorOpen", 0.0f);
        doorSound.start();
        yield return new WaitForSeconds(audioTime / 1000.0f);
        doorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    IEnumerator OpenDoor(float audioTime) {
        RuntimeManager.AttachInstanceToGameObject(doorSound, transform, GetComponent<Rigidbody>());
        doorSound.setParameterValue("MainDoorClose", 0.0f);
        doorSound.setParameterValue("MainDoorOpen", 1.0f);
        doorSound.start();
        yield return new WaitForSeconds(audioTime / 1000.0f);
        doorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
