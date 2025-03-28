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

    
    bool shooting, readyToShoot, reloading;

    
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public Animator animator;
    public Animator pistolAnimations;
    public Animator assaultRifleAnimations;

    public GameObject muzzleFlash, bulletHoleGraphic;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;
  

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

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
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
      

        readyToShoot = false;
        

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

      
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log("Hit: " + rayHit.collider.name);

     
            if (bulletHoleGraphic != null)
            {
                GameObject hole = Instantiate(bulletHoleGraphic, rayHit.point + rayHit.normal * 0.01f, Quaternion.LookRotation(rayHit.normal));
                Destroy(hole, 5f); 
            }
        }

       
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
    //PUT THIS ON OTHER SCRIPT
   
}