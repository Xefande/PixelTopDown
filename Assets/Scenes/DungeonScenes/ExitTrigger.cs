using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class ExitTrigger : MonoBehaviour
{
    public CanvasGroup blackScreenCanvasGroup;
    public TextMeshProUGUI messageText;
    public float fadeDuration = 1f;
    public float messageDisplayDuration = 2f;
    public float messageDisplayDelay = 1f;

    private bool playerEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = true;
        }
    }

    private void Update()
    {
        if (playerEntered)
        {
            StartCoroutine(DisplayMessagesAndFade());
            playerEntered = false;
        }
    }

    private IEnumerator DisplayMessagesAndFade()
    {
        messageText.text = "You found the great hall!";
        StartCoroutine(FadeInMessage(messageDisplayDelay));


        yield return new WaitForSeconds(messageDisplayDuration + messageDisplayDelay);

        StartCoroutine(FadeOutMessage(messageDisplayDelay));

        yield return new WaitForSeconds(messageDisplayDelay + messageDisplayDelay);

        messageText.text = "To be continued...";
        StartCoroutine(FadeInMessage(2f));

        yield return new WaitForSeconds(messageDisplayDuration);

        StartCoroutine(FadeInBlackScreen());
    }

    private IEnumerator FadeInMessage(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, Mathf.Lerp(0f, 1f, elapsed / fadeDuration));
            yield return null;
        }
    }

    private IEnumerator FadeOutMessage(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, Mathf.Lerp(1f, 0f, elapsed / fadeDuration));
            yield return null;
        }
    }

    private IEnumerator FadeInBlackScreen()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            blackScreenCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }

        // Esc, restart ilyenek
    }
}