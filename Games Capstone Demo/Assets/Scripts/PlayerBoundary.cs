using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundary : MonoBehaviour
{
    // Checks if the player is the object to trigger the method
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Reload();
        }
    }
    
    // Reloads the current scene
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
