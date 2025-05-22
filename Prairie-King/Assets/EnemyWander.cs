using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    public float minDirectionTime = 1f;
    public float maxDirectionTime = 4f;

    private float currentSpeed;
    private int direction = 0; // -1: izquierda, 0: quieto, 1: derecha
    private float changeDirectionTime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewDirection();
    }

    void Update()
    {
        changeDirectionTime -= Time.deltaTime;

        // Mover solo horizontal, sin velocidad vertical (bloqueada)
        rb.velocity = new Vector2(currentSpeed * direction, 0);

        if (changeDirectionTime <= 0f)
        {
            PickNewDirection();
        }
    }

    void PickNewDirection()
    {
        direction = Random.Range(-1, 2); // -1, 0, 1
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        changeDirectionTime = Random.Range(minDirectionTime, maxDirectionTime);

        // Voltear sprite solo si se mueve a izquierda o derecha
        if (direction == 1)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (direction == -1)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
