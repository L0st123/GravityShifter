using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject weapon1;
   // public GameObject weapon15;
    public GameObject weapon2;
    public Animator animator;

    void Start()
    {
        animator.SetBool("ShootAR", false);
        animator.SetBool("ShootPistol", false);

        SelectWeapon(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(1);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(3);
        }
    }

    void SelectWeapon(int weaponNumber)
    {
        if (weaponNumber == 1)
        {
            animator.SetBool("ShootAR", false);
            animator.SetBool("ShootPistol", true);
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
        else if (weaponNumber == 2)
        {
            animator.SetBool("ShootAR", true);
            animator.SetBool("ShootPistol", false);
            weapon1.SetActive(false);
            
            weapon2.SetActive(true);
        }
        else if (weaponNumber == 3)
        {
            
            weapon1.SetActive(false);
            weapon2.SetActive(false);
        }
        
        
    }
    public void EquipFinished()
    {
        animator.SetBool("ShootPistol", false);
        animator.SetBool("ShootAR", false);

        //allow shoot
    }
}
