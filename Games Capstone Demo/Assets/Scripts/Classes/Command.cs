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
        ChangeSpeed,
        Checkpoint,
        Camera,
        PlayAudio,
        SetLayer
    };
    public enum AudioFunctions
    {
        Fade,
        Play,
        Stop
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
    [Tooltip("Audio Clip")]
    public AudioClip audioClip;
    [Tooltip("Audio Function")]
    public AudioFunctions audioFunction;
    [Tooltip("Duration of fade. Leave at 0 for no fade.")]
    public float audioDuration = 0;
    [Tooltip("Volume. Leave this at -1 for swapping audio tracks if you want to use the existing volume.")]
    public float audioVolume = -1;
    [Tooltip("Set the state of a certain layer")]
    public int layerNo = 0;
    public bool layerState = false;
}