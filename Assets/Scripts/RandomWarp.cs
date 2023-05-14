using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

public class RandomWarp : MonoBehaviour
{
    [SerializeField] public SceneAsset exitScene; // Húzható és szerkeszthetõ az Unity szerkesztõjében
    [SerializeField] public List<SceneAsset> randomScenes; // Húzható és szerkeszthetõ az Unity szerkesztõjében

    public Animator animator;
    public IsFadeOutDone isFadeOutDone;
    public PlayerController playerController;
    public static int LatestTargetSpawnPointID = 1; // Mindig az 1-es warpID-jú spawnpontra visz (ellentétben a SimpleWarppal)
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
            // A játékos dungeonPoint-jának megfelelõen töltsük be a scene-t
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