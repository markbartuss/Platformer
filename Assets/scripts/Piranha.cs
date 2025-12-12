using UnityEngine;
using System.Collections;
public class Piranha : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 3f;
    public float jumpHeight = 2f;
    public float jumpSpeed = 3f;

    private Vector3 startPos;
    private bool isJumping = false;
    private float timer;

    void Start()
    {
        startPos = transform.position;
        timer = Random.Range(minDelay, maxDelay);
    }
    void Update()
    {
        if (!isJumping)//if bool is false
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                StartCoroutine(Jumping());
            }
        }
    }

    private IEnumerator Jumping()
    {
        isJumping = true;

        Vector3 peak = startPos + Vector3.up * jumpHeight;

        // jump up
        while (Vector3.Distance(transform.position, peak) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, peak, jumpSpeed * Time.deltaTime);
            yield return null;
        }

        // Move back 
        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, jumpSpeed * Time.deltaTime);
            yield return null;
        }

        // Reset timer and jumping 
        timer = Random.Range(minDelay, maxDelay);
        isJumping = false;
    }
}
