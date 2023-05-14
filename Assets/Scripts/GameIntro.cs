
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class GameIntro : MonoBehaviour
{

    [System.Serializable]
    public class MessageSettings
    {
        public TextMeshProUGUI message;
        public float fadeInTime = 1f;
        public float displayTime = 2f;
        public float fadeOutTime = 1f;
    }

    public CanvasGroup fadePanel;
    public List<MessageSettings> messages = new List<MessageSettings>();

    public TextMeshProUGUI lastMessage;
    public float lastFadeInTime = 1f;
    public float lastDisplayTime = 1f;
    public float lastFadeOutTime = 1f;

    public bool lastMessageActive = true;

    private void Start()
    { 
       StartCoroutine(StartScreen());
    }

    IEnumerator StartScreen()
    {
        // v�gig megy az editorban hozz�adott �zeneteken egym�s ut�n megjelen�tve �ket fade (in - display - fade out - k�vetkez� message)
        for (int i = 0; i < messages.Count; i++)
        {
            MessageSettings messageSettings = messages[i];

            Coroutine fadeOutCoroutine = null;
            yield return StartCoroutine(FadeTextIn(messageSettings.message, messageSettings.fadeInTime));
            float elapsedTime = 0f;

            while (elapsedTime < messageSettings.displayTime + messageSettings.fadeOutTime)
            {
                elapsedTime += Time.deltaTime;

                // fadout elkezd�d�tt-e
                if (elapsedTime >= messageSettings.displayTime && fadeOutCoroutine == null)
                {
                    fadeOutCoroutine = StartCoroutine(FadeTextOut(messageSettings.message, messageSettings.fadeOutTime));
                }

                // ha a player a Space-re nyom, elt�nik az aktu�lis �zenet azonnal
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (fadeOutCoroutine != null)
                    {
                        StopCoroutine(fadeOutCoroutine);
                    }

                    messageSettings.message.color = new Color(messageSettings.message.color.r, messageSettings.message.color.g, messageSettings.message.color.b, 0f);
                    break;
                }
                
                if (i == messages.Count  && elapsedTime >= messageSettings.displayTime + messageSettings.fadeOutTime)
                {
                    elapsedTime = 0f;
                    fadeOutCoroutine = null;
                }
                yield return null;
            }
        }

        // Last Message Start

        StartCoroutine(LastMessagePlay()); // + utols� �zenetet megh�vja ami villog 

        // v�runk am�g space-t nyom
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        // kikapcsolja a villog� utols� �zenetet
        lastMessageActive = false;

        // v�runk az utols� �zenet megjelen�t�si + elhalv�nyod�si idej�nyi id�t
        yield return new WaitForSeconds(lastFadeOutTime+lastDisplayTime);

        // fade out r�teg l�thatatlann� v�lik �s megsz�nik
        yield return StartCoroutine(FadeCanvasGroupOut(fadePanel, 2f));
        Destroy(gameObject);

    }


    IEnumerator FadeTextIn(TextMeshProUGUI text, float duration)
    {
        float startTime = Time.time;
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (Time.time - startTime < duration)
        {
            text.color = Color.Lerp(startColor, targetColor, (Time.time - startTime) / duration);
            yield return null;
        }

        text.color = targetColor;
    }

    IEnumerator FadeTextOut(TextMeshProUGUI text, float duration)
    {
        float startTime = Time.time;
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (Time.time - startTime < duration)
        {
            text.color = Color.Lerp(startColor, targetColor, (Time.time - startTime) / duration);
            yield return null;
        }

        text.color = targetColor;
    }

    IEnumerator FadeCanvasGroupOut(CanvasGroup canvasGroup, float duration)
    {
        float startTime = Time.time;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        while (Time.time - startTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, (Time.time - startTime) / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    IEnumerator LastMessagePlay()
    {

        while (lastMessageActive)
        {
            // Fade in the last message
            yield return StartCoroutine(FadeTextIn(lastMessage, lastFadeInTime));

            // Display the last message for the given duration
            yield return new WaitForSeconds(lastDisplayTime);

            // Fade out the last message
            yield return StartCoroutine(FadeTextOut(lastMessage, lastFadeOutTime));
        }
    }


}

