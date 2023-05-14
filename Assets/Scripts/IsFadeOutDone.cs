using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsFadeOutDone : MonoBehaviour
{

    public Animator animator; 
    public SimpleWarp simpleWarp;
    public RandomWarp randomWarp;
 // public PlayerController playerController; 

    // Start is called before the first frame update
    public void Start()
    {
        //simpleWarp = FindObjectOfType<SimpleWarp>();
        simpleWarp = transform.parent.GetComponent<SimpleWarp>(); // Az objektum szülõobjektumán lévõ SimpleWarp scriptet hívja be
        randomWarp = transform.parent.GetComponent<RandomWarp>(); // Az objektum szülõobjektumán lévõ RandomWarp scriptet hívja be
        animator = GetComponent<Animator>(); // Behívja az objektumon lévõ Animatok komponenst
    }

    public void BlackFadeOutIsDone() // Elsütétedés után hívja meg a Fade In animéció
    {
        Debug.Log("Sima BlackFadeOutIsDone meghívva");

        //string x = simpleWarp.targetScene.name; // simpleWarp scriptból veszi ki hogy melyik Scene-t töltse be

        string x = "3_D_Exit";

        if (simpleWarp != null) // ha simple warpba ment a játékos
        {
            x = simpleWarp.targetScene.name;
        }
        else if (randomWarp != null) // ha random warpba ment a játékos
        {
            x = randomWarp.targetScene.name;
        }

        SceneManager.LoadScene(x); // Betölti az új scene-t

    }

    public void BlackFadeInIsDone() // Fade In után visszaadja az irányításta játékosnak (Fade In animacio hívja meg)
    {
        
        StartCoroutine(BlackFadeInIsDoneDelayed());
    }

    IEnumerator BlackFadeInIsDoneDelayed() // Pihen-e
    {

        
        PlayerController playerController = FindObjectOfType<PlayerController>(); // Megkeresi a gameobjektet amelyiken a Player Controller script van (Player nevû Empty object) és behívja
        playerController.SlowDown(0.1f);
        yield return new WaitForSeconds(0.05f);
        playerController.SlowDown(0.8f);
        yield return new WaitForSeconds(0.05f);
        playerController.SlowDown(0.06f);
        yield return new WaitForSeconds(0.2f);
        playerController.SetFrozen(false); // A PlayerController-ben lévõ SetFrozen értéket állítja falsere
        playerController.SlowDown(1f); // A Frozen állapot alatti lassítást állítja vissza, normálsebességre
    }

}
