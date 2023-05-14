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
        simpleWarp = transform.parent.GetComponent<SimpleWarp>(); // Az objektum sz�l�objektum�n l�v� SimpleWarp scriptet h�vja be
        randomWarp = transform.parent.GetComponent<RandomWarp>(); // Az objektum sz�l�objektum�n l�v� RandomWarp scriptet h�vja be
        animator = GetComponent<Animator>(); // Beh�vja az objektumon l�v� Animatok komponenst
    }

    public void BlackFadeOutIsDone() // Els�t�ted�s ut�n h�vja meg a Fade In anim�ci�
    {
        Debug.Log("Sima BlackFadeOutIsDone megh�vva");

        //string x = simpleWarp.targetScene.name; // simpleWarp scriptb�l veszi ki hogy melyik Scene-t t�ltse be

        string x = "3_D_Exit";

        if (simpleWarp != null) // ha simple warpba ment a j�t�kos
        {
            x = simpleWarp.targetScene.name;
        }
        else if (randomWarp != null) // ha random warpba ment a j�t�kos
        {
            x = randomWarp.targetScene.name;
        }

        SceneManager.LoadScene(x); // Bet�lti az �j scene-t

    }

    public void BlackFadeInIsDone() // Fade In ut�n visszaadja az ir�ny�t�sta j�t�kosnak (Fade In animacio h�vja meg)
    {
        
        StartCoroutine(BlackFadeInIsDoneDelayed());
    }

    IEnumerator BlackFadeInIsDoneDelayed() // Pihen-e
    {

        
        PlayerController playerController = FindObjectOfType<PlayerController>(); // Megkeresi a gameobjektet amelyiken a Player Controller script van (Player nev� Empty object) �s beh�vja
        playerController.SlowDown(0.1f);
        yield return new WaitForSeconds(0.05f);
        playerController.SlowDown(0.8f);
        yield return new WaitForSeconds(0.05f);
        playerController.SlowDown(0.06f);
        yield return new WaitForSeconds(0.2f);
        playerController.SetFrozen(false); // A PlayerController-ben l�v� SetFrozen �rt�ket �ll�tja falsere
        playerController.SlowDown(1f); // A Frozen �llapot alatti lass�t�st �ll�tja vissza, norm�lsebess�gre
    }

}
