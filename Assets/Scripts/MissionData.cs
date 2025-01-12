using System;
using UnityEngine;


public class MissionData : MonoBehaviour
{
        public Transform[] hintPositions;
        
        // Calls the OnMissionSolved action when the mission is solved
        public bool Solved
        {
                get => _solved;
                set
                {
                        _solved = value;
                        if (_solved)
                        {
                                OnMissionSolved?.Invoke();
                        }
                }
        }

        public Action OnMissionSolved;
        private bool _solved = false;
}
