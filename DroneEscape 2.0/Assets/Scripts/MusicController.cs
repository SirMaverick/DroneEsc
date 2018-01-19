using UnityEngine;
using System.Collections;
using FMODUnity;

public class MusicController : MonoBehaviour {

    public string eventName;
    public FMOD.Studio.EventInstance backgroundMusic;
    FMOD.Studio.EventDescription backgroundDescription;
    GameObject core;

    // Use this for initialization
    void Start() {
        //core = GameObject.Find("Core");
        //backgroundMusic = RuntimeManager.CreateInstance("event:/Music/" + eventName);
        //RuntimeManager.AttachInstanceToGameObject(backgroundMusic, core.transform, core.GetComponent<Rigidbody>());
        //backgroundMusic.start();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MusicChange") {
            eventName = other.GetComponent<MusicContainer>().FMODMusicName;
            MusicPlayer.Instance.SwitchMusic(eventName);
        }
    }

    public void RemoveMusic() {
        backgroundMusic.release();
        backgroundMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
