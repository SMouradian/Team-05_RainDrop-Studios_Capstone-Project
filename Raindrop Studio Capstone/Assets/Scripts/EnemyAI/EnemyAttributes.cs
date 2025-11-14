using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt anmimation

        if (currentHealth == 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Enemy has died");

        //Die Animation
    }
}