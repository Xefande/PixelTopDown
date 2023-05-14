using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bronzekey : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip keyFound;





    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Ha a játékos belép
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.keys++;
            audioSource.PlayOneShot(keyFound);
            Destroy(gameObject, keyFound.length);
        }




    }


}
