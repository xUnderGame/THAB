using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fbSpeed = 0.5f;

    void FixedUpdate()
    {
        // Impulsa la bala hacia la derecha, se usa un FixedUpdate para que sus frames sean fijos en todo momento
        gameObject.transform.Translate(Vector2.right * fbSpeed);
    }

    // Borrar bala al salir de la pantalla (solo funciona en game/build mode)
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
