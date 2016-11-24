using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
    public List<GameObject> waypoints;
    public GameObject currentWaypoint;
    public ParticleSystem deathParticle;
    public SpriteRenderer spRenderer;
    public float speed;

    Vector2 startPos;
    float journeyLength;
    float startTime;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
        GetNextWaypoint();
        journeyLength = Vector2.Distance(startPos, currentWaypoint.transform.position);
    }

    public void GetNextWaypoint()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] != currentWaypoint)
            {
                currentWaypoint = waypoints[i];
                return;
            }
        }
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        transform.position = Vector2.Lerp(startPos, currentWaypoint.transform.position, fracJourney);

        transform.right = currentWaypoint.transform.position - transform.position;

        if (Vector2.Distance(transform.position, currentWaypoint.transform.position) < 0.02f)
        {
            GetNextWaypoint();
            startPos = transform.position;
            journeyLength = Vector2.Distance(startPos, currentWaypoint.transform.position);
            startTime = Time.time;
        }
    }

    public void FixAfterRotation()
    {
        startPos = transform.position;
        journeyLength = Vector2.Distance(startPos, currentWaypoint.transform.position);
        startTime = Time.time;
    }

    public void Die()
    {
        deathParticle.Play();
        spRenderer.gameObject.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine("DisableObject");
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Bullet"))
        {
            audioSource.Play();
            Die();
            collision.gameObject.SetActive(false);
        }

        if(collision.gameObject.tag.Equals("Player"))
        {
            GameManager.instance.playerScript.Die();
        }
    }
}
