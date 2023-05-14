using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

public class RandomWarp : MonoBehaviour
{
    [SerializeField] public SceneAsset exitScene; // H�zhat� �s szerkeszthet� az Unity szerkeszt�j�ben
    [SerializeField] public List<SceneAsset> randomScenes; // H�zhat� �s szerkeszthet� az Unity szerkeszt�j�ben

    public Animator animator;
    public IsFadeOutDone isFadeOutDone;
    public PlayerController playerController;
    public static int LatestTargetSpawnPointID = 1; // Mindig az 1-es warpID-j� spawnpontra visz (ellent�tben a SimpleWarppal)
    [SerializeField] public SceneAsset targetScene;
    [SerializeField] public int dungPointsChange = 0;
    [SerializeField] public int dungPointsExit = 20;
    [SerializeField] public float slowingValue = 0.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController isPlayer = other.GetComponent<PlayerController>();
        playerController = FindObjectOfType<PlayerController>();


        if (isPlayer != null)
        {
            isPlayer.dungeonPoints += dungPointsChange;
            // A j�t�kos dungeonPoint-j�nak megfelel�en t�lts�k be a scene-t
            if (playerController.dungeonPoints >= dungPointsExit)
            {
                // SceneManager.LoadScene(exitScene.name);
                targetScene = exitScene;
            }
            else
            {
                int randomIndex = Random.Range(0, randomScenes.Count);
                // SceneManager.LoadScene(randomScenes[randomIndex].name);

                targetScene = randomScenes[randomIndex];
            }

            playerController.SetFrozen(true);
            playerController.SlowDown(slowingValue);
            FadeOut();

        }
    }

    void FadeOut()
    {
        animator.SetTrigger("FadeOutTrigger");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}