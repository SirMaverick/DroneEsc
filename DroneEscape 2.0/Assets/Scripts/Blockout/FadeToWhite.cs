using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class FadeToWhite : MonoBehaviour {

    public static FadeToWhite _instance;
    public static FadeToWhite Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<FadeToWhite>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Fader");
                    _instance = container.AddComponent<FadeToWhite>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private Image whiteImage;
    [SerializeField] private Animator animImage;
    [SerializeField] private float timeTilReset;
    [SerializeField] private float timeTilFade;
    [SerializeField] private int sceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drone")
        {
            CallFading();
        }
    }

    public void CallFading()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        yield return new WaitForSeconds(timeTilFade);
        animImage.SetBool("Fade", true);
        yield return new WaitUntil(() => whiteImage.color.a == 1);
        yield return new WaitForSeconds(timeTilReset);
        SceneManager.LoadScene(sceneIndex);
    }
}
