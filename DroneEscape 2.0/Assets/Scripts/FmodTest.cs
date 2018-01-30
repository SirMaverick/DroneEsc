using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodTest : MonoBehaviour {

    FMOD.Studio.EventInstance backgroundMusic;
    FMOD.Studio.ParameterInstance currentHealth;
    FMOD.Studio.PLAYBACK_STATE musicPlaybackState;
    FMOD.Studio.EventDescription description;
    string path;


    private void Awake() {
        backgroundMusic = RuntimeManager.CreateInstance("event:/Core/CoreEmotions");
    }
    // Use this for initialization
    void Start() {
        
    }

    void PlayStep(FMOD.Studio.EventInstance step, FMOD.Studio.PLAYBACK_STATE playState) {
        //step.setParameterValue("Happy", 1.0f);
        step.setParameterValue("Sad", 1.0f);
        //step.setParameterValue("Keep Walking", 1.0f);
        if (playState != FMOD.Studio.PLAYBACK_STATE.PLAYING) {
            RuntimeManager.AttachInstanceToGameObject(step, transform, GetComponent<Rigidbody>());
            step.getDescription(out description);
            description.getPath(out path);
            step.start();
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            backgroundMusic.getPlaybackState(out musicPlaybackState);
            PlayStep(backgroundMusic, musicPlaybackState);
            }
        }
        
    }
