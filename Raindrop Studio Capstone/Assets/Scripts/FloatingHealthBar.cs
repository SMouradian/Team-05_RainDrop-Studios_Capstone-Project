using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    private CanvasGroup canvasGroup;
    private void Awake()
    {
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }    

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (slider == null)
        {
            Debug.LogWarning("Slider component is not assigned.");
            return;
        }
        slider.value = currentHealth / maxHealth;
    }

    public void FadeOutAndDestroy()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float duration = 1.0f;
        float t = 0f;

        while (t <duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
