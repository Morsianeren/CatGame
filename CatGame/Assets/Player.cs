using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Healthbar healthbar;
    public int health = 900;
    public int maxHealth = 900;
    
    public Staminabar staminabar;
    public int stamina = 100;
    public int maxStamina = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damageValue)
    {
        health -= damageValue;
        healthbar.UpdateHealthBar(health);
    }

    public void heal(int healValue)
    {
        health += healValue;
        healthbar.UpdateHealthBar(health);
    }

    public void consumeStamina(int staminaCost)
    {
        stamina -= staminaCost;
        staminabar.updateStamina(stamina, maxStamina);
    }
    public void regainStamina(int regainValue)
    {
        stamina += regainValue;
        staminabar.updateStamina(stamina, maxStamina);
    }
}
