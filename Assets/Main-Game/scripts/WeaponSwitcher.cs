using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon15;
    public GameObject weapon2;

    void Start()
    {
        // Ensure only weapon 1 is active at the start
        SelectWeapon(1);
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
    }

    void SelectWeapon(int weaponNumber)
    {
        if (weaponNumber == 1)
        {
            weapon15.SetActive(true);
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
        else if (weaponNumber == 2)
        {
            weapon1.SetActive(false);
            weapon15.SetActive(false);
            weapon2.SetActive(true);
        }
    }
}
