using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staminabar : MonoBehaviour
{

    public UnityEngine.UI.Image staminaBarImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateStamina(float currentstamina, float maxStamina)
    {
        staminaBarImage.transform.localScale = new Vector3(currentstamina / maxStamina, 1, 1);
    }
}
