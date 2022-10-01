using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded { private set; get; } = false;

    [SerializeField] private Transform ground;
    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        // TODO: hardcoded values
        isGrounded = Physics2D.OverlapBox(ground.position, new Vector2(1f, 0.15f), 0, groundLayer);
    }
}
