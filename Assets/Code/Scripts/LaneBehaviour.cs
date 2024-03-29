using System.Collections;
using UnityEngine;

public class LaneBehaviour : MonoBehaviour
{
    [HideInInspector] public Coroutine temp;

    public bool SwapLane(bool currentLane, Rigidbody2D rb, GameObject character)
    {
        // El personaje salta antes del cambio de linea
        rb.AddForce(Vector2.up * 40, ForceMode2D.Impulse);

        // tf you talking about
        if (temp != null) StopCoroutine(temp);
        character.GetComponent<Collider2D>().enabled = false;
        temp = StartCoroutine(LaneJump(currentLane, rb, character));
        return !currentLane;
    }

    IEnumerator LaneJump(bool currentLane, Rigidbody2D rb, GameObject character)
    {
        do
        { yield return new WaitForSeconds(0.02f);
        } while (character.transform.position.y <= 12);

        character.GetComponent<Collider2D>().enabled = true;

        // Change to top lane
        if (!currentLane)
        {
            rb.mass = 0.6f;
            character.layer = 7; // Top layer
            character.transform.localScale = new Vector3(1f, 1f, 1f);
            character.transform.position = new Vector3(character.transform.position.x, 12.5f, 4f);
        }

        // Change to bottom lane
        else
        {
            rb.mass = 0.5f;
            character.layer = 6; // Bottom layer
            character.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            character.transform.position = new Vector3(character.transform.position.x, 12.5f, 0f);
        }

        rb.velocity = Vector3.zero;
    }
}
