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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float playerHealth)
    {
        float lives = playerHealth / 100;
        float currentLifeHealth = 100 - (playerHealth % 100);
        healthBarImage.transform.localScale = new Vector3(currentLifeHealth / 100, 1, 1);
        livesText.text = lives.ToString();
    }
}
