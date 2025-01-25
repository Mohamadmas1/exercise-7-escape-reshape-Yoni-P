using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviourSingleton<GameTimer>
{
    [SerializeField, Tooltip("In minutes")]
    private float gameTime = 10f;
    [SerializeField] private StudioEventEmitter gameOverSoundEmitter;

    private float _timeRemaining;

    public float TimeRemaining => _timeRemaining;
    public float GameTime => gameTime * 60;

    private void Start()
    {
        _timeRemaining = GameTime * 60;
    }

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0)
        {
            _timeRemaining = 0;
            Debug.Log("Game Over");
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        gameOverSoundEmitter.Play();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
