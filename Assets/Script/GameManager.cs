using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance
    static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] GameObject deathScreen;

    private void Awake()
    {
        Application.targetFrameRate = 50;
        Debug.Log(Application.targetFrameRate + " fps");
    }

    public void PlayerDeath()
    {
        if (!deathScreen.activeInHierarchy)
        {
            deathScreen.SetActive(true);
            Invoke("RestartGame", 4);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("LevelTest");
    }
}
