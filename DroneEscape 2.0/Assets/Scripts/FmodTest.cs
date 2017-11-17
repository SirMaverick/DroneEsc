using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodTest : MonoBehaviour {

    FMOD.Studio.EventInstance backgroundMusic;
    FMOD.Studio.ParameterInstance currentHealth;


    private void Awake() {
        backgroundMusic = RuntimeManager.CreateInstance("event:/Drone/Walking/DroneWalk1");
    }
    // Use this for initialization
    void Start() {
        
    }

    void PlayStep(FMOD.Studio.EventInstance step, FMOD.Studio.PLAYBACK_STATE playState) {
        step.setParameterValue("Start walking", 1);
        step.setParameterValue("Stop Walking", 1);
        step.setParameterValue("Keep Walking", 1);
        if (playState != FMOD.Studio.PLAYBACK_STATE.PLAYING) {
            print(FMOD.Studio.PLAYBACK_STATE.PLAYING);
            step.start();
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayStep(backgroundMusic, FMOD.Studio.PLAYBACK_STATE.PLAYING);
            }
        }
        
    }
