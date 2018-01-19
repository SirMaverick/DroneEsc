using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicPlayer : MonoBehaviour {

    public FMOD.Studio.EventInstance musicInstance;


    public static MusicPlayer _instance;
    public static MusicPlayer Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<MusicPlayer>();

                if (_instance == null) {
                    GameObject musicPlayer = new GameObject("MusicPlayer");
                    _instance = musicPlayer.AddComponent<MusicPlayer>();
                }
            }
            return _instance;
        }
    }

    public void SwitchMusic(string eventName) {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        musicInstance = RuntimeManager.CreateInstance("event:/Music/" + eventName);
        RuntimeManager.AttachInstanceToGameObject(musicInstance, transform, GetComponent<Rigidbody>());
        musicInstance.start();
    }

    public void RemoveMusic() {
        musicInstance.release();
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
