using UnityEngine;
using System.Collections;
using FMODUnity;

public class MusicController : MonoBehaviour {

    public string eventName;
    public FMOD.Studio.EventInstance backgroundMusic;
    FMOD.Studio.EventDescription backgroundDescription;

    // Use this for initialization
    void Start() {
        backgroundMusic = RuntimeManager.CreateInstance("event:/Music/" + eventName);
        RuntimeManager.AttachInstanceToGameObject(backgroundMusic, transform, GetComponent<Rigidbody>());
        backgroundMusic.start();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MusicChange") {
            print("ollah");
            eventName = other.GetComponent<MusicContainer>().FMODMusicName;
            backgroundMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            backgroundMusic.release();
            backgroundMusic = RuntimeManager.CreateInstance("event:/Music/" + eventName);
            RuntimeManager.AttachInstanceToGameObject(backgroundMusic, transform, GetComponent<Rigidbody>());
            backgroundMusic.start();
        }
    }
}
