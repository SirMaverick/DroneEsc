using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class FadeToWhite : MonoBehaviour {

    [SerializeField] private Image whiteImage;
    [SerializeField] private Animator animImage;
    [SerializeField] private Animator animText;
    [SerializeField] private float timeTilReset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drone")
        {
            StartCoroutine(Fading());   
        }
    }

    IEnumerator Fading()
    {
        animImage.SetBool("Fade", true);
        yield return new WaitUntil(() => whiteImage.color.a == 1);
        animText.SetBool("FadeText", true);
        yield return new WaitForSeconds(timeTilReset);
        Application.LoadLevel(Application.loadedLevel);
    }
}
