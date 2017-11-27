using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemUIController : UIController {
    [SerializeField]
    private Slider energyBar;
    [SerializeField]
    private Image energyBarImage;
    [SerializeField]
    private Color minColor;
    [SerializeField]
    private Color maxColor;

    private float maxEnergyLevel;

    public void SetMaxEnergyLevel(float maxEnergy)
    {
        maxEnergyLevel = maxEnergy;
    }

    public void SetEnergyLevelBar(float energyLevel)
    {
        float value = energyBar.maxValue * (energyLevel / maxEnergyLevel);
        energyBar.value = value;
        energyBarImage.color = (value * maxColor) + ((energyBar.maxValue - value) * minColor);
    }
}
