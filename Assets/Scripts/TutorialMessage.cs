using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class TutorialMessage : MonoBehaviour
{
    public string message = "Press E";
    public GameObject messagePrefab;
    public Vector3 messageOffset;
    public GameObject triggerCanvas;
    public GameObject dependentObject;

    private GameObject messageInstance;
    private TextMeshProUGUI messageText;
    private bool playerNear;
    private bool isE;

    private void Start()
    {
        messageInstance = Instantiate(messagePrefab, transform.position + messageOffset, Quaternion.identity, transform);
        messageInstance.transform.SetParent(triggerCanvas.transform, false);
        messageText = messageInstance.GetComponent<TextMeshProUGUI>();
        messageText.text = message;
        SetTextAlpha(0);
        messageInstance.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            SetTextAlpha(1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            SetTextAlpha(0);
        }
    }

    void Update()
    {

        if (dependentObject == null)
        {
            Destroy(gameObject);
        }

        
        isEcheck();

        if (isE && playerNear)
        {
            SetTextAlpha(0);
        }
    }

    public void isEcheck()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isE = true;
        }
        else
        {
            isE = false;
        }
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = messageText.color;
        color.a = alpha;
        messageText.color = color;
    }


    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color32(0xFF, 0x66, 0x00, 0xFF);

        Gizmos.color = gizmoColor;

        //  Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }



    private void OnDestroy()
    {
        if (messageInstance != null)
        {
            Destroy(messageInstance);
        }
    }
}