using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFade : MonoBehaviour {

	[SerializeField] private float timeBeforeFade;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(fadeToWhite());
    }

    IEnumerator fadeToWhite()
    {
        yield return new WaitForSeconds(timeBeforeFade);
        FadeToWhite.Instance.CallFading();
    }
}
