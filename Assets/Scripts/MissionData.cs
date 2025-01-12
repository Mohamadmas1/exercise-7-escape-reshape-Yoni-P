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
                                DestroyHints();
                        }
                }
        }

        private void DestroyHints()
        {
                foreach (var hintPosition in hintPositions)
                {
                        Destroy(hintPosition.gameObject);
                }
                OnMissionSolved?.Invoke();
        }

        public Action OnMissionSolved;
        private bool _solved = false;


        public void ShowHint()
        {
                Debug.Log("Showing hint of " + gameObject.name);
                if (Solved) return;
                Debug.Log("Showed hint of " + gameObject.name);
                foreach (var hintPosition in hintPositions)
                {
                        hintPosition.gameObject.SetActive(true);
                }
        }
}
