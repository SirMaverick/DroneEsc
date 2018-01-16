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
                        //print(color.a);
                    } else {
                        color.a = targetAlpha;
                        image.color = color;
                        if(fadeInOut) {
                            fadeInOut = false;

                            targetAlpha = 1 - targetAlpha;
                            StartCoroutine(WaitForFade(waitTime, "FadeScreen"));
                        }
                        function = "";
                    }
                    break;
                }
            case "FadeScreen": {
                    
                    if (color.a > targetAlpha) {
                        color.a -= (fadeRate * Time.deltaTime);
                        image.color = color;
                        print(targetAlpha);
                        print(color.a);
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
                        SetFadeOut("BlackFadeImage", 0.0f, 1f, 0.0f);
                        function = "";
                        
                    }

                    break;
                }
            default: {
                    break;
                }
        }
    }

    public void SetFadeOut(string imageNameTemp, float targetAlphaTemp, float fadeRateTemp, float waitStartTime) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        canvas.enabled = true;
        image = GameObject.Find(imageNameTemp).GetComponent<Image>();
        targetAlpha = targetAlphaTemp;
        fadeRate = fadeRateTemp;
        color = image.color;
        print(color.a);
        StartCoroutine(WaitForFade(waitStartTime, "FadeScreen"));
    }

    public void SetFadeIn(string imageNameTemp, float targetAlphaTemp, float fadeRateTemp, float waitStartTime) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        canvas.enabled = true;
        image = GameObject.Find(imageNameTemp).GetComponent<Image>();
        targetAlpha = targetAlphaTemp;
        fadeRate = fadeRateTemp;
        color = image.color;
        StartCoroutine(WaitForFade(waitStartTime, "FadeImage"));
    }

    public void SetFadeInAndOut(string imageNameTemp, float targetAlphaTemp, float fadeRateTemp, float waitStartTime, float waitTimeTemp) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        image = GameObject.Find(imageNameTemp).GetComponent<Image>();
        targetAlpha = targetAlphaTemp;
        fadeRate = fadeRateTemp;
        color = image.color;
        waitTime = waitTimeTemp;
        fadeInOut = true;
        StartCoroutine(WaitForFade(waitStartTime, "FadeImage"));
    }

    public void SetFadeOutCamera(Camera cameraName) {
        canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        canvas.enabled = false;
        cameraProfile = cameraName.GetComponent<PostProcessingBehaviour>().profile;
        cameraCRT = cameraName.GetComponent<CRTEffect>();
        cameraCRT.enabled = true;
        tempSettings = cameraProfile.vignette.settings;
        function = "FadeCamera";
    }

    public void SetFadeInCamera(Camera cameraName) {
        cameraProfile = cameraName.GetComponent<PostProcessingBehaviour>().profile;
        cameraCRT = cameraName.GetComponent<CRTEffect>();
        cameraCRT.enabled = true;
        tempSettings = cameraProfile.vignette.settings;
        function = "FadeScreenFromCamera";
    }

    IEnumerator WaitForFade(float waitTime, string functionName) {
        yield return new WaitForSeconds(waitTime);
        function = functionName;
    }
}
