using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    #region Refrences
    [Header("Set Transform for the Daily Rotation")]
    [SerializeField]
    private Transform dailyRotation;
    #endregion
    #region variables
    [Header("Time")]
    [Tooltip("Day Length in Minutes")]
    [SerializeField]
    private float _targetDayLength = 1f; //Length of day in minutes
    public bool pause = false;
    [SerializeField]
    private AnimationCurve timeCurve;
    private float timeCurvNormalization;
   /* [SerializeField]*/
    public float elapsedTime;
    [SerializeField]
    private TMP_Text clockText;
    [SerializeField]
    private bool use24hrClock = true;

    [Header("Sun Variables")]
    [SerializeField]
    private Light sun;
    private float intensity;
    [SerializeField]
    private float sunBaseIntensity = 1f;
    [SerializeField]
    private float sunVariation = 1.5f;
    [SerializeField]
    private Gradient sunColor;

    [Header("Seasonal Variables")]
    [SerializeField]
    private Transform sunSeasonalRotation;
    [SerializeField]
    [Range(-45f, 45f)]
    private float maxSeasonalTilt;

    [Header("Modules")]
    [SerializeField]
    private List<DNModuleBase> moduleList = new List<DNModuleBase>();
    #endregion

    #region Target Day Length
    public float targetDayLength
    {
        get
        {
            return _targetDayLength;
        }
    }
    #endregion
    #region Time of Day
    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }
    #endregion
    #region Day Number
    [SerializeField]
    private int _dayNumber = 0;
    public int dayNumber
    {
        get
        {
            return _dayNumber;
        }
    }
    #endregion
    #region Year Number
    [SerializeField]
    private int _yearNumber = 0;
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }
    #endregion
    #region Year Length
    private float _timeScale = 100f;
    [SerializeField]
    private int _yearLength = 100;
    public float yearLength
    {
        get
        {
            return _yearLength;
        }
    }
    #endregion
    #region Time Scale Update
    private void UpdateTimeScale()
    {
        _timeScale = 24 / (_targetDayLength / 60);
        _timeScale += timeCurve.Evaluate(elapsedTime / (targetDayLength * 60));//Changes Time Scale based on time curve
        _timeScale /= timeCurvNormalization;//Keeps day length at target value
    }
    #endregion
    #region Update Time
    public void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400; // Seconds in a day
        elapsedTime += Time.deltaTime;
        if (_timeOfDay > 1)//New Day
        {
            elapsedTime = 0;
            _dayNumber++;
            _timeOfDay -= 1;

            if (_dayNumber > _yearLength)//New Year
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }
    #endregion
    #region Update Clock
    private void UpdateClock()
    {
        float time = elapsedTime / (targetDayLength * 60);
        float hour = Mathf.FloorToInt(time * 24);
        float minute = Mathf.FloorToInt(((time * 24) - hour) * 60);

        string hourString;
        string minuteString;
        
        if (!use24hrClock && hour > 12)
            hour -= 12;

        if (hour < 10)
            hourString = "0" + hour.ToString();
        else
            hourString = hour.ToString();

        if (minute < 10)
            minuteString = "0" + minute.ToString();
        else
            minuteString = minute.ToString();

        if (use24hrClock)
            clockText.text = hourString + " : " + minuteString;
        else if (time > 0.5f)
            clockText.text = hourString + " : " + minuteString + " PM";
        else
            clockText.text = hourString + " : " + minuteString + " AM";


    }
    #endregion
    #region Daily/Seasonal Sun Rotation 
    private void AdjustSunRotation()
    {
        float sunAngle = timeOfDay * 360f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));

        float seasonalAngle = -maxSeasonalTilt * Mathf.Cos(dayNumber / _yearLength * 2f * Mathf.PI);
        sunSeasonalRotation.localRotation = Quaternion.Euler(new Vector3(seasonalAngle, 0f, 0f));
    }
    #endregion
    #region Sun Itensity
    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }
    #endregion
    #region Sun Color Adjustment
    private void AdjustSunColor()
    {
        sun.color = sunColor.Evaluate(intensity);
    }
    #endregion
    #region Normalized Time Curve
    private void NormalizedTimeCurve()
    {
        float stepSize = 0.01f;
        int numberSteps = Mathf.FloorToInt(1f / stepSize);
        float curveTotal = 0;

        for (int i = 0; i < numberSteps; i++)
        {
            curveTotal += timeCurve.Evaluate(i * stepSize);
        }

        timeCurvNormalization = curveTotal / numberSteps;
    }
    #endregion
    #region Add Module
    public void AddModule(DNModuleBase module)
    {
        moduleList.Add(module);
    }
    #endregion
    #region Remove Module
    public void RemoveModule(DNModuleBase module)
    {
        moduleList.Remove(module);
    }
    #endregion
    #region Update Modules
    private void updateModules()
    {
        foreach (DNModuleBase module in moduleList)
        {
            module.UpdateModule(intensity);
        }
    }
    #endregion

    private void Start()
    {
        NormalizedTimeCurve();
    }

    private void Update()
    {
        if(!pause)
        {
            UpdateTimeScale();
            UpdateTime();
            UpdateClock();
        }

        AdjustSunRotation();
        SunIntensity();
        AdjustSunColor();
        updateModules();
    }
}
