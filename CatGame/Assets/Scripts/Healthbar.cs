using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Healthbar : MonoBehaviour
{

    public UnityEngine.UI.Image healthBarImage;
    public Text livesText;

    public void UpdateHealthBar(float playerHealth)
    {
        int lives = (int) playerHealth / 100;
        float currentLifeHealth = playerHealth % 100;
        healthBarImage.transform.localScale = new Vector3(currentLifeHealth / 100, 1, 1);
        livesText.text = lives.ToString();
    }
}
