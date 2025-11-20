using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    [Header("Camera Shake")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeMagnitude = 0.15f;

    public void Start()
    {
        currentHealth = maxHealth;

        if (cameraShake == null)
        {
            cameraShake = FindObjectOfType<CameraShake>();
        }
    }
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);
        
        if (cameraShake != null)
        {
            cameraShake.ShakeOnce(shakeDuration, shakeMagnitude);
        }
        
        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Player is dead.");
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
