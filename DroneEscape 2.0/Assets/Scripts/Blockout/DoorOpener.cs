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
    bool startClose;

    private FMOD.Studio.EventDescription description;

    private FMOD.Studio.EventInstance doorSoundOpen, doorSoundClose, doorSoundDeny;
    private FMOD.Studio.PLAYBACK_STATE playbackOpen, playbackClose, playbackDeny;
    private float audioLengthOpen, audioLengthClose, audioLengthDeny;
    bool startSound, stopSound;
        
    // Use this for initialization
    void Start () {
        doorSoundOpen = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
        RuntimeManager.AttachInstanceToGameObject(doorSoundOpen, transform, GetComponent<Rigidbody>());
        doorSoundClose = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
        RuntimeManager.AttachInstanceToGameObject(doorSoundClose, transform, GetComponent<Rigidbody>());

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
        doorSoundOpen.getPlaybackState(out playbackOpen);
        doorSoundClose.getPlaybackState(out playbackClose);
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
        startClose = true;
        yield return new WaitForSeconds(timeBeforeClose);
        if (startClose) {
            doorIsOpen = false;
            _animator.SetBool("openDoor", false);
            //doorSoundClose = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
            if (playbackClose == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

            } else {
                RuntimeManager.AttachInstanceToGameObject(doorSoundClose, transform, GetComponent<Rigidbody>());
                doorSoundClose.setParameterValue("MainDoorClose", 1.0f);
                doorSoundClose.setParameterValue("MainDoorOpen", 0.0f);
                audioLengthClose = GetAudioLength(doorSoundClose);
                doorSoundClose.start();

                yield return new WaitForSeconds(audioLengthClose);
                doorSoundClose.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }

    }

    IEnumerator OpenDoor() {
        startClose = false;
        if(!doorIsOpen) {
            doorIsOpen = true;
            if (playbackClose == FMOD.Studio.PLAYBACK_STATE.PLAYING) {
                doorSoundClose.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
            StopCoroutine("DoorCloser");

            //doorSoundOpen = RuntimeManager.CreateInstance("event:/SFX/Door/DoorOpenAndClose");
            if (playbackOpen == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

            } else {
                RuntimeManager.AttachInstanceToGameObject(doorSoundOpen, transform, GetComponent<Rigidbody>());
                doorSoundOpen.setParameterValue("MainDoorClose", 0.0f);
                doorSoundOpen.setParameterValue("MainDoorOpen", 1.0f);
                audioLengthOpen = GetAudioLength(doorSoundOpen);
                doorSoundOpen.start();

                yield return new WaitForSeconds(audioLengthOpen);
                doorSoundOpen.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
    }

    IEnumerator DenyDoor() {
        if (playbackDeny == FMOD.Studio.PLAYBACK_STATE.PLAYING) {
            doorSoundDeny.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        doorSoundDeny = RuntimeManager.CreateInstance("event:/SFX/Door/DoorDenied");
        RuntimeManager.AttachInstanceToGameObject(doorSoundDeny, transform, GetComponent<Rigidbody>());
        doorSoundDeny.setParameterValue("DoorDenied", 1.0f);
        audioLengthDeny = GetAudioLength(doorSoundDeny);
        doorSoundDeny.start();
        yield return new WaitForSeconds(audioLengthDeny);
        doorSoundDeny.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
