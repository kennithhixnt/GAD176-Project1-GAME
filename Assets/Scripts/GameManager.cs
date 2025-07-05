using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> players = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayerDied(GameObject player)
    {
        players.Remove(player);

        if (players.Count == 1)
        {
            Debug.Log($"{players[0].name} wins!");
            // TODO: Show UI or restart game
        }
        else if (players.Count == 0)
        {
            Debug.Log("Draw!");
        }
    }
    void Start()
    {
        Debug.Log("Time scale: " + Time.timeScale); // Should be 1
    }

}


