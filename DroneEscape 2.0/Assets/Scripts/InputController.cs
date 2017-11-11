using UnityEngine;
class InputController : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {

        }


        // mouse movement
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }
}
