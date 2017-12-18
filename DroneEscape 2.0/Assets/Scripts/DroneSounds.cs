using UnityEngine;
using System.Collections;
using FMODUnity;

public class DroneSounds : MonoBehaviour {


    private FMOD.Studio.EventInstance playSound;
    private FMOD.Studio.PLAYBACK_STATE playback;
    private FMOD.Studio.EventDescription description;
    private int insertMachineLength, shootChargeLength;

    public void InsertIntoMachineSound() {
        playSound = RuntimeManager.CreateInstance("event:/SFX/Core/CoreInsertIntoMachine");
        RuntimeManager.AttachInstanceToGameObject(playSound, transform, GetComponent<Rigidbody>());
        playSound.setParameterValue("CoreInsertIn", 1.0f);
        playSound.getDescription(out description);
        description.getLength(out insertMachineLength);
        playSound.start();
        StartCoroutine(TurnOffAudio(insertMachineLength));
    }

    public void ExitOutOfMachineSound() {
        playSound = RuntimeManager.CreateInstance("event:/SFX/Core/CoreInsertIntoMachine");
        RuntimeManager.AttachInstanceToGameObject(playSound, transform, GetComponent<Rigidbody>());
        playSound.setParameterValue("CoreInsertOut", 1.0f);
        playSound.getDescription(out description);
        description.getLength(out insertMachineLength);
        playSound.start();
        StartCoroutine(TurnOffAudio(insertMachineLength));
    }

    public void StartChargeSound() {
        playSound = RuntimeManager.CreateInstance("event:/SFX/Drone/ChargeAndShoot/DroneCharge");
        RuntimeManager.AttachInstanceToGameObject(playSound, transform, GetComponent<Rigidbody>());
        playSound.setParameterValue("Charge!", 1.0f);
        playSound.setParameterValue("Charge Loop", 1.0f);
        playSound.getDescription(out description);
        description.getLength(out shootChargeLength);
        playSound.start();
        StartCoroutine(TurnOffAudio(shootChargeLength));
    }

    public void StartShootSound() {
        playSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        playSound = RuntimeManager.CreateInstance("event:/SFX/Drone/ChargeAndShoot/DroneShoot");
        RuntimeManager.AttachInstanceToGameObject(playSound, transform, GetComponent<Rigidbody>());
        playSound.setParameterValue("Shoot", 1.0f);
        playSound.getDescription(out description);
        description.getLength(out shootChargeLength);
        playSound.start();
        StartCoroutine(TurnOffAudio(shootChargeLength));
    }

    IEnumerator TurnOffAudio(float audioTime) {
        yield return new WaitForSeconds(audioTime / 1000.0f);
        playSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }


}
