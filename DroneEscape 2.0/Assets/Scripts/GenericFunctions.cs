using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericFunctions : MonoBehaviour {

    string function;
    float targetAlpha;
    float fadeRate;
    float waitTime;
    Image image;
    Color color;
    Canvas canvas;
    bool fadeInOut;

    public static GenericFunctions _instance;
    public static GenericFunctions Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GenericFunctions>();

                if (_instance == null) {
                    GameObject container = new GameObject("FunctionContainer");
                    _instance = container.AddComponent<GenericFunctions>();
                }
            }
            return _instance;
        }
    }

    private void Update() {
        switch (function) {
            case "FadeImage": {
                    if (color.a < targetAlpha) {
                        color.a += 1.0f / fadeRate * Time.deltaTime;
                        image.color = color;
                    } else {
                        color.a = targetAlpha;
                        image.color = color;
                        print("hallo??");
                        if(fadeInOut) {
                            print("allah");
                            StartCoroutine(WaitForFade(waitTime));
                        }
                        function = "";
                    }
                    break;
                }
            case "FadeScreen": {
                    if (color.a > targetAlpha) {
                        color.a -= 1.0f / fadeRate * Time.deltaTime;
                        image.color = color;
                    } else {
                        color.a = targetAlpha;
                        image.color = color;
                        function = "";
                    }
                    break;
                }
            default: {
                    break;
                }
        }
    }

    public void SetFade(string imageNameTemp, float targetAlphaTemp, float fadeRateTemp) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        image = GameObject.Find(imageNameTemp).GetComponent<Image>();
        targetAlpha = targetAlphaTemp;
        fadeRate = fadeRateTemp;
        color = image.color;
        function = "FadeImage";
    }

    public void SetFadeInAndOut(string imageNameTemp, float targetAlphaTemp, float fadeRateTemp, float waitTimeTemp) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        image = GameObject.Find(imageNameTemp).GetComponent<Image>();
        targetAlpha = targetAlphaTemp;
        fadeRate = fadeRateTemp;
        color = image.color;
        waitTime = waitTimeTemp;
        fadeInOut = true;
        function = "FadeImage";
    }

    IEnumerator WaitForFade(float waitTime) {
        print("ohaio");
        fadeInOut = false;
        targetAlpha = 1 - targetAlpha;
        yield return new WaitForSeconds(waitTime);
        function = "FadeScreen";
    }
}
