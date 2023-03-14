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

    public void updateStamina(int currentstamina, int maxStamina)
    {
        staminaBarImage.transform.localScale = new Vector3((float) currentstamina / maxStamina, 1, 1);
    }
}
