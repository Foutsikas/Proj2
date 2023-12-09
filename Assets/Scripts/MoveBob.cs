using UnityEngine;

public class MoveBob : MonoBehaviour
{
    public float[] speedGradients = { 1f, 2f, 5f, 8f, 10f }; // Speed gradients
    public int currentSpeedIndex = 2; // Initial speed level
    public Terrain terrain; // Assign the terrain in the Inspector

    void Update()
    {
        AdjustSpeed();
        MoveCharacter();
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
}
