using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    public string playerName;
    public KeyCode turnLeft;
    public KeyCode turnRight;
    public Color trailColor;
    public Vector2 startPosition;
}


