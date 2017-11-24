﻿using UnityEngine;
[System.Serializable]
public abstract class AbstractPlayerController : MonoBehaviour
{
    [SerializeField]
    protected Camera camera;

    [SerializeField]
    protected MovementController movementController;

    [SerializeField]
    protected UIController uiController;

    protected virtual void Start()
    {
        if (camera.enabled)
        {
            PlayerControllerSupervisor.GetInstance().SetCurrentPlayerController(this);
        }
    }

    public virtual void EnableController()
    {
        // meshRenderer.enabled = true;
        camera.enabled = true;
        movementController.enabled = true;
        movementController.EnableController();
        camera.GetComponent<AudioListener>().enabled = true;
        uiController.EnableController();
    }

    public virtual void DisableController()
    {
        // meshRenderer.enabled = false;
        camera.enabled = false;
        movementController.DisableController();
        movementController.enabled = false;
        camera.GetComponent<AudioListener>().enabled = false;

        uiController.DisableController();
    }

}

