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
    public float waterDecreaseRate = 1f; // Su azalma oranı

    public HealthBar healthBar;
    public BodyWaterRate waterRateBar;

    void Start()
    {
        currentHealth = Health;
        healthBar.SetMaxHealth(Health);
        currentWaterRate = WaterRate;
        waterRateBar.SetMaxWaterRate(WaterRate);

        PlayerEventManager.EventPlayerDamageTake += SetHealthBar;
    }

    void Update()
    {
        WaterLoss();

    }


    public void DamageFist(int damageTaken)
    {
        currentHealth -= damageTaken;
        SetHealthBar();

        Invoke("TakeFistAnimation", 0.15f);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void DamageKick(int damageTaken)
    {
        currentHealth -= damageTaken;
        SetHealthBar();

        Invoke("TakeKickAnimation", 0.15f);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }


    public void Kill()
    {
        Debug.Log("Game Over");
        // ölme animasyonu
        Destroy(gameObject); // Bu şart olmayabilir
        // LoadScene-GameOverScene
    }

    public void WaterLoss()
    {
        // BURAYA ÖLME SESİ EKLEME KİLL'DE OLACAK ZATEN
        currentWaterRate -= waterDecreaseRate * Time.deltaTime; // Sürekli olarak su kaybı yaşıyor.
        if (currentWaterRate <= 0f)
        {
            Kill();
        }
    }

    public void TakeFistAnimation()
    {
        // KARAKTERİN acı çekme sesi VE RAKİBİN YUMRUK SESİ (TakeFistAnimation'U VEYA SADECE BU SESİ EVENT İLE YAP)
        playerAnim.SetTrigger("TakeFistPlayer");
    }

    public void TakeKickAnimation()
    {
        // KARAKTERİN acı çekme sesi VE RAKİBİN YUMRUK SESİ (TakeKickAnimation'U VEYA SADECE BU SESİ EVENT İLE YAP)
        playerAnim.SetTrigger("TakeKickPlayer");
    }

    void OnEnable()
    {
        PlayerEventManager.EventPlayerDamageTake += SetHealthBar;
    }

    void OnDisable()
    {
        PlayerEventManager.EventPlayerDamageTake -= SetHealthBar;
    }

    public void SetHealthBar()
    {
        healthBar.SetHealth(currentHealth); // Bu satır hata vermeye devam ederse bu event'ı sil ve eventsız şekilde yaz
                                            // DamageFist ve DamageKick içine "currentHealth -= damageTaken" satırından sonra
                                            // " healthBar.SetHealth(currentHealth); " bunu yaz
    }

}
