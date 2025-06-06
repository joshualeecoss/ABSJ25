using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pickUpText; // Changed to TextMeshPro object (Josh)
    [SerializeField]
    private string collectableName; // Added variable (Josh)

    private bool pickUpAllowed;

    private int inspirationValue = 25;

    // Use this for initialization
    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    // Update called once per frame
    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) // Changed to string "Player" (Josh)
        {
            pickUpText.gameObject.SetActive(true);
            pickUpText.text = collectableName + "  (press E)";
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) // Changed to string "Player" (Josh)
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        // Add inspiration
        if (Inspiration.Instance != null)
        {
            Inspiration.Instance.AddInspiration(inspirationValue);
        }
        else
        {
            Debug.LogWarning("Inspiration system not found in the scene.");
        }

        // Clean up
        Destroy(gameObject);
    }
}