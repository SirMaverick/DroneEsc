using UnityEngine;

public class CorePlayerController : AbstractPlayerController
    {
    [SerializeField]
    private GameObject core;

    public GameObject GetCore()
    {
        return core;
    }


}
