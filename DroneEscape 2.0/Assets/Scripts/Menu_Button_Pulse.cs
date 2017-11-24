using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Button_Pulse : MonoBehaviour {

    public float maxScale;
    float step;


	// Use this for initialization
	void Start () {
        step = maxScale / 6 ;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0.3f, 1.0f, 0.15f);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.localScale.x > maxScale)
        {
            gameObject.transform.localScale = new Vector3(step / 2 * Time.deltaTime, step / 2 * Time.deltaTime, step / 2 * Time.deltaTime);

        }
        else
        {
            gameObject.transform.localScale += new Vector3(step * Time.deltaTime, step * Time.deltaTime, step * Time.deltaTime);
            Color oldColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, (maxScale - gameObject.transform.localScale.x) / maxScale);
        }
    }
}
