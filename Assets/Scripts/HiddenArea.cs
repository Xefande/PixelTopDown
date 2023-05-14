using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenArea : MonoBehaviour
{
    public float fadeDuration = 1f;
    private bool fading;
    private float elapsedTime;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fading = true;
        }
    }

    private void Update()
    {
        if (fading)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            if (elapsedTime >= fadeDuration)
            {
                fading = false;
            }
        }
    }
}