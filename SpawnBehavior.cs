using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour {
    public int MAX_SPAWN = 5;
    public int currentSpawns;
    private bool canSpawn;

	// Use this for initialization
	void Start () {
        currentSpawns = 0;
        canSpawn = true;
	}

    public bool AllowSpawning()
    {
        if(currentSpawns < MAX_SPAWN)
        {
            canSpawn = true;
            increaseSpawnCount();
        }
        else
        {
            canSpawn = false;
        }
        return canSpawn;
    }

    private void increaseSpawnCount()
    {
        currentSpawns += 1;
    }

    public void decreaseSpawnCount()
    {
        currentSpawns -= 1;
    }
}
