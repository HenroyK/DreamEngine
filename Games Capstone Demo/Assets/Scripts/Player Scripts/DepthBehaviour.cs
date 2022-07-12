using UnityEngine;

public class DepthBehaviour : MonoBehaviour
{
    //Variables
    public bool[] layerAvailable;
    public float[] layerAxis;
    public int curDepth;
    public GameObject player;

    // Check if the player can move to layer
    public int CheckLayer(int direction)
    {
        int checkLayer = curDepth + direction;
        while (checkLayer >= 0 && checkLayer < layerAvailable.Length)
        if (layerAvailable[checkLayer])
        {
            return checkLayer;
        }
        else
        {
            checkLayer += direction;
        }
        return -1;
    }

    // Set the state of a layer being available
    public void SetLayerState (int layer,bool state)
    {
        if (layer >= 0 && layer < layerAvailable.Length)
            layerAvailable[layer] = state;
        else
            Debug.LogWarning("Outside of layer range ("+"0"+(layerAvailable.Length-1)+")");
    }

    // Set the axis of a layer
    public void SetLayerAxis(int layer, float axis)
    {
        if (layer >= 0 && layer < layerAvailable.Length)
            layerAxis[layer] = axis;
        else
            Debug.LogWarning("Outside of layer range (" + "0" + (layerAvailable.Length - 1) + ")");
    }
}
