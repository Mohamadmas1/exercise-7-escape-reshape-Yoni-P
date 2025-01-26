using System;
using FMODUnity;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private MissionData[] missionData;
    [SerializeField] private StudioGlobalParameterTrigger progressParameterTrigger;

    private float timeForNextHint;
    private float timeSinceLastHint;
    
    private int currentMissionIndex = 0;
    
    private float progress = 0;

    private void Start()
    {
        timeForNextHint = GameTimer.Instance.GameTime / (missionData.Length + 1);
        
        progressParameterTrigger.Value = progress;
        progressParameterTrigger.TriggerParameters();

        foreach (var missionData in missionData)
        {
            missionData.OnMissionSolved += Progress;
        }
    }
    
    
    private void Progress()
    {
        progress += 1f;
        progressParameterTrigger.Value = progress;
        progressParameterTrigger.TriggerParameters();
    }

    private void Update()
    {
        timeSinceLastHint += Time.deltaTime;
        if (timeSinceLastHint >= timeForNextHint)
        {
            timeSinceLastHint = 0;
            ShowNextHint();
        }
    }

    private void ShowNextHint()
    {
        if (currentMissionIndex >= missionData.Length)
        {
            Debug.Log("All hints shown");
            return;
        }
        missionData[currentMissionIndex].ShowHint();
        currentMissionIndex++;
    }
}
