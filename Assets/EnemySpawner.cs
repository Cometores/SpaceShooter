using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    //[SerializeField] GameObject boss;

    [SerializeField] GameObject bottomLeft;
    [SerializeField] GameObject topRight;

    AudioSource auSource;
    [SerializeField] AudioClip bgm;

    Vector3 xyMax;
    Vector3 xyMin;

    void Start()
    {
        xyMax = topRight.transform.position;
        xyMin = bottomLeft.transform.position;
        
        auSource = GetComponent<AudioSource>();
        auSource.clip = bgm;
        auSource.Play();

        Spawner();
    }

    void Spawner()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xyMin.x, xyMax.x), Random.Range(xyMin.y, xyMax.y), 0);
        Instantiate(enemy, spawnPos, Quaternion.Euler(0,0,90));
        Invoke(nameof(Spawner), Random.Range(3.5f, 5));
    }
}
