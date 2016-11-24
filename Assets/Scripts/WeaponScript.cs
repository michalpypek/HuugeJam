using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firingPoint;
    public ParticleSystem particles;
    public Sprite weaponSprite;
    public Sprite weaponUnloadedSprite;
    public SpriteRenderer sRenderer;
    public AudioClip gunShot;
    public AudioClip gunClick;

    public float rateOfFire;
    public float shotForce;
    public int ammo;

    bool canFire = true;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if (canFire && ammo > 0)
        {
            ammo--;
            canFire = false;
            sRenderer.sprite = weaponUnloadedSprite;
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, new Vector3(firingPoint.transform.position.x, firingPoint.transform.position.y,1), firingPoint.transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * -shotForce, ForceMode2D.Impulse);
            particles.Play();
            audioSource.clip = gunShot;
            audioSource.Play();

            if (ammo <= 0)
            {
                return;
            }
            StartCoroutine("FireEnum");
        }
        
        if (ammo <= 0)
        {
            audioSource.clip = gunClick;
            audioSource.Play();
        }

    }

    IEnumerator FireEnum()
    {
        yield return new WaitForSeconds(rateOfFire);
        sRenderer.sprite = weaponSprite;
        canFire = true;
    }
}
