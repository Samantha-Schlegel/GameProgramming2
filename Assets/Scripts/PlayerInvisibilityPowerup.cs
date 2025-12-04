using System.Collections;
using UnityEngine;

public class PlayerInvisibilityPowerup : MonoBehaviour
{
    public bool isInvisibleToAI = false;
    public bool isInvincible = false;

    public float powerupDuration = 5f;

    private SpriteRenderer sr;

    public void ActivatePowerup()
    {
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(PowerupRoutine());
    }

    private IEnumerator PowerupRoutine()
    {
        isInvisibleToAI = true;
        isInvincible = true;


        StartCoroutine(FadeSprite(sr, 1f, 0.4f, 0.5f));

        yield return new WaitForSeconds(powerupDuration);

        StartCoroutine(FadeSprite(sr, 0.4f, 1f, 0.5f));

        isInvisibleToAI = false;
        isInvincible = false;
    }


    public IEnumerator FadeSprite(SpriteRenderer sprite, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color c = sprite.color;

        while (time < duration)
        {
            float t = time / duration;
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            sprite.color = c;

            time += Time.deltaTime;
            yield return null;
        }

        c.a = endAlpha;
        sprite.color = c;
    }

}
