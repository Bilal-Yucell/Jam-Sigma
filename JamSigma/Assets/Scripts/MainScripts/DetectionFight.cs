using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetectionFight : MonoBehaviour
{
    public bool playerInDetectionFight = false;
    public DateTime nextDamage;
    public float fightAfterTime;

    public Enemy enemy;

    public int randomNumber;

    void Awake()
    {
        nextDamage = DateTime.Now;
    }

    void FixedUpdate()
    {
        randomNumber = UnityEngine.Random.Range(0, 100);


        if (playerInDetectionFight == true)
        {
            if (randomNumber < 60)
            {
                FightInDetectionFight1();
            }
            else
            {
                FightInDetectionFight2();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInDetectionFight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInDetectionFight = false;
        }
    }

    public void FightInDetectionFight1()
    {
        if (nextDamage <= DateTime.Now)
        {
            enemy.EnemyFist();
            nextDamage = DateTime.Now.AddSeconds(System.Convert.ToDouble(fightAfterTime));
        }
    }

    public void FightInDetectionFight2()
    {
        if (nextDamage <= DateTime.Now)
        {
            enemy.EnemyKick();
            nextDamage = DateTime.Now.AddSeconds(System.Convert.ToDouble(fightAfterTime));
        }
    }

}
