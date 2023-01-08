using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DNModuleBase : MonoBehaviour
{
    protected DayNightCycle dayNightControl;

    #region On Enable
    private void OnEnable()
    {
        dayNightControl = this.GetComponent<DayNightCycle>();
        if(dayNightControl != null)
        {
            dayNightControl.AddModule(this);
        }
    }
    #endregion
    #region On Disable
    private void OnDisable()
    {
        if(dayNightControl != null)
        {
            dayNightControl.RemoveModule(this);
        }
    }
    #endregion

    public abstract void UpdateModule(float intensity);

}
