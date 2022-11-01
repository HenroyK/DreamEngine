using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStampScript : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreStampA;
    [SerializeField]
    private GameObject scoreStampB;
    [SerializeField]
    private GameObject scoreStampC;
    [SerializeField]
    private GameObject scoreStampS;
    [SerializeField]
    private GameObject memoryImage;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public enum Stamp {A, B, C, S}

    public void DisplayStamp(Stamp grade)
    {
        switch (grade)
        {
            case Stamp.A:
                scoreStampA.SetActive(true);
                break;
            case Stamp.B:
                scoreStampB.SetActive(true);
                break;
            case Stamp.C:
                scoreStampC.SetActive(true);
                break;
            case Stamp.S:
                scoreStampS.SetActive(true);
                break;
        }
        memoryImage.SetActive(true);
        audioSource.PlayOneShot(audioClip);
    }
}
