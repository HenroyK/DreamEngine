using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Command
{
    public enum CommandType
    {
        None,
        Spawn,
        Wait,
        ChangeSpeed,
        Checkpoint
    };
    [Tooltip("Type of command")]
    public CommandType commandType = CommandType.None;
    [Tooltip("Prefab to spawn")]
    public GameObject spawnObject = null;
    [Tooltip("Location to spawn object.")]
    public Vector3 vector3;
    [Tooltip("Time to wait.")]
    public float time = 0;
    [Tooltip("New global speed.")]
    public float speed = 0;
}