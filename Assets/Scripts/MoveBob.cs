using UnityEngine;

public class MoveBob : MonoBehaviour
{
    public float[] speedGradients = { 1f, 2f, 5f, 8f, 10f }; // Speed gradients
    public int currentSpeedIndex = 2; // Initial speed level
    public Terrain terrain; // Assign the terrain in the Inspector
    public float explosionThreshold = 15f; // Threshold for explosion
    public float escalateMultiplier = 1.5f; // Multiplier for escalation
    public float reductionMultiplier = 1f; // Multiplier for gradual reduction

    private float explosionForce = 1000f; // Set the force of the explosion
    private float maxEscalation = 10f; // Set the maximum escalation before explosion
    private float minReduction = 0.2f; // Set the maximum escalation before explosion

    void Update()
    {
        AdjustSpeed();
        MoveCharacter();
        EscalateBob();
        ReduceBob();
    }

    void AdjustSpeed()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentSpeedIndex = Mathf.Min(currentSpeedIndex + 1, speedGradients.Length - 1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            currentSpeedIndex = Mathf.Max(currentSpeedIndex - 1, 0);
        }
    }

    void MoveCharacter()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned in the Inspector!");
            return;
        }

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        float newX = Mathf.Clamp(transform.position.x + horizontalMovement * speedGradients[currentSpeedIndex] * Time.deltaTime,
                                 terrain.transform.position.x, terrain.transform.position.x + terrain.terrainData.size.x);

        float newZ = Mathf.Clamp(transform.position.z + verticalMovement * speedGradients[currentSpeedIndex] * Time.deltaTime,
                                 terrain.transform.position.z, terrain.transform.position.z + terrain.terrainData.size.z);

        transform.position = new Vector3(newX, transform.position.y, newZ);

        // Move parallel to the Y-axis (altitude change)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E))
        {
            float verticalMove = 0f;

            if (Input.GetKey(KeyCode.W))
            {
                verticalMove = 1f;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                verticalMove = -1f;
            }

            float newY = Mathf.Clamp(transform.position.y + verticalMove * speedGradients[currentSpeedIndex] * Time.deltaTime,
                                     terrain.transform.position.y, terrain.transform.position.y + terrain.terrainData.size.y);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    void EscalateBob()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.localScale *= escalateMultiplier;
        }
        if (transform.localScale.x > maxEscalation)
        {
            Explode();
        }
    }

    void ReduceBob()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Gradually reduce the scale over time
            transform.localScale *= reductionMultiplier;
        }
        if (transform.localScale.x < minReduction)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Perform explosion
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.AddExplosionForce(explosionForce, transform.position, 5f);

        // Optionally, you can destroy the object after the explosion
        Destroy(gameObject);
    }
}
