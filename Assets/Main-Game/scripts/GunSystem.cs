using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{

    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public GameObject Pistol;
    public GameObject AssaultRifle;

    EnemyScript2 enemyScript;
    RangedEnemy rangedEnemy;
    bool shooting, readyToShoot, reloading;


    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public LayerMask target;




    public Animator animator;
    public Animator pistolAnimations;
    public Animator assaultRifleAnimations;

    public GameObject muzzleFlash, bulletHoleGraphic;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    public GameObject player;

    public AudioSource audioSource; 
    public AudioClip shootingSound;

    public AudioClip reloadSound;
    public float clipLength = 1f; 


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        allowButtonHold = true;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown("1"))
        {
            animator.SetBool("PistolShoot", true);
        }
        else if (Input.GetKeyDown("2"))
        {
            animator.SetBool("ARShooting", true);
        }

        MyInput();
        text.SetText(bulletsLeft + " / " + magazineSize);
        print("enemy health = " + enemyScript.health);
        print("ready to shoot=" + readyToShoot);
        print("shoot=" + shooting);
        print("reloading=" + reloading);
        print("bullets left=" + bulletsLeft);
    }

    private void MyInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading || bulletsLeft == 0 && !reloading )
        {
            Reload();
            audioSource.PlayOneShot(reloadSound);    
        }

        // Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
      

        if (bulletsLeft > 0)
        {
            audioSource.PlayOneShot(shootingSound);
        }





            readyToShoot = false;


        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        Vector3 direction = player.transform.forward + new Vector3(x, y, 0);


     //   Debug.DrawRay(player.transform.position, direction * 10, Color.green, 20, false);


        if (Physics.Raycast(player.transform.position, direction, out rayHit, Mathf.Infinity, whatIsEnemy))
        {
            Debug.Log("Hit: " + rayHit.collider.name);
            EnemyScript2 hitEnemy = rayHit.collider.GetComponent<EnemyScript2>(); 
            RangedEnemy hitRangedEnemy = rayHit.collider.GetComponent<RangedEnemy>();   

            if (hitEnemy != null )
            {
                hitEnemy.health -= 10f;
                print("enemy health = " + hitEnemy.health);
               
            }
            if ( hitRangedEnemy != null)
            {
                hitRangedEnemy.health -= 10f;
                print("ranged enemy health = " + hitRangedEnemy.health);
            }
            else
            {
                print("enemy objct is null");
            }

            if (bulletHoleGraphic != null)
            {
                GameObject hole = Instantiate(bulletHoleGraphic, rayHit.point + rayHit.normal * 0.01f, Quaternion.LookRotation(rayHit.normal));
                Destroy(hole, 1.5f);
            }
        }
        else
        {
            print("shoot didn't hit anything");
        }





      /*  if (Physics.Raycast(player.transform.position, direction, out rayHit, Mathf.Infinity, target))
        {
            Debug.Log("Hit: " + rayHit.collider.name);
            Target hitTarget = rayHit.collider.GetComponent<Target>();
            if (hitTarget != null)
            {
                hitTarget.health -= 100f;
                print("Target health = " + hitTarget.health);
            }
            else
            {
                print("target objct is null");
            }

            if (bulletHoleGraphic != null)
            {
                GameObject hole = Instantiate(bulletHoleGraphic, rayHit.point + rayHit.normal * 0.01f, Quaternion.LookRotation(rayHit.normal));
                Destroy(hole, 1.5f);
            }
        }
        else
        {
            print("shoot didn't hit anything");
        }
      */





        if (muzzleFlash != null)
        {
            GameObject flashInstance = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            Destroy(flashInstance, 0.1f);
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);





    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        animator.SetTrigger("Reload");
        pistolAnimations.SetTrigger("Reload");
        assaultRifleAnimations.SetTrigger("Reload");

        

        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}