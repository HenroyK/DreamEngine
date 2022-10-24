using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTallySpawner : MonoBehaviour
{
    private int score = 0;
    [SerializeField]
    private float spawnDelay = 0.2f;
    private float spawnDelayCounter = 0;
    [SerializeField]
    private float spawnInterval = 0.05f;
    private float spawnIntervalCounter = 0;
    private int spawnCounter = 0;
    [SerializeField]
    GameObject tallyPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(PlayerStats.score);
        //PlayerStats.score = 570;
        score = PlayerStats.score;
        Debug.Log(score);
        Time.timeScale = 1;
    }
        

    // Update is called once per frame
    void Update()
    {
        spawnDelayCounter += Time.deltaTime;
        spawnIntervalCounter += Time.deltaTime;
        if (spawnDelayCounter > spawnDelay && spawnIntervalCounter > spawnInterval && spawnCounter < score)
        {
            spawnCounter += 10;
            spawnIntervalCounter = 0;
            GameObject.Instantiate(tallyPrefab, this.transform.position, this.transform.rotation).transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        }
    }
    public void SkipScore()
    {
        this.enabled = false;
    }
}
