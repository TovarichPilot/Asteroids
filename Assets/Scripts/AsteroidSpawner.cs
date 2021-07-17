using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float spawnRate = 2.5f;
    public float spawnDistance = 12.5f;
    public float trajectoryDeviation = 15.0f;

    public int spawnAmount = 2;
    private void Start()
    {
        InvokeRepeating("Spawn", this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance; // spawn in the definite circle around the spawn
            Vector3 spawnPoint = this.transform.position + spawnDirection; // our point around the spawn


            float deviation = Random.Range(-trajectoryDeviation, trajectoryDeviation); // deviation from the spawn
            Quaternion rotation = Quaternion.AngleAxis(deviation, Vector3.forward); // rotation around z axis

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);

           
            asteroid.SetTrajectory(rotation * -spawnDirection); 
        }
    }
}
