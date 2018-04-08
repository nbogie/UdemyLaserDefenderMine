using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public KeyCode leftKey;
    public PowerUp.WeaponType x;

    public KeyCode rightKey;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode fireKey;
    public LevelManager levelManager;

    public float moveSpeed;
    public float health;

    public Rigidbody2D laserPrefab;

    private float xMin;
    private float xMax;

    private float timeLaserIsNextReady = -1;
    [Range(0.1f, 2)]
    public float laserCooloffTime = 0.5f;
    private float minLaserCooloffTime = 0.1f;

    public float laserMoveSpeed = 3;
    public AudioClip[] dmgSounds;
    public AudioClip[] deathSounds;

    public ParticleSystem dmgParticlesPrefab;
    private ParticleSystem dmgParticles;

    private CameraShaker cameraShaker;
    private PowerUp.WeaponType weaponType;


    void PlayDamageSound(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(dmgSounds[Random.Range(0, dmgSounds.Length)], pos);
    }

    void PlayDeathSound(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0, deathSounds.Length)], pos);
    }
    public float GetHealth()
    {
        return health;
    }
    void Start()
    {
        weaponType = PowerUp.WeaponType.Single;

        dmgParticles = Instantiate(dmgParticlesPrefab) as ParticleSystem;
        cameraShaker = Camera.main.GetComponent<CameraShaker>();
        levelManager = FindObjectOfType<LevelManager>();
        health = 100;
        float zDist = transform.position.z - Camera.main.transform.position.z;
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0, zDist)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0, zDist)).x;
    }

    void move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        float x = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector2(x, transform.position.y);
    }

    void Update()
    {
        if (Input.GetKey(upKey))
        {
            move(Vector2.up);
        }
        if (Input.GetKey(downKey))
        {
            move(Vector2.down);
        }
        if (Input.GetKey(leftKey))
        {
            move(Vector2.left);
        }
        if (Input.GetKey(rightKey))
        {
            move(Vector2.right);
        }
        if (Input.GetKey(fireKey))
        {
            tryToFireWeapon();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            weakenForDebug();
        }
    }

    void weakenForDebug()
    {
        TakeDamage(health - 1f);
    }

    void tryToFireWeapon()
    {
        if (Time.time > timeLaserIsNextReady)
        {
            fireWeapon();
            timeLaserIsNextReady = Time.time + laserCooloffTime;
        }
    }

    void fireWeaponAtAngle(Vector3 offset, Quaternion rot, Vector3 vel)
    {
        Rigidbody2D bullet = Instantiate(laserPrefab, transform.position + offset, rot) as Rigidbody2D;
        bullet.GetComponent<Rigidbody2D>().velocity = vel;
    }

    void fireWeapon()
    {
        //TODO: figure out velocity from angle (or rotation from velocity)
        fireWeaponAtAngle(Vector3.forward, Quaternion.identity, Vector2.up * laserMoveSpeed);
        if (weaponType == PowerUp.WeaponType.FiveWay || weaponType == PowerUp.WeaponType.ThreeWay)
        {
            fireWeaponAtAngle(Vector3.forward + Vector3.left * 0.15f, Quaternion.AngleAxis(-26.565f, Vector3.back), new Vector3(-1, 2, 0).normalized * laserMoveSpeed);
            fireWeaponAtAngle(Vector3.forward + Vector3.right * 0.15f, Quaternion.AngleAxis(26.565f, Vector3.back), new Vector3(1, 2, 0).normalized * laserMoveSpeed);
        }
        if (weaponType == PowerUp.WeaponType.FiveWay){
            fireWeaponAtAngle(Vector3.forward + Vector3.left * 0.3f, Quaternion.AngleAxis(-45f, Vector3.back), new Vector3(-1, 1, 0).normalized * laserMoveSpeed);
            fireWeaponAtAngle(Vector3.forward + Vector3.right * 0.3f, Quaternion.AngleAxis(45f, Vector3.back), new Vector3(1, 1, 0).normalized * laserMoveSpeed);            
        }   
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            PlayDamageSound(transform.position);
            cameraShaker.ShakeCamera(1.0f, 0.4f);

        }
    }

    void Die()
    {

        levelManager.GotoLoseScreenAfterDelay(3);
        PlayDeathSound(transform.position);
        Destroy(gameObject);
    }
    public void ImproveFireRate()
    {
        laserCooloffTime = laserCooloffTime / 2f;
        if (laserCooloffTime < minLaserCooloffTime)
        {
            laserCooloffTime = minLaserCooloffTime;
        }
    }
    private PowerUp.WeaponType RandomWeaponType(){
        PowerUp.WeaponType[] ts = PowerUp.allWeaponTypes();
        return ts[Random.Range(0, ts.Length)];
    }
    public void ReceivePowerUp()
    {
        Debug.Log("got power up!");
        weaponType = RandomWeaponType();
        ImproveFireRate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        EnemyLaser enemyLaser = other.GetComponent<EnemyLaser>();
        if (enemyLaser)
        {
            TakeDamage(enemyLaser.GetDamage());
            dmgParticles.transform.position = other.transform.position;
            dmgParticles.Play();
            enemyLaser.Hit();
        }
    }

}
