using UnityEngine;

public class Collectable : MonoBehaviour
{
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<Player>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player touched it
        if (other.CompareTag("Player"))
        {

            player.fishes--;

            // Destroy the object
            Destroy(gameObject);
        }
    }
}
