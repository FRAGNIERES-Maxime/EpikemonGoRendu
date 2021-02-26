using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Classes;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesBehaviour : MonoBehaviour
{
    #region Public props

    /// <summary>
    /// Prefab to create mobs
    /// </summary>
    public GameObject prefab;
    /// <summary>
    /// Min radius for spawn mobs
    /// </summary>
    public float minRadius = 10f;
    /// <summary>
    /// Max radius for spawn mobs
    /// </summary>
    public float maxRadius = 15f;
    /// <summary>
    /// Time (in seconds) between two waves
    /// </summary>
    public int secondsBetweenWave = 15;
    public float Score = 0f;

    #endregion

    #region Private props

    [HideInInspector]
    public int actualLevel = 1;
    private DateTime lastSpawnTime;
    private int minimumTimeBetweenWave = 5;

    #endregion

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        AddNewWave(GameObject.FindGameObjectsWithTag("Player")[0].transform);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            return;
        }
        var difficulty = secondsBetweenWave - (0.75 + (actualLevel / 2));
        if ((DateTime.Now - lastSpawnTime).TotalSeconds > (difficulty > minimumTimeBetweenWave ? difficulty : minimumTimeBetweenWave))
        {
            AddNewWave(players[0].transform);
        }
    }

    /// <summary>
    /// Generate a wave with game seconds
    /// </summary>
    /// <param name="target">Target for new wave</param>
    public void AddNewWave(Transform target)
    {
        lastSpawnTime = DateTime.Now;
        for (int i = 0; i < (0.75 + (actualLevel / 2)); i++)
        {
            GenerateMob(target);
        }

        if (actualLevel % 5 == 0)
        {
            var bossLevel = actualLevel / 5;
            for (int i = 0; i < bossLevel; i++)
            {
                GenerateMob(target, bossLevel);
            }
        }
        actualLevel++;
    }

    /// <summary>
    /// Generate a mob with focus on target
    /// if boss level != 0
    /// then update size and level
    /// else just update level
    /// </summary>
    /// <param name="target"></param>
    /// <param name="bossLevel"></param>
    /// <param name="isBoss"></param>
    private void GenerateMob(Transform target, int bossLevel = 0)
    {
        Vector3 point = RandomPointInAnnulus(target.position, minRadius, maxRadius);
        var newMob = Instantiate(prefab, point, prefab.transform.localRotation);
        var mobBehaviour = newMob.GetComponent<MobBehaviour>();
        mobBehaviour.target = target;
        if (bossLevel != 0)
        {
            mobBehaviour.size += actualLevel / 10f;
            mobBehaviour.level += bossLevel;
        }
        else
        {
            mobBehaviour.level = actualLevel;
        }
    }

    /// <summary>
    /// Generate a point in annulus
    /// </summary>
    /// <param name="origin">Vector2 of player position</param>
    /// <param name="minRadius">Min radius arround player</param>
    /// <param name="maxRadius">Max radius arround player</param>
    /// <returns></returns>
    private Vector3 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        var randomDirection = (Random.insideUnitCircle * origin).normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return new Vector3(point.x, 15, point.y);
    }
}
