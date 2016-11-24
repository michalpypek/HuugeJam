using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Text ammoText;
    public GameObject maze;
    public GameObject handIK;
    public GameObject weaponObj;
    public GameObject sprites;
    public ParticleSystem stompParticle;
    public WeaponScript currentWeapon = null;
    public AudioClip stompSound;
    public AudioClip deathSound;
    public AudioClip rotateSound;
    public AudioClip keySound;
    public AudioClip doorSound;
    public ParticleSystem bloodParticle;

    public float horizontalSpeed;
    public float verticalSpeed;
    public float rotationSpeed;

    bool isRotating = false;

    float yVelo;
    float startTime;
    float journeyLength;
    Vector2 startPos;
    Vector3 FlipLeft = new Vector3(-1, 1, 1);
    Vector3 FlipRight = new Vector3(1, 1, 1);
    Vector2 moveVector;
    Rigidbody2D rbody;
    AudioSource audioSource;

    void Start()
    {
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        ammoText.text = "Ammo: " + currentWeapon.ammo;
        handIK.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon != null)
            {
                currentWeapon.Fire();
            }
        }

        if (!isRotating)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                moveVector.x = Input.GetAxis("Horizontal") * horizontalSpeed;
                moveVector.y = rbody.velocity.y;
                rbody.velocity = moveVector;
                gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, 0, 0));
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                audioSource.clip = rotateSound;
                audioSource.Play();
                GameManager.instance.enemyManager.PrepareToRotate();
                maze.transform.eulerAngles = new Vector3(0, 0, maze.transform.rotation.eulerAngles.z - 90);
                GameManager.instance.enemyManager.FixAfterRotation();
                StartCoroutine("RotateChar");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                audioSource.clip = rotateSound;
                audioSource.Play();
                GameManager.instance.enemyManager.PrepareToRotate();
                maze.transform.eulerAngles = new Vector3(0, 0, maze.transform.rotation.eulerAngles.z + 90);
                GameManager.instance.enemyManager.FixAfterRotation();
                StartCoroutine("RotateChar");
            }
        }

        if (isRotating)
        {
            float distCovered = (Time.time - startTime) * rotationSpeed;
            float fracJourney = distCovered / journeyLength;

            transform.right = Vector2.Lerp(startPos, Vector2.right, fracJourney);
        }

    }

    void FixedUpdate()
    {
        yVelo = rbody.velocity.y;
    }

    IEnumerator RotateChar()
    {
        rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        isRotating = true;
        startTime = Time.time;
        journeyLength = Vector2.Angle(transform.right, Vector2.right);
        startPos = transform.right;
        yield return new WaitForSeconds(1f);
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        isRotating = false;
    }

    public void Die()
    {
        sprites.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        audioSource.clip = deathSound;
        audioSource.Play();
        bloodParticle.Play();
        StartCoroutine("EndGame");
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.GameOver();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Key"))
        {
            Debug.Log("Key");
            GameManager.instance.door.Unlock();
            collision.gameObject.SetActive(false);
            audioSource.clip = keySound;
            audioSource.Play();
        }

        if (collision.gameObject.tag.Equals("Door"))
        {
            if (GameManager.instance.door.isUnlocked)
            {
                audioSource.clip = doorSound;
                audioSource.Play();
                GameManager.instance.WinGame();
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if(yVelo< -3 || yVelo >3)
        {
            audioSource.clip = stompSound;
            audioSource.Play();
            stompParticle.Play();
        }
    }
}
