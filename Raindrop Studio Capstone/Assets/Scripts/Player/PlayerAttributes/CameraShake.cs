using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void ShakeOnce(float duration, float magnitude)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(duration, magnitude));
    }

    private System.Collections.IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}