using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : GameMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }


    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }

        Time.timeScale = 1.0f;
    }

    public virtual void WinGame()
    {
        EndGame(true);
    }

    public virtual void LoseGame()
    {
        EndGame(false);
    }

    public void EndGame(bool hasWon)
    {
        Time.timeScale = 0.001f;

        if (hasWon)
        {
           //TO DO

        }
        else
        {
            LoseGamePn.Instance.Open();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void ReturnToFirstScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }

}