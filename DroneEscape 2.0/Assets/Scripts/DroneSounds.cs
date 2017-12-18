using UnityEngine;
using System.Collections;
using FMODUnity;

public class DroneSounds : MonoBehaviour {


    private FMOD.Studio.EventInstance actionSound;
    private FMOD.Studio.EventInstance moveSound;
    private FMOD.Studio.PLAYBACK_STATE playback;
    private FMOD.Studio.EventDescription description;
    private int insertMachineLength, shootChargeLength;

    public void InsertIntoMachineSound() {
        actionSound = RuntimeManager.CreateInstance("event:/SFX/Core/CoreInsertIntoMachine");
        RuntimeManager.AttachInstanceToGameObject(actionSound, transform, GetComponent<Rigidbody>());
        actionSound.setParameterValue("CoreInsertIn", 1.0f);
        actionSound.getDescription(out description);
        description.getLength(out insertMachineLength);
        actionSound.start();
        StartCoroutine(TurnOffAudio(insertMachineLength, actionSound));
    }

    public void ExitOutOfMachineSound() {
        actionSound = RuntimeManager.CreateInstance("event:/SFX/Core/CoreInsertIntoMachine");
        RuntimeManager.AttachInstanceToGameObject(actionSound, transform, GetComponent<Rigidbody>());
        actionSound.setParameterValue("CoreInsertOut", 1.0f);
        actionSound.getDescription(out description);
        description.getLength(out insertMachineLength);
        actionSound.start();
        StartCoroutine(TurnOffAudio(insertMachineLength, actionSound));
    }

    public void StartChargeSound() {
        actionSound = RuntimeManager.CreateInstance("event:/SFX/Drone/ChargeAndShoot/DroneCharge");
        RuntimeManager.AttachInstanceToGameObject(actionSound, transform, GetComponent<Rigidbody>());
        actionSound.setParameterValue("Charge!", 1.0f);
        actionSound.setParameterValue("Charge Loop", 1.0f);
        actionSound.getDescription(out description);
        description.getLength(out shootChargeLength);
        actionSound.start();
        StartCoroutine(TurnOffAudio(shootChargeLength, actionSound));
    }

    public void StartShootSound() {
        actionSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        actionSound = RuntimeManager.CreateInstance("event:/SFX/Drone/ChargeAndShoot/DroneShoot");
        RuntimeManager.AttachInstanceToGameObject(actionSound, transform, GetComponent<Rigidbody>());
        actionSound.setParameterValue("Shoot", 1.0f);
        actionSound.getDescription(out description);
        description.getLength(out shootChargeLength);
        actionSound.start();
        StartCoroutine(TurnOffAudio(shootChargeLength, actionSound));
    }

    public void StartMoveSound() {
        moveSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        moveSound = RuntimeManager.CreateInstance("event:/SFX/Drone/Walking/DroneWalk4");
        RuntimeManager.AttachInstanceToGameObject(moveSound, transform, GetComponent<Rigidbody>());
        moveSound.setParameterValue("StartWalking", 1.0f);
        moveSound.setParameterValue("StopWalking", 0.0f);
        moveSound.setParameterValue("KeepWalking", 1.0f);
        moveSound.getDescription(out description);
        description.getLength(out shootChargeLength);
        moveSound.start();
        StartCoroutine(TurnOffParameter(shootChargeLength, "StartWalking", 0.0f, moveSound));
    }

    public void StopMoveSound() {
        StartCoroutine(TurnOffAudio(0, moveSound));
    }

    IEnumerator TurnOffAudio(float audioTime, FMOD.Studio.EventInstance source) {
        yield return new WaitForSeconds(audioTime / 1000.0f);
        source.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    IEnumerator TurnOffParameter(float audioTime, string parameterName, float value, FMOD.Studio.EventInstance source) {
        yield return new WaitForSeconds(audioTime / 1000.0f);
        source.setParameterValue(parameterName, value);
    }


}
