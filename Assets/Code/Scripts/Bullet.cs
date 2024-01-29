using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed = 0.5f;
    public bool direction = true;
    public bool targetPlayer = false;

    void FixedUpdate()
    {
        // Impulsa la bala hacia una direccion, se usa un FixedUpdate para que sus frames sean fijos en todo momento
        if (direction) gameObject.transform.Translate(Vector2.right * projectileSpeed, Space.World);
        else gameObject.transform.Translate(GameManager.Instance.gameSpeed * 2 * projectileSpeed * Vector2.left, Space.World);
    }

    // Borrar bala al salir de la pantalla (solo funciona en game/build mode)
    private void OnBecameInvisible() { Destroy(gameObject); }

    public void OnTriggerEnter2D(Collider2D go)
    {
        if (go.TryGetComponent(out IDamageable test)) { test?.Kill(gameObject); Destroy(gameObject); }
    }
}
