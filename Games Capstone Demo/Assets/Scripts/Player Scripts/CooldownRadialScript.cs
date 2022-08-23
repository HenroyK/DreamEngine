using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownRadialScript : MonoBehaviour
{
    public void UpdateRadialBar(float fillAmount)
    {
        this.gameObject.GetComponent<Image>().fillAmount = fillAmount;
    }

}
