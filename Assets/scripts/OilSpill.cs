using UnityEngine;

public class OilSpill : MonoBehaviour
{

    public void Start()
    {
        Debug.Log("start spill");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            Player movement = other.GetComponent<Player>();
            if (movement != null)
            {
                movement.isSlippery = true; // enable slippery mode
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player movement = other.GetComponent<Player>();
            if (movement != null)
            {
                movement.isSlippery = false; // disable slippery mode
            }
        }
    }
}
