using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{   
    PlayerConfig[] playerConfigs;
    public string playerName;
    public KeyCode turnLeft;
    public KeyCode turnRight;
    public Color trailColor;
    public Vector2 startPosition;

    public void update()
    {
        foreach (var config in playerConfigs)
        {
            // Your existing code…

            // ✅ Put the debug line here — inside the loop!
            Debug.Log($"{config.playerName} color: {config.trailColor}, alpha: {config.trailColor.a}");
        }
    }
}


