using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PlayerConfig
    {
        public string playerName;
        public Transform spawnPoint;
        public Color trailColor;
        public KeyCode turnLeft;
        public KeyCode turnRight;
    }

    public GameObject playerPrefab; // The prefab to use when creating player instances
    public PlayerConfig[] playerConfigs;  // Array of player configurations to loop through

    void Start()
    {
        Debug.Log("Spawning players...");
        SpawnPlayers();
    }

    /// <summary>
    /// Spawns each player according to their PlayerConfig settings.
    /// </summary>
    public void SpawnPlayers()
    {
            Debug.Log($"Spawning {playerConfigs.Length} players");

            for (int i = 0; i < playerConfigs.Length; i++)
            {
                PlayerConfig config = playerConfigs[i];
                if (config.spawnPoint == null)
                {
                    Debug.LogWarning($"Spawn point missing for {config.playerName}, skipping.");
                    continue;
                }

                GameObject player = Instantiate(playerPrefab, config.spawnPoint.position, config.spawnPoint.rotation);
                player.name = config.playerName;

                // Set input keys
                TrailPlayer trailPlayer = player.GetComponent<TrailPlayer>();
                if (trailPlayer != null)
                {
                    trailPlayer.turnLeft = config.turnLeft;
                    trailPlayer.turnRight = config.turnRight;
                }

                // Set player sprite color (make sure it's the right SpriteRenderer!)
                SpriteRenderer sprite = player.GetComponentInChildren<SpriteRenderer>();
                if (sprite != null)
                {
                    sprite.color = config.trailColor; // Set player visible color
                }
                else
                {
                    Debug.LogWarning($"SpriteRenderer not found on player prefab for {config.playerName}");
                }

                // Set trail color
                TrailManager trail = player.GetComponent<TrailManager>();
                if (trail != null)
                {
                    trail.SetTrailColor(config.trailColor);
                }
                else
                {
                    Debug.LogWarning($"TrailManager not found on player prefab for {config.playerName}");
                }
            }
    }
}




        
    






