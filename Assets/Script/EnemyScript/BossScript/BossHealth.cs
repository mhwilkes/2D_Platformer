﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth;
    public GameObject loot;
    private int health;
    public bool ent;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        healthSlider.value = health;
    }
    public void applyDamage(int d)
    {
        health -= d;
        if(health <= 0)
        {
            if (ent)
            {
                this.transform.parent.SendMessage("die");
            }
            else
            {
                this.transform.SendMessage("die");
            }
            
            
            health = 0;
            deathLoot();
        }
        healthSlider.value = health;
    }
    private void deathLoot()
    {
        GameObject temp = Instantiate(loot);
        temp.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-1f, 1f), 4f, 0);
        temp.GetComponent<ItemMananger>().bossOverride();

        temp = Instantiate(loot);
        temp.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-1f, 1f), 4f, 0);
        temp.GetComponent<ItemMananger>().bossOverride();
    }
}
