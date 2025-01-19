using System;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private MissionData[] missionData;

    private float timeForNextHint;
    private float timeSinceLastHint;
    
    private int currentMissionIndex = 0;

    private void Start()
    {
        timeForNextHint = GameTimer.Instance.GameTime / (missionData.Length + 1);
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
