using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHandler : MonoBehaviour
{ 
    public float hoverFrequency = 2;
    public float hoverAmplitude = 0.3f;
    float angleFrequency;
    Vector3 tempOffset = Vector3.zero;

    void Start() {
        /* Calculate angle frequency for the hover-effect */
        if (hoverFrequency == 0)
            angleFrequency = 0;
        else
            angleFrequency = 2 * Mathf.PI * 1 / hoverFrequency;
    }

    // Update is called once per frame
    void FixedUpdate() {
        hover();
    }

    void hover() {
        /* Calculate sinus as offset */
        float sinusOffset = Mathf.Sin(angleFrequency * Time.time);

        /* Add amplitude to sinus, defined by gamedesigner */
        sinusOffset *= hoverAmplitude;

        Vector3 offset = new Vector3(0, sinusOffset, 0);
        Vector3 deltaOffset = offset - tempOffset;

        transform.position += deltaOffset;

        tempOffset = offset;
    }
   

    void OnCollisionEnter(Collision other) {
      if (other.gameObject.tag == "Player") {
         if (gameObject.tag == "Health") {
            other.gameObject.GetComponent<PlayerController>().heal(100);
            Destroy(gameObject);
         }

         if (gameObject.tag == "Stamina") {
            other.gameObject.GetComponent<PlayerController>().regainStamina(100);
            Destroy(gameObject);
         }
      }
    }
}
