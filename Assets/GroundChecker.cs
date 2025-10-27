using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded;
    public BoxCollider2D boxCollider;

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}