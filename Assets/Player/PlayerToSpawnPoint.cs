

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

    private void OnSceneChanged(Scene previousScene, Scene currentScene) // Scene V�lt�skor
    {
        //MoveToSpawnPoint();
        MoveToSpawnPoint(SimpleWarp.LatestTargetSpawnPointID); // A SimpleWarp-b�l veszi ki a SpawnPoint ID-t �s �tadja a MoveToSpawnPointnak

        // Meg kell csin�lni, ha randomspawnpoint warpb�l j�n a j�t�kos, akkor onnan vegye ki a Spawnpointot


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


        // targetSpawnPoint-ra h�zza a playert

        if (targetSpawnPoint != null) // Ha l�tezik a targetSpawnPoint
        {
      
            Vector3 spawnPointPosition = targetSpawnPoint.GetSpawnPointPosition();
            transform.position = spawnPointPosition;

            /*foreach (Transform child in transform)
            {
                child.position = spawnPointPosition; // �s a Childokat is odah�zza, ezt m�g kezelni kell
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
