using UnityEngine;
using System.Collections;
using FMODUnity;

public class DroneSounds : MonoBehaviour {


    private FMOD.Studio.EventInstance insertSound;
    private FMOD.Studio.PLAYBACK_STATE playback;
    private FMOD.Studio.EventDescription description;
    private int insertMachineLength;

    public void InsertIntoMachineSound() {
        insertSound = RuntimeManager.CreateInstance("event:/SFX/Core/CoreInsertIntoMachine");
        RuntimeManager.AttachInstanceToGameObject(insertSound, transform, GetComponent<Rigidbody>());
        insertSound.setParameterValue("CoreInsertIn", 1.0f);
        insertSound.getDescription(out description);
        description.getLength(out insertMachineLength);
        insertSound.start();
        StartCoroutine(TurnOffAudio(insertMachineLength));
    }

    public void ExitOutOfMachineSound() {
        insertSound = RuntimeManager.CreateInstance("event:/SFX/Core/CoreInsertIntoMachine");
        RuntimeManager.AttachInstanceToGameObject(insertSound, transform, GetComponent<Rigidbody>());
        insertSound.setParameterValue("CoreInsertOut", 1.0f);
        insertSound.getDescription(out description);
        description.getLength(out insertMachineLength);
        insertSound.start();
        StartCoroutine(TurnOffAudio(insertMachineLength));
    }

    IEnumerator TurnOffAudio(float audioTime) {
        yield return new WaitForSeconds(audioTime / 1000.0f);
        insertSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
