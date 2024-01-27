using UnityEngine;

public class LaneEnemy : LaneBehaviour, IDamageable
{
    private bool hasSwapped = false;

    // Update is called once per frame
    void Update()
    {
        // Swaps lanes when position passes a threshold
        if (transform.position.x <= 20 && !hasSwapped)
        {
            hasSwapped = true;
            SwapLane(gameObject.layer == 8,
                GetComponent<Rigidbody2D>(),
                gameObject);
        }

        // Destroys enemy after a position threshold
        if (transform.position.x <= -20) Destroy(gameObject);
    }

    // Collision actions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable test))
        {
            test?.Kill(gameObject);
            Destroy(gameObject);
        }
    }

    public void Kill(GameObject go)
    {
        Debug.Log($"{gameObject.name} was killed!");
        GameManager.Instance.achievements.Ach5Shoot();
        Destroy(gameObject);
    }
}
