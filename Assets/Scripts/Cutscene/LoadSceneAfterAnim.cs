using System.Collections;
using System.Collections.Generic;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterAnim : MonoBehaviour
{
    [SerializeField] private AnimationClip anim;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private SceneField sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > anim.length + waitTime)
        {
            SceneManager.LoadScene(sceneToLoad.Name);
        }
    }
}
