using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonModule : DNModuleBase
{
    #region Variables
    [Header("Moon: Source/Gradiant/Intensity")]
    [SerializeField]
    private Light moon;
    [SerializeField]
    private Gradient moonColor;
    [SerializeField]
    private float baseIntensity;
    #endregion

    public override void UpdateModule(float intensity)
    {
        moon.color = moonColor.Evaluate(1 - intensity);
        moon.intensity = (1 - intensity) * baseIntensity + 0.05f;
    }
    
}
