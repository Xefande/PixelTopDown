

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerToSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad();
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene currentScene) // Scene Váltáskor
    {
        //MoveToSpawnPoint();
        MoveToSpawnPoint(SimpleWarp.LatestTargetSpawnPointID); // A SimpleWarp-ból veszi ki a SpawnPoint ID-t és átadja a MoveToSpawnPointnak

        // Meg kell csinálni, ha randomspawnpoint warpból jön a játékos, akkor onnan vegye ki a Spawnpointot


    }

    public void MoveToSpawnPoint(int sp)
    {

        
        SpawnPoint targetSpawnPoint = null;
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.SpawnPointID == sp)
            {
                targetSpawnPoint = spawnPoint;
                break;
            }
        }


        // targetSpawnPoint-ra húzza a playert

        if (targetSpawnPoint != null) // Ha létezik a targetSpawnPoint
        {
      
            Vector3 spawnPointPosition = targetSpawnPoint.GetSpawnPointPosition();
            transform.position = spawnPointPosition;

            /*foreach (Transform child in transform)
            {
                child.position = spawnPointPosition; // És a Childokat is odahõzza, ezt még kezelni kell
            }*/
        }
        else
        {
            Debug.LogWarning("There is no spawn point.");
        }
    }



    private void DontDestroyOnLoad()
    {
        DontDestroyOnLoad(gameObject);
    }

}
