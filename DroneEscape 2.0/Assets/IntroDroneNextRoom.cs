using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroDroneNextRoom : MonoBehaviour {

    private PlayableDirector director;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
    }
    public void Throw()
    {
        director.Play();
    }

}
