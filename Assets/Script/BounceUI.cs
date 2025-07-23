using System.Collections;
using UnityEngine;

public class BounceUI : MonoBehaviour
{
    public float BounceHeight;
    public float BounceDuration;

    private RectTransform rectTransform;
    private Vector2 startAnchoredPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startAnchoredPos = rectTransform.anchoredPosition;

        StartCoroutine(BounceUp());
    }

    /// <summary>
    /// Move the title text up once
    /// </summary>
    IEnumerator BounceUp()
    {
        yield return StartCoroutine(MoveVertical(startAnchoredPos, startAnchoredPos + Vector2.up * BounceHeight, BounceDuration));
        StartCoroutine(BounceDown());
    }

    /// <summary>
    /// move the title text down after going up
    /// </summary>
    IEnumerator BounceDown()
    {
        yield return StartCoroutine(MoveVertical(startAnchoredPos + Vector2.up * BounceHeight, startAnchoredPos, BounceDuration));
    }

    /// <summary>
    /// The function inside both BounceDown and BounceUp to move the Title text up and down
    /// </summary>
    IEnumerator MoveVertical(Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = to;
    }
}
