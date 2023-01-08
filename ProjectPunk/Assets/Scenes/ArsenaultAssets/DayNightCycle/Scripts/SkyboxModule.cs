using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxModule : DNModuleBase
{
    #region Variables
    [Header("Sky and Horizon Color")]
    [SerializeField]
    private Gradient skyColor;
    [SerializeField]
    private Gradient horizonColor;
    #endregion

    public override void UpdateModule(float intensity)
    {
        RenderSettings.skybox.SetColor("_skyTint", skyColor.Evaluate(intensity));
        RenderSettings.skybox.SetColor("_GroundColor", horizonColor.Evaluate(intensity));
    }
}
