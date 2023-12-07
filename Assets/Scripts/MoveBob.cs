using UnityEngine;

public class MoveBob : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Terrain terrain; // Assign the terrain in the Inspector

    void Update()
    {
        MoveCharacter();
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

        float newX = Mathf.Clamp(transform.position.x + horizontalMovement * moveSpeed * Time.deltaTime,
                                 terrain.transform.position.x, terrain.transform.position.x + terrain.terrainData.size.x);

        float newZ = Mathf.Clamp(transform.position.z + verticalMovement * moveSpeed * Time.deltaTime,
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

            float newY = Mathf.Clamp(transform.position.y + verticalMove * moveSpeed * Time.deltaTime,
                                     terrain.transform.position.y, terrain.transform.position.y + terrain.terrainData.size.y);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
