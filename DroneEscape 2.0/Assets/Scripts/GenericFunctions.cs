using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class GenericFunctions : MonoBehaviour {

    string function;
    float targetAlpha;
    float fadeRate;
    float waitTime;
    Image image;
    Color color;
    Canvas canvas;
    bool fadeInOut;
    PostProcessingProfile cameraProfile;
    VignetteModel.Settings tempSettings;
    CRTEffect cameraCRT;
    
    

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
                        if(fadeInOut) {
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
            case "FadeCamera": {
                    if (cameraProfile.vignette.settings.intensity > 0) {
                        tempSettings.intensity -= 0.05f;
                        cameraProfile.vignette.settings = tempSettings;
                    } else {
                        tempSettings.intensity = 0;
                        cameraProfile.vignette.settings = tempSettings;
                        cameraCRT.enabled = false;
                        function = "";
                    }

                    break;
                }
            case "FadeScreenFromCamera": {
                    if (cameraProfile.vignette.settings.intensity < 1) {
                        tempSettings.intensity += 0.05f;
                        cameraProfile.vignette.settings = tempSettings;
                    } else {
                        tempSettings.intensity = 1;
                        cameraProfile.vignette.settings = tempSettings;
                        cameraCRT.enabled = false;
                        SetFade("BlackFadeImage", 0.0f, 0.1f, 0.0f);
                    }

                    break;
                }
            default: {
                    break;
                }
        }
    }

    public void SetFade(string imageNameTemp, float targetAlphaTemp, float fadeRateTemp, float waitStartTime) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        canvas.enabled = true;
        image = GameObject.Find(imageNameTemp).GetComponent<Image>();
        targetAlpha = targetAlphaTemp;
        fadeRate = fadeRateTemp;
        color = image.color;
        StartCoroutine(WaitForFadeIn(waitStartTime));
    }

    public void SetFadeInAndOut(string imageNameTemp, float targetAlphaTemp, float fadeRateTemp, float waitStartTime, float waitTimeTemp) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        image = GameObject.Find(imageNameTemp).GetComponent<Image>();
        targetAlpha = targetAlphaTemp;
        fadeRate = fadeRateTemp;
        color = image.color;
        waitTime = waitTimeTemp;
        fadeInOut = true;
        StartCoroutine(WaitForFadeIn(waitStartTime));
    }

    public void SetFadeOutCamera(string cameraName) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        canvas.enabled = false;
        cameraProfile = GameObject.Find(cameraName).GetComponent<PostProcessingBehaviour>().profile;
        cameraCRT = GameObject.Find(cameraName).GetComponent<CRTEffect>();
        cameraCRT.enabled = true;
        tempSettings = cameraProfile.vignette.settings;
        function = "FadeCamera";
    }

    public void SetFadeInCamera(string cameraName) {
        cameraProfile = GameObject.Find(cameraName).GetComponent<PostProcessingBehaviour>().profile;
        cameraCRT = GameObject.Find(cameraName).GetComponent<CRTEffect>();
        cameraCRT.enabled = true;
        tempSettings = cameraProfile.vignette.settings;
        function = "FadeScreenFromCamera";
    }

    IEnumerator WaitForFadeIn(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        function = "FadeImage";
    }

    IEnumerator WaitForFade(float waitTime) {
        fadeInOut = false;
        targetAlpha = 1 - targetAlpha;
        yield return new WaitForSeconds(waitTime);
        function = "FadeScreen";
    }
}
