using UnityEngine;
using MoreMountains.Feedbacks;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth = 100;
    public MMFeedbacks CameraShake;
    public ParticleSystem PS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            PS.Play();
            CameraShake.PlayFeedbacks();
            Destroy(gameObject);
        }
    }
}
