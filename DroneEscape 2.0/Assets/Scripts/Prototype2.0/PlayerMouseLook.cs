using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour {

    private Vector2 mouseLook;
    private Vector2 smoothV;
    [SerializeField] private float minClamp;
    [SerializeField] private float maxClamp;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    [SerializeField] private float selectionRange = 1.5f;

    GameObject character;

    private Material lastMaterialHit;
    private bool hitButton = false;

	// Use this for initialization
	void Start () {
        character = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, selectionRange))
        {
            if (hit.collider.tag == "Button")
            {
                hitButton = true;
                lastMaterialHit = hit.collider.gameObject.GetComponent<MeshRenderer>().material;
                EnableEmission(0);
                //hit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0));
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<CameraDisableButton>().ToggleEnableCamera();
                    lastMaterialHit.DisableKeyword("_EMISSION");
                    //StartCoroutine(EnableEmission(1));
                }

            }
            else
            {
                if (hitButton)
                {
                    lastMaterialHit.DisableKeyword("_EMISSION");
                    hitButton = false;
                }

            }
        }
        else
        {
            if (hitButton)
            {
                lastMaterialHit.DisableKeyword("_EMISSION");
                hitButton = false;
            }

        }
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minClamp, maxClamp);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
		
	}

    private IEnumerator EnableEmission(int seconds) {
        yield return new WaitForSeconds(seconds);
        lastMaterialHit.EnableKeyword("_EMISSION");
    }
}
