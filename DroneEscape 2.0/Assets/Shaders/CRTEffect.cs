﻿using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

[ExecuteInEditMode]
public class CRTEffect : ImageEffectBase {
    public Texture2D displacementMap;
    float glitchup, glitchdown, flicker;
    float glitchupTime = 0.05f, glitchdownTime = 0.05f, flickerTime = 0.5f;

    [Header("Glitch Intensity")]

    [Range(0, 10)]
    public float intensity;

    [Range(0, 10)]
    public float flipIntensity;

    [Range(0, 10)]
    public float colorIntensity;

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination) {

        material.SetFloat("_Intensity", intensity);
        material.SetFloat("_ColorIntensity", colorIntensity);
        material.SetTexture("_DispTex", displacementMap);

        flicker += Time.deltaTime * colorIntensity;
        if (flicker > flickerTime) {
            material.SetFloat("filterRadius", Random.Range(-2f, 2f) * colorIntensity);
            material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * colorIntensity, Vector3.forward) * Vector4.one);
            flicker = 0;
            flickerTime = Random.value;
        }

        if (colorIntensity == 0)
            material.SetFloat("filterRadius", 0);

        glitchup += Time.deltaTime * flipIntensity;

        if (glitchup > glitchupTime) {
            if (Random.value < 0.2f * flipIntensity)
                material.SetFloat("flip_up", Random.Range(0, 1f) * flipIntensity);
            else
                material.SetFloat("flip_up", 0);

            glitchup = 0;
            glitchupTime = Random.value / 10f;
        }

        if (flipIntensity == 0)
            material.SetFloat("flip_up", 0);


        glitchdown += Time.deltaTime * flipIntensity;
        if (glitchdown > glitchdownTime) {
            if (Random.value < 0.1f * flipIntensity)
                material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * flipIntensity);
            else
                material.SetFloat("flip_down", 1);

            glitchdown = 0;
            glitchdownTime = Random.value / 10f;
        }

        if (flipIntensity == 0)
            material.SetFloat("flip_down", 1);

        if (Random.value < 0.05 * intensity) {
            material.SetFloat("displace", Random.value * intensity);
            material.SetFloat("scale", 1 - Random.value * intensity);
        } else
            material.SetFloat("displace", 0);

        Graphics.Blit(source, destination, material);
    }
}