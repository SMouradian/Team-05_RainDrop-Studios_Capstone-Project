using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
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

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
