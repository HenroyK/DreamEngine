using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameContollerScript : MonoBehaviour
{
    //Script controllers the events of the game
    
    //list of all instantiated moving objects, ie. Buildings
    private List<GameObject> movingObjects = new List<GameObject>();

    
    //List of Commands, such as spawning stuff, waiting or changing gamespeed.
    public List<Command> commandList = new List<Command>();

    private int commandListIndex = 0;
    private int lastCheckpoint = 0;
    private float delay;
    private float globalSpeed;

    //Below is stuff for later.
    //public List<GameObject> otherSpawnMovingObjects = new List<GameObject>();
    //public List<GameObject> FixedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
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
                            newObject.GetComponent<BlockMove>().speed = globalSpeed;
                            movingObjects.Add(newObject);
                        }
                        else
                        {
                            Debug.LogError("Invalid command (Spawn) -> need to fill Spawn Object");
                        }
                        
                        break;
                    // makes the controller wait a set amount of 
                    // time before commiting another command
                    case Command.CommandType.Wait:
                        delay = nextCommand.time;
                        Debug.Log("Waiting " + delay + " seconds.");
                        break;
                    //Change the global speed of all objects
                    case Command.CommandType.ChangeSpeed:
                        globalSpeed = nextCommand.speed;
                        foreach (GameObject a in movingObjects)
                        {
                            a.GetComponent<BlockMove>().speed = globalSpeed;
                        }
                        Debug.Log("Global speed is now" + globalSpeed);
                        break;
                    case Command.CommandType.Camera:
                        //make Camera look at worldpoint.

                        //NotImplemented
                        break;
                    case Command.CommandType.Checkpoint:
                        //Set last checkpoint to current command index for easy access.
                        //Might want to do something like save the state of many things.
                        lastCheckpoint = commandListIndex;
                        break;
                    case Command.CommandType.PlayAudio:
                        //Checkpoint

                        //NotImplemented
                        break;
                    default:
                        Debug.LogError("Something has gone wrong -> no command type match or command not implemented.");
                        break;
                }
            }
            else
            {
                //Debug.Log("End of commands");
            }
        }
    }

    void LoadCheckpoint()
    {
        //Handle loading checkpoint. Probably want to set commandListIndex to lastCheckpoint in case of going to last checkpoint.
    }
}



//Code Archive

//list of scripted spawns. The Game Controller will spawn these in order, based on the instructions given elsewhere.
//public List<GameObject> scriptedSpawnMovingObjects = new List<GameObject>();