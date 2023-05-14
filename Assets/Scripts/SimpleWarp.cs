using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class SimpleWarp : MonoBehaviour
{
    // [SerializeField] public string targetScene; // A c�l scene neve // L�trhehozzuk a szerkeszt�be a mez�t, ahov� be�rjuk az �j szoba nev�t


    [SerializeField] public SceneAsset targetScene;
    [SerializeField] public int targetSpawnPointID; // A c�l spawn pont azonos�t�ja
    [SerializeField] public int useLimit = 0; // Ennyiszer lehet haszn�lni! Ha 0, akkor v�gtelen

   // public int warpUsed = 0; // Ennyiszer volt eddig haszn�lva

    [SerializeField] public int warpID;
    public static Dictionary<int, int> warpUsed = new Dictionary<int, int>();

    public Animator animator;
    public IsFadeOutDone isFadeOutDone;
    public PlayerController playerController;
    public static int LatestTargetSpawnPointID = 1;

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other) // Lefut ha bel�pett a collider az objektumunkba
    {
        PlayerController isPlayer = other.GetComponent<PlayerController>(); // Ha a PlayerMove script rajta van az objektumon ami bele l�pett, akkor �rt�ket ad neki

        LatestTargetSpawnPointID = targetSpawnPointID;

        if (isPlayer != null) // Ha j�t�kos, akkor fut le
        {
            if (!warpUsed.ContainsKey(warpID))
            {
                warpUsed[warpID] = 0;
            }

            if (useLimit == 0 || useLimit > warpUsed[warpID]) 
            { 
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.SetFrozen(true); // Le�ll�tja az ir�ny�t�st
            playerController.SlowDown(0.2f); // Leveszi a j�t�kos sebess�g�t
            FadeOut();
            warpUsed[warpID]++; 
            }
        }


    }

    void FadeOut()
    {
        animator.SetTrigger("FadeOutTrigger");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

}
