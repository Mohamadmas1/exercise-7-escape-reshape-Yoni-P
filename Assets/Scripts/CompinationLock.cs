using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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
    [SerializeField] private TextMeshPro[] digitTexts;
    [SerializeField] private MissionData suitCaseHints;     

    public void OnActivate(int index) {
        if (!isLocked) return;
        var num = (digits[index] + 1) % 10;
        digits[index] = num;
        digitTexts[index].text = num.ToString();
        FadeInAndOutText(index);
        Debug.Log("Digit " + index + " set to " + digits[index]);
    }

    private void FadeInAndOutText(int index)
    {
        var text = digitTexts[index];
        
        // kill any previous tweens
        text.DOKill();
        
        text.alpha = 1;
        text.DOFade(0, 0.5f).SetDelay(0.5f);
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
            if (suitCaseHints != null)
            {
                suitCaseHints.Solved = true;
            }
            Debug.Log("Lock is open!");
        }
    }
}
