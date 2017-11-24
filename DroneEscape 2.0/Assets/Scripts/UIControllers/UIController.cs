using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public void EnableController()
    {
        canvas.enabled = true;
    }

    public void DisableController()
    {
        canvas.enabled = false;
    }
}
