using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetButton : Button {

    /*[SerializeField]
    private GameObject magnet;
    public Camera surveillanceCamera;
    public float speed = 2;
    public bool coreInside;
    public GameObject drone;*/

    [SerializeField]
    private MagnetPlayerController playerController;


    public override void Toggle()
    {
        /*  if (!enabled)
          {
              playerControllerSupervisor.SwitchPlayerController(playerController);
              enabled = true;
          }
          else
          {
              playerControllerSupervisor.SwitchPlayerControllerPrevious();

              enabled = false;
          }
          */
        playerControllerSupervisor.SwitchPlayerController(playerController);
    }
}
