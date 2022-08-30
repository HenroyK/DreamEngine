using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownRadialScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cooldownBackground;
    public void UpdateRadialBar(float fillAmount)
    {
        
        this.gameObject.GetComponent<Image>().fillAmount = fillAmount;

        if (this.gameObject.GetComponent<Image>().fillAmount == 0)
        {
            cooldownBackground.SetActive(false);
        }
        else
        {
            cooldownBackground.SetActive(true);
        }
    }
}
