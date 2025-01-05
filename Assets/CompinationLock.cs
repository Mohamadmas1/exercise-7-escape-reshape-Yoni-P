using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompinationLock : MonoBehaviour
{
    [SerializeField] private int[] correctCombination = new int[3];
    [SerializeField] private int[] digits = new int[3];
    [SerializeField] private Animator[] digitAnimators = new Animator[3];
    [SerializeField] private float animationSpeed = 10;
    private bool isLocked = true;
    [SerializeField] private Animator suitcaseAnimator;

    public void OnActivate(int index) {
        if (!isLocked) return;
        digits[index] = (digits[index] + 1) % 10;
        Debug.Log("Digit " + index + " set to " + digits[index]);
    }

    private void Update ()
    {
        for (int i = 0; i < 3; i++)
        {
            float animatorDigit = digitAnimators[i].GetFloat("Digit");
            animatorDigit = Mathf.Lerp(animatorDigit, digits[i] / 9.0f, Time.deltaTime * animationSpeed);
            digitAnimators[i].SetFloat("Digit", animatorDigit);
        }

        if (isLocked && digits[0] == correctCombination[0] && digits[1] == correctCombination[1] && digits[2] == correctCombination[2])
        {
            isLocked = false;
            suitcaseAnimator.SetTrigger("Open");
            Debug.Log("Lock is open!");
        }
    }
}
