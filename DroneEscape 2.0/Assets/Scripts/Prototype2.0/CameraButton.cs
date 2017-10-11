using UnityEngine;


class CameraButton : MonoBehaviour
{
    private bool inRange = false;
    private bool swapped = false;
    private PlayerCameraController playerCameraController;
    [SerializeField]
    private Camera cam;

    private string tag = "Drone";


    private void Start()
    {
        playerCameraController = FindObjectOfType<PlayerCameraController>();
    }

    private void Update()
    {
        if (inRange)
        {
            if (!swapped)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerCameraController.SwapCamera(cam);
                    swapped = true;
                    Debug.Log("Switching camera");
                    return;
                }
            }
        }
        if (swapped)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerCameraController.SwapCameraToPrevious();
                swapped = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tag)
        { 
            inRange = false;
        }
    }
}
