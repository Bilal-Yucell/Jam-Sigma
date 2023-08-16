using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable<int>, IKillable, IAttack<int>
{
    public int Health { get; set; } = 100;

    public float WaterRate { get; set; } = 100;

    public int FistAttack { get; set; } = 10;
    public int KickAttack { get; set; } = 15;

    public Animator playerAnim;

    public int currentHealth;
    public float currentWaterRate;
    public float waterDecreaseRate = 10f; // Su azalma oranı

    public HealthBar healthBar;
    public BodyWaterRate waterRateBar;

    void Start()
    {
        currentHealth = Health;
        healthBar.SetMaxHealth(Health);
        currentWaterRate = WaterRate;
        waterRateBar.SetMaxWaterRate(WaterRate);
    }

    void Update()
    {
        WaterLoss();

    }


    public void DamageFist(int damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.SetHealth(currentHealth);

        Invoke("TakeFistAnimation", 0.15f);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void DamageKick(int damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.SetHealth(currentHealth);

        Invoke("TakeKickAnimation", 0.15f);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }


    public void Kill()
    {
        Debug.Log("Game Over");
        Destroy(gameObject); // Bu şart olmayabilir
        // ölme animasyonu
        // LoadScene-GameOverScene
    }

    public void WaterLoss()
    {
        currentWaterRate -= waterDecreaseRate * Time.deltaTime; // Sürekli olarak su kaybı yaşıyor.
        if (currentWaterRate <= 0f)
        {
            Kill();
        }
    }

    public void TakeFistAnimation()
    {
        playerAnim.SetTrigger("TakeFistPlayer");
    }

    public void TakeKickAnimation()
    {
        playerAnim.SetTrigger("TakeKickPlayer");
    }

}
