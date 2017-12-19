using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFadeStart : MonoBehaviour
{

    [SerializeField] private float timeBeforeFade;

    private void Start()
    {
        StartCoroutine(fadeToWhite());
    }

    IEnumerator fadeToWhite()
    {
        yield return new WaitForSeconds(timeBeforeFade);
        FadeToWhite.Instance.CallFading();
    }
}
