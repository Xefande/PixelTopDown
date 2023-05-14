using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class SimpleWarp : MonoBehaviour
{
    // [SerializeField] public string targetScene; // A cél scene neve // Létrhehozzuk a szerkesztõbe a mezõt, ahová beírjuk az új szoba nevét


    [SerializeField] public SceneAsset targetScene;
    [SerializeField] public int targetSpawnPointID; // A cél spawn pont azonosítója
    [SerializeField] public int useLimit = 0; // Ennyiszer lehet használni! Ha 0, akkor végtelen

   // public int warpUsed = 0; // Ennyiszer volt eddig használva

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
    void OnTriggerEnter2D(Collider2D other) // Lefut ha belépett a collider az objektumunkba
    {
        PlayerController isPlayer = other.GetComponent<PlayerController>(); // Ha a PlayerMove script rajta van az objektumon ami bele lépett, akkor értéket ad neki

        LatestTargetSpawnPointID = targetSpawnPointID;

        if (isPlayer != null) // Ha játékos, akkor fut le
        {
            if (!warpUsed.ContainsKey(warpID))
            {
                warpUsed[warpID] = 0;
            }

            if (useLimit == 0 || useLimit > warpUsed[warpID]) 
            { 
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.SetFrozen(true); // Leállítja az irányítást
            playerController.SlowDown(0.2f); // Leveszi a játékos sebességét
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
