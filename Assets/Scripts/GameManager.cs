using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance {get; private set;}

    // Constants
    private static readonly string KEY_HIGHEST_SCORE = "HighestScore";

    // API
    public bool isGameOver{get; private set;}
    [Header ("Audio")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource GameOverSfx;
    [Header ("Score")]
    [SerializeField] private float score;
    [SerializeField] private int highestScore;


    void Awake()
    {
        // Singleton
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Score
        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE);
    }

    void Update()
    {
        if(!isGameOver)
        {
            // Increment score
            score += Time.deltaTime;

            // Update hisghest score
            if(GetScore() > GetHighestScore())
            {
                highestScore = GetScore();
            }
        }
    }

    public int GetScore()
    {
        return (int) Mathf.Floor(score);
    }

    public int GetHighestScore()
    {
        return highestScore;
    }

    public void EndGame()
    {
        if(isGameOver) return;

        // Set flag
        isGameOver = true;
        musicPlayer.Stop();
        GameOverSfx.Play();

        PlayerPrefs.SetInt(KEY_HIGHEST_SCORE,GetHighestScore());

        // Reload scene
        StartCoroutine(ReloadScene(5));
    }
    private IEnumerator ReloadScene (float delay)
    {
        yield return new WaitForSeconds(delay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);

    }



}
