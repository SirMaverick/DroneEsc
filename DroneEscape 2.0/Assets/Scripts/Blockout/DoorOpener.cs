using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DoorOpener : MonoBehaviour {

    private Animator _animator;
    [SerializeField] private float timeBeforeClose;
    [SerializeField] private GameObject Door;
    public bool doorOpen;
    bool doorIsOpen;

    private FMOD.Studio.EventDescription description;

    private FMOD.Studio.EventInstance doorSound;
    private FMOD.Studio.PLAYBACK_STATE playback;
    private float audioLength;
    bool startSound, stopSound;
        
    // Use this for initialization
    void Start () {
        doorSound = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
        RuntimeManager.AttachInstanceToGameObject(doorSound, transform, GetComponent<Rigidbody>());

        _animator = Door.GetComponent<Animator>();
        _animator.SetBool("openDoor", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Drone" || other.tag == "NPCDrone")
        {
            if (doorOpen) {
                _animator.SetBool("openDoor", true);
                StartCoroutine(OpenDoor());
            } else {
                StartCoroutine(DenyDoor());
            }


        }
    }

    private void Update() {
        doorSound.getPlaybackState(out playback);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Drone" || other.tag == "NPCDrone") {
            if(doorOpen)
            StartCoroutine(DoorCloser());
        }
         
    }

    private float GetAudioLength(FMOD.Studio.EventInstance sound) {
        FMOD.Studio.EventDescription _description;
        int _audioLength;
        sound.getDescription(out _description);
        _description.getLength(out _audioLength);

        return _audioLength / 1000.0f;
    }

    IEnumerator DoorCloser() {
        yield return new WaitForSeconds(timeBeforeClose);
        _animator.SetBool("openDoor", false);
        doorSound = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
        RuntimeManager.AttachInstanceToGameObject(doorSound, transform, GetComponent<Rigidbody>());
        doorSound.setParameterValue("MainDoorClose", 1.0f);
        doorSound.setParameterValue("MainDoorOpen", 0.0f);
        audioLength = GetAudioLength(doorSound);
        doorSound.start();
        yield return new WaitForSeconds(audioLength);
        doorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    IEnumerator OpenDoor() {
        if(playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {
            doorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        } StopCoroutine("DoorCloser");
        doorSound = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
        RuntimeManager.AttachInstanceToGameObject(doorSound, transform, GetComponent<Rigidbody>());
        doorSound.setParameterValue("MainDoorClose", 0.0f);
        doorSound.setParameterValue("MainDoorOpen", 1.0f);
        audioLength = GetAudioLength(doorSound);
        doorSound.start();
        
        yield return new WaitForSeconds(audioLength);
        doorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    IEnumerator DenyDoor() {
        if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {
            doorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        doorSound = RuntimeManager.CreateInstance("event:/SFX/Door/DoorDenied");
        RuntimeManager.AttachInstanceToGameObject(doorSound, transform, GetComponent<Rigidbody>());
        doorSound.setParameterValue("DoorDenied", 1.0f);
        audioLength = GetAudioLength(doorSound);
        doorSound.start();
        yield return new WaitForSeconds(audioLength);
        doorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
