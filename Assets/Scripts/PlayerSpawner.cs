using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Player Prefab and Configs")]
    public GameObject playerPrefab;
    public PlayerConfig[] playerConfigs;

    private void Start()
    {
        foreach (var config in playerConfigs)
        {
            //Instantiate player at their start position
            GameObject player = Instantiate(playerPrefab, config.startPosition, Quaternion.identity);
            player.name = config.playerName;

            // Assign controls to TrailPlayer
            TrailPlayer controller = player.GetComponent<TrailPlayer>();
            if (controller != null)
            {
                controller.turnLeft = config.turnLeft;
                controller.turnRight = config.turnRight;
            }
            
            // Get the player's SpriteRenderer
            var sprite = player.GetComponentInChildren<SpriteRenderer>();
            Color visibleColor = config.trailColor;
            visibleColor.a = 1f; // make sure alpha is fully visible

            if (sprite != null)
            {
                // Set player color
                sprite.color = visibleColor;
            }

            // Now set trail color to match the player color
            TrailManager trailManager = player.GetComponent<TrailManager>();
            if (trailManager != null)
            {
                LineRenderer line = trailManager.GetComponentInChildren<LineRenderer>();
                if (line != null)
                {
                    line.startColor = visibleColor;
                    line.endColor = visibleColor;
                }
            }
        

            // Optional debug
            Debug.Log($"{config.playerName} spawned with color: {visibleColor}");
        }
    }
}




