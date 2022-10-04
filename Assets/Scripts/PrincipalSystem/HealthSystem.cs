using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 1;
    public int maxHealth = 1;
    public bool isDead { get { return health <= 0; } }

    bool immune = false;
    float time = 0f;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (immune)
        {
            time -= Time.deltaTime;
            immune = time > 0; // Es immune hasta que el tiempo deja de ser mayor de 0.
        }
    }

    public void MakeDamage(int damage)
    {
        if (immune) return;

        ImmuneActivate(true, 0.72f);

        health -= damage;
        CheckHealth();

    }

    public void GetHealth(int life)
    {
        health += life;
        CheckHealth();
    }

    public void Revive()
    {
        health = maxHealth;
        GetComponent<Collider>().enabled = true;
    }

    void CheckHealth()
    {
        health = health > maxHealth ? maxHealth : health;
        health = health < 0 ? 0 : health;
    }

    public void ImmuneActivate(bool value, float time)
    {
        immune = true;
        this.time = time;
    }
}
