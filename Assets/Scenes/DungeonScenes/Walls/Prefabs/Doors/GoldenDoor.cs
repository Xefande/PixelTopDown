using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenDoor : MonoBehaviour
{
    public float interactDistance = 3f;
    private Transform playerTransform;
    private PlayerController playerController;
    private AudioSource audioSource;
    public AudioClip doorOpenClip;
    public AudioClip doorLockedClip;


    private SpriteRenderer spriteRenderer;
    private bool isFading = false;
    private float fadeDuration = 1f;
    private float fadeStarTime;



    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < interactDistance)
        {
            Debug.Log("Press E to open the door");

            if (Input.GetKeyDown(KeyCode.E)) 
            {
                if (playerController.goldenKeys > 0)
                {
                    playerController.goldenKeys--;
                    Debug.Log("Door Open");
                    audioSource.PlayOneShot(doorOpenClip);
                    StartFading();
                    
                    Destroy(gameObject, doorOpenClip.length);
                }
                else 
                {
                    Debug.Log("You have no Golden keys");
                    audioSource.PlayOneShot(doorLockedClip);
                }
            }
        }

        if (isFading)
        {
            float elapsed = Time.time - fadeStarTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            Color color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);

            if (elapsed >= fadeDuration)
            {
                isFading = false;
            }
        }

    }

    public void StartFading()
    {
        isFading = true;
        fadeStarTime = Time.time;
    }
}


