using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            Player fishes = other.GetComponent<Player>();

            fishes.door = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player fishes = other.GetComponent<Player>();

            fishes.door = false; 

        }
    }
}
