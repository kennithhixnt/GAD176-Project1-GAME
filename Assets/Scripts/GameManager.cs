    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Settings")]
    public int totalPlayers = 4; // Set this to your actual number of players at start
    private int alivePlayers;

    [Header("UI Elements")]
    public Button restartButton;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        alivePlayers = totalPlayers;

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }
        else
        {
            Debug.LogWarning("Restart Button not assigned in GameManager.");
        }
    }

// Call this method whenever a player dies/eliminated
   
    public void PlayerDied()
    {
        alivePlayers = Mathf.Max(0, alivePlayers - 1);

        if (alivePlayers <= 1)
        {
            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(true);
                Debug.Log("Only one player left — showing restart button.");
            }
        }
    }

    /// Reload the current scene to restart the game
   
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}




