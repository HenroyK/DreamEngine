using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameContollerScript : MonoBehaviour
{
    //list of all instantiated moving objects, ie. Buildings
    private List<GameObject> movingObjects = new List<GameObject>();

    
    //List of Commands, such as spawning stuff, waiting or changing gamespeed.
    public List<Command> commandList = new List<Command>();

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
            if(commandList.Count() > 0)
            {
                //get the next command from the list
                Command nextCommand = commandList.ElementAt(0);
                commandList.RemoveAt(0);
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
                    default:
                        Debug.LogError("Something has gone wrong -> no command type match");
                        break;
                }
            }
            else
            {
                //Debug.Log("End of commands");
            }
        }
    }
}



//Code Archive

//list of scripted spawns. The Game Controller will spawn these in order, based on the instructions given elsewhere.
//public List<GameObject> scriptedSpawnMovingObjects = new List<GameObject>();