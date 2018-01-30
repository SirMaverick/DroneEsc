using UnityEngine;
class NewSystemMovementController : MovementController
{
    private GameObject systemArmObject;
    [SerializeField] private float movementSpeed = 5.0f;

    private SystemArm systemArm;
    private NewSystemPlayerController newSystemPlayerController;

    protected override void Start()
    {
        base.Start();
        systemArm = FindObjectOfType<SystemArm>();
        systemArmObject = systemArm.gameObject;
        newSystemPlayerController = GetComponent<NewSystemPlayerController>();
    }


    public override void Horizontal(float direction)
    {
        systemArmObject.transform.Translate(direction * Time.deltaTime * movementSpeed, 0, 0);
    }

    public override void Vertical(float direction)
    {
        systemArmObject.transform.Translate(0, 0, direction * Time.deltaTime * movementSpeed);
    }

    public override void Look(Vector2 md)
    {

    }

    public override void LeftClick(bool key)
    {
        if (key)
        {
            bool success = systemArm.PickUpDrone();
            /*if (success)
            {
                systemArm.RegisterCallBack(newSystemPlayerController);
            }*/
        }
    }
}

