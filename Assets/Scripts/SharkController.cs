using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SharkController : MonoBehaviour
{
    [SerializeField] private GameObject ladder;
    [SerializeField] private SplineAnimate sharkSpline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LargeRock"))
        {
            ladder.transform.SetParent(null);
            ladder.GetComponent<Rigidbody>().isKinematic = false;

            StartCoroutine(FloatAndDie());
        }
    }

    private IEnumerator FloatAndDie()
    {
        sharkSpline.enabled = false;

        var duration = 16f;
        
        for (var i = 0f; i < duration; i += Time.deltaTime)
        {
            transform.position += Vector3.up * (1.5f * Time.deltaTime);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
