using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Blaster,
    Sword, 
    Laser
}

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;
    public bool isAvailable;
    public GunType type;
    public int laserDamage;
    public GameObject ray;
    public BoxCollider2D rayCollider;
    public static GunController gun;
    public int ammo;
    public bool isAvailableToShoot;
    public Sprite weaponSprite;

    private void Awake()
    {
        gun = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isAvailableToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.UICanvas.passwordScreen.activeInHierarchy && !LevelManager.lvlManager.isPaused)
        {
            if (type == GunType.Blaster && !PlayerController.player.weapons[PlayerController.player.currentWeapon].CompareTag("PlayerSword"))
            {
                if (ammo > 0 && isAvailableToShoot)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        shotCounter = timeBetweenShots;
                        Audio.instance.PlayEffects(0);
                        ammo--;
                        UIController.UICanvas.ammoBarText.text = ammo.ToString();
                    }
                    if (Input.GetMouseButton(0))
                    {
                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            Instantiate(bullet, firePoint.position, firePoint.rotation);
                            Audio.instance.PlayEffects(0);
                            ammo--;
                            UIController.UICanvas.ammoBarText.text = ammo.ToString();
                            shotCounter = timeBetweenShots;
                        }
                    }
                }
            }
            else if(type == GunType.Laser && !PlayerController.player.weapons[PlayerController.player.currentWeapon].CompareTag("PlayerSword"))
            {
                if (ammo > 0 && isAvailableToShoot)
                {
                    if(Input.GetMouseButtonDown(0))
                        Audio.instance.PlayEffects(11);
                    if (Input.GetMouseButton(0))
                    {
                        ray.SetActive(true);
                        rayCollider.enabled = true;
                        if (shotCounter > 0)
                            shotCounter -= Time.deltaTime;
                        else
                        {
                            ammo--;
                            UIController.UICanvas.ammoBarText.text = ammo.ToString();
                            shotCounter = timeBetweenShots;
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    rayCollider.enabled = false;
                    ray.SetActive(false);
                    shotCounter = timeBetweenShots;
                }
            }
            else if(type == GunType.Sword)
            {
                if (Input.GetMouseButtonDown(0) && isAvailableToShoot)
                {
                    PlayerController.player.anim.SetTrigger("sword");
                    Audio.instance.PlayEffects(9);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (type == GunType.Laser)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().DamageEnemy(laserDamage);
            }
        }
    }
}
