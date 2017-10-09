using UnityEngine;

class PlayerCameraController : MonoBehaviour
{
    Camera previousCamera;
    Camera activeCamera;

    private void Start()
    {
        activeCamera = Camera.main;
    }

    public void SwapCamera(Camera camera)
    {
        camera.enabled = true;
        previousCamera = activeCamera;
        activeCamera.enabled = false;
        activeCamera = camera;
    }

    public void SwapCameraToPrevious()
    {
        previousCamera.enabled = true;
        activeCamera.enabled = false;
        Camera temp = previousCamera;
        previousCamera = activeCamera;
        activeCamera = temp;
    }


}

