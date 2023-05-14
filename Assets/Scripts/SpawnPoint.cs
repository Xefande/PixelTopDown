

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SpawnPoint : MonoBehaviour
    {
        public static SpawnPoint LatestSpawnPoint;

       // private static Dictionary<int, SpawnPoint> spawnPoints = new Dictionary<int, SpawnPoint>();

        [SerializeField] public int SpawnPointID;

        private void Awake()
        {
        LatestSpawnPoint = this;

        }



        public Vector3 GetSpawnPointPosition() // Visszaadja az objektum pozicióját a térben
        {
            return transform.position;
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }


