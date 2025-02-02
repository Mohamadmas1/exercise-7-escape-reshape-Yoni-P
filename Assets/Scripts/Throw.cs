using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Throw : MonoBehaviour
{
    [SerializeField] private Queue<Vector3> positionHistory = new();
    [SerializeField] private int historySize = 10;
    public float boost = 2f;
    [SerializeField] private StudioEventEmitter hitEmitter;

    private void Update()
    {
            if (!GetComponent<XRGrabInteractable>().isSelected) return;

        if (positionHistory.Count < historySize)
        {
            positionHistory.Enqueue(transform.position);
        }
        else
        {
            positionHistory.Dequeue();
            positionHistory.Enqueue(transform.position);
        }
    }

    public void OnThrow()
    {
        // calculate the average throw direction
        if (positionHistory.Count > 1)
        {
            // iterate through the position history in pairs
            var throwDirection = Vector3.zero;
            var previousPosition = positionHistory.Dequeue();
            foreach (var position in positionHistory)
            {
                throwDirection += position - previousPosition;
                previousPosition = position;
            }
            throwDirection /= positionHistory.Count;

            // apply the throw force
            GetComponent<Rigidbody>().AddForce(throwDirection * boost, ForceMode.Impulse);
        }

        // clear the position history
        positionHistory.Clear();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (Time.timeSinceLevelLoad < 0.1f) return;
        if (hitEmitter != null)
            hitEmitter.Play();
    }
}
