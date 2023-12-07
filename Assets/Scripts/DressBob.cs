using UnityEngine;

public class DressBob : MonoBehaviour
{
    public GameObject bob; // Αναθέστε τον Bob στο πεδίο αυτό στον Inspector
    public GameObject BigCube; // Ο κύβος που μεγαλώνει τον Bob
    public GameObject SmallCube; // Ο κύβος που μικραίνει τον Bob

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            // Πάρτε τον renderer του κύβου
            Renderer cubeRenderer = other.GetComponent<Renderer>();

            // Εφαρμόστε τη υφή του κύβου στον Bob
            ApplyTextureToBob(cubeRenderer.material);
        }

        if (other.CompareTag("BigCube"))
        {
            // Πάρτε τον renderer του κύβου
            Renderer cubeRenderer = other.GetComponent<Renderer>();

            // Εφαρμόστε τη υφή του κύβου στον Bob
            ApplyTextureToBob(cubeRenderer.material);
            GetBigger();
        }
        else if (other.CompareTag("SmallCube"))
        {
            // Πάρτε τον renderer του κύβου
            Renderer cubeRenderer = other.GetComponent<Renderer>();

            // Εφαρμόστε τη υφή του κύβου στον Bob
            ApplyTextureToBob(cubeRenderer.material);
            GetSmaller();
        }
    }

    private void ApplyTextureToBob(Material cubeMaterial)
    {
        // Πάρτε όλους τους renderers του Bob
        Renderer[] bobRenderers = bob.GetComponentsInChildren<Renderer>();

        // Εφαρμόστε τη υφή του κύβου σε κάθε renderer του Bob
        foreach (Renderer bobRenderer in bobRenderers)
        {
            bobRenderer.material = cubeMaterial;
        }
    }

    private void GetBigger()
    {
        bob.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f); // Μεγαλώνει τον Bob
    }

    private void GetSmaller()
    {
        bob.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Μικραίνει τον Bob
    }
}
