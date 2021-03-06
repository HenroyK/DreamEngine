using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameContollerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject playerSpawn;       
    //private GameObject playerRef;

    public AudioManager audioManager;
    //list of all instantiated moving objects, ie. Buildings
    private List<GameObject> movingObjects = new List<GameObject>();

    //list of all instantiated moving objects to be saved at a checkpoint
    private List<GameObject> checkpointObjects = new List<GameObject>();

    private int lastCheckpoint = 0;

    //List of Commands, such as spawning stuff, waiting or changing gamespeed.
    public List<Command> commandList = new List<Command>();
    [SerializeField]
    private int commandListIndex = 0;

    //Score variable, does nothing right now.
    private int score;
    public int Score { get => score; set => score = value; }

    //Combo variable, does nothing right now.
    private int combo;
    public int Combo { get => combo; set => combo = value; }

    private float delay = 0;
    private float globalSpeed;
    private float checkpointDelay;

    

    //Below is stuff for later.
    //public List<GameObject> otherSpawnMovingObjects = new List<GameObject>();
    //public List<GameObject> FixedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (audioManager = null)
        {
            audioManager = new AudioManager();
        }

        //Instantiate(player, playerSpawn.transform.position, Quaternion.identity);

        player = Instantiate(player, playerSpawn.transform.position, Quaternion.identity);
        player.BroadcastMessage("UpdateSpeed", globalSpeed);
        this.GetComponent<CameraScript>().SetLookat(player);
    }

    // Update is called once per frame
    void Update()
    {
        //handle if the gamecontroller should wait before executing next command
        if(delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            //if not end of commands
            if(commandList.Count() > commandListIndex)
            {
                //get the next command from the list
                Command nextCommand = commandList.ElementAt(commandListIndex);
                commandListIndex++;
                switch (nextCommand.commandType)
                {
                    case Command.CommandType.None:
                        Debug.LogWarning("Command with no type encountered.");
                        break;
                    case Command.CommandType.Spawn:
                        if (nextCommand.spawnObject != null)
                        {
                            //Spawn Object
                            GameObject newObject = Instantiate(nextCommand.spawnObject, nextCommand.vector3, Quaternion.identity);
                            Debug.Log("Spawning " + nextCommand.spawnObject + " at " + nextCommand.vector3 + ".");
                            //Set Object speed
                            newObject.BroadcastMessage("ChangeSpeed", globalSpeed);

                            //newObject.GetComponent<BlockMove>().ChangeSpeed(globalSpeed);
                            movingObjects.Add(newObject);
                        }
                        else
                        {
                            Debug.LogError("Invalid command (Spawn) -> need to fill Spawn Object");
                        }
                        
                        break;
                    //case Command.CommandType.Wait:
                    //    delay = nextCommand.time;
                    //    Debug.Log("Waiting " + delay + " seconds.");
                    //    break;
                    //Change the global speed of all objects
                    case Command.CommandType.ChangeSpeed:
                        globalSpeed = nextCommand.speed;
                        foreach (GameObject a in movingObjects)
                        {
                            a.BroadcastMessage("ChangeSpeed", globalSpeed);
                            //a.GetComponent<BlockMove>().ChangeSpeed(globalSpeed);
                        }
                        player.BroadcastMessage("UpdateSpeed", globalSpeed);
                        Debug.Log("Global speed is now" + globalSpeed);
                        break;
                    case Command.CommandType.Camera:
                        //make Camera look at worldpoint.

                        //NotImplemented
                        break;
                    case Command.CommandType.Checkpoint:
                        //Set     public int Combo { get => combo; set => combo = value; } checkpoint to current command index for easy access. Create a copy of all objects in movingObjects and disable the copies.
                        SetCheckpoint();
                        
                        break;
                    case Command.CommandType.PlayAudio:
                        audioManager.AudioCommand(nextCommand.audioClip, nextCommand.audioDuration, nextCommand.audioVolume);
                        //NotImplemented
                        //Probably make a list of audio sources, place them into a list and use that to access them.
                        break;
                    case Command.CommandType.SetLayer:
                        //Checkpoint
                        player.GetComponent<DepthBehaviour>().SetLayerState(nextCommand.layerNo, nextCommand.layerState);
                        //NotImplemented
                        //Probably make a list of audio sources, place them into a list and use that to access them.
                        break;
                    default:
                        Debug.LogError("Something has gone wrong -> no command type match or command not implemented.");
                        break;
                }
                delay = nextCommand.time;
                Debug.Log("Waiting " + delay + " seconds.");
            }
            else
            {
                //Debug.Log("End of commands");
            }
        }
    }

    public void SetCheckpoint()
    {
        checkpointDelay = delay;
        //Handle saving checkpoint.
        lastCheckpoint = commandListIndex;
        checkpointObjects.Clear();

        foreach (GameObject obj in movingObjects)
        {
            GameObject newObj = Instantiate(obj);
            newObj.SetActive(false);
            checkpointObjects.Add(newObj);
        }
    }
    public void LoadCheckpoint()
    {
        //Handle loading checkpoint.
        clearObjects();
        foreach (GameObject obj in checkpointObjects)
        {
            GameObject newObj = Instantiate(obj);
            movingObjects.Add(newObj);
        }
        foreach (GameObject obj in movingObjects)
        {
            obj.SetActive(true);
            obj.BroadcastMessage("ChangeSpeed", globalSpeed);
        }
        //lateCheckpointUpdate = true;
        commandListIndex = lastCheckpoint;
        delay = checkpointDelay;
        player.transform.position = playerSpawn.transform.position;
    }
    void clearObjects()
    {
        //Clear all moving objects.
        foreach (GameObject obj in movingObjects)
        {
            Destroy(obj);
        }
        movingObjects.Clear();
    }
    float GetCurrentSpeed()
    {
        return globalSpeed;
    }
    public void EndLevel()
    {
        //EndLevelCode
    }
    //private void setScore(int pscore)
    //{

    //}

    //private void setCombo(int pcombo)
    //{

    //}
}



//Code Archive

//list of scripted spawns. The Game Controller will spawn these in order, based on the instructions given elsewhere.
//public List<GameObject> scriptedSpawnMovingObjects = new List<GameObject>();