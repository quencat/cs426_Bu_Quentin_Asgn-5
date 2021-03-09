using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{


    public GameObject cat;

    public Wave[] waves; //if we had multiple waves we would keep track of them here

    //Vector3[] mazeSpawnsA = { new Vector3(-2.23f, 0f, -16.33f), new Vector3(-5.054f, 0f, -17.74f), new Vector3(-15.64f, 0f, -14.77f), new Vector3(-13.14f, 0f, -5.63f)};
    Vector3[] mazeSpawnsA = { new Vector3(-2.23f, 0f, -16.33f), new  Vector3(-16.33f, 0f, 2.23f), new  Vector3(16.33f, 0f, -2.23f), new  Vector3(2.23f, 0f, 16.33f)};
    Vector3[] mazeSpawnsB = { new Vector3(-5.054f, 0f, -17.74f), new Vector3(-17.74f, 0f, 5.054f), new Vector3(17.74f, 0f, -5.054f), new Vector3(5.054f, 0f, 17.74f)};
    Vector3[] mazeSpawnsC = { new Vector3(-15.64f, 0f, -14.77f), new Vector3( -14.77f, 0f, 15.64f), new Vector3(14.77f, 0f, -15.64f), new Vector3(15.64f, 0f, 14.77f)};
    Vector3[] mazeSpawnsD = { new Vector3(-13.14f, 0f, -5.63f), new Vector3(-5.63f, 0f, 13.14f), new Vector3(5.63f, 0f, -13.14f), new Vector3(13.14f, 0f, 5.63f)};
    Vector3[] centerSpawns = { new Vector3(-8.5f, 0f, -8.5f), new Vector3(8.5f, 0f, 8.5f), new Vector3(-8.5f, 0f, 8.5f), new Vector3(8.5f, 0f, -8.5f)};


    List<Vector3> spawnPoints;
    List<Vector3 []> allMazeSpawns;
    Wave currWave;
    int currWaveNum = 0;

    int catsLeftToSpawn;

    void Start() {
      NextWave();
    }


    void Update() {
      if (catsLeftToSpawn > 0) {
        catsLeftToSpawn--;

        // the spawn location is the 2nd parameter and is randomly picked from a list of possible spawns
        // spawn is unique
        int r = Random.Range(0, spawnPoints.Count);
        Vector3 spawnPoint = spawnPoints[r];
        spawnPoints.RemoveAt(r);
        GameObject newSpawn = GameObject.Instantiate(cat, spawnPoint, Quaternion.identity) as GameObject;
        newSpawn.transform.localScale = new Vector3(.01f, .01f, .01f); //scale appropriately to map
      }
    }

    void NextWave() {
      currWaveNum++;
      currWave = waves[currWaveNum - 1];

      catsLeftToSpawn = currWave.catCount + 1; // +1 to include tie breaker cat always
      generateSpawns();
    }


    void generateSpawns() {
        spawnPoints = new List<Vector3>();

        // always add the 4 spawns from the middle of the map and the default point
        spawnPoints.Add(Vector3.zero);
        foreach (Vector3 v in centerSpawns) {
          spawnPoints.Add(v);
        }

        int numListsToAdd = (currWave.catCount - 4) / 4; // lists to add to the spawnPoints
        // we will randomly pick from the maze spawns each time we generate spawns
        allMazeSpawns = new List<Vector3 []>();
        allMazeSpawns.Add(mazeSpawnsA);
        allMazeSpawns.Add(mazeSpawnsB);
        allMazeSpawns.Add(mazeSpawnsC);
        allMazeSpawns.Add(mazeSpawnsD);
        for (int i = 0; i < numListsToAdd; i++) {
          // randomly pick a maze spawn group and add it to our spawnpoints
          int r = Random.Range(0, allMazeSpawns.Count);
          Vector3[] tmp = allMazeSpawns[r];
          allMazeSpawns.RemoveAt(r);
          foreach (Vector3 v in tmp) {
            spawnPoints.Add(v);
          }
        }

    }


    [System.Serializable]
    public class Wave {
      public int catCount; // range: (0, 20), there will always be 1 cat spawned at midpoint of map (0, 0, 0)


      //example: catCount = 4 means that there will be 4 cats spawned outside of the maze and 1 in the midpoint (5 cats total)
      //         catCount = 8 means that there will be 4 cats spawned outside of the maze, 1 in the midpoint, and 4 in the maze (1 on each players side)
      //         catCount = 20 means that there will be 4 cats spawned outside of the maze, 1 in the midpoint, and 16 in the maze (4 on each players side)
    }
}
