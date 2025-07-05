using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour

{
    public GameObject playerPrefab;
    public PlayerConfig[] playerConfigs;

    void Start()
    {
        foreach (var config in playerConfigs)
        {
            GameObject player = Instantiate(playerPrefab, config.startPosition, Quaternion.identity);
            player.name = config.playerName;

            var trailPlayer = player.GetComponent<TrailPlayer>();
            trailPlayer.turnLeft = config.turnLeft;
            trailPlayer.turnRight = config.turnRight;

            var trailManager = player.GetComponent<TrailManager>();
            if (trailManager != null)
            {
                var line = trailManager.GetComponent<LineRenderer>();
                line.material.color = config.trailColor;
            }

            GameManager.Instance.players.Add(player);
        }
    }
}


