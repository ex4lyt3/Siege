using UnityEngine;

public class ToolController : MonoBehaviour
{

    public Animator playerAnimator;
    public Camera fpsCam;
    public AmnoUI AmnoUI;
    public WeaponUI WeaponUI;

    public Transform sword;

    public float damage = 30f;
    public float meleeDamage = 20f;
    public float range = 100f;
    public float rate;

    int weaponState = 0;
    int swordState = 0;
    int gunState = 0;
    int amno = 8;

    bool gunReady = true;

    // Update is called once per frame
    void Update()
    {
        //Plays the equip animation
        if (Input.GetKey(KeyCode.Alpha1) && weaponState != 1 && gunState < 2)
        {
            weaponState = 1; //gun
            playerAnimator.SetInteger("WeaponState", weaponState); //plays
            gunState = 1; //GunIdle
            playerAnimator.SetInteger("GunState", gunState);
            swordState = 0; //resets state 
                      
            WeaponUI.ChangeWeapon("PISTOL");
            AmnoUI.ChangeAmno(amno.ToString());

        }
        else if (Input.GetKey(KeyCode.Alpha2) && weaponState != 2 && swordState < 2)
        {

            weaponState = 2; //sword
            playerAnimator.SetInteger("WeaponState", weaponState); //plays
            swordState = 1; //idle
            playerAnimator.SetInteger("SwordState", swordState);
            gunState = 0;

            WeaponUI.ChangeWeapon("SWORD");
            AmnoUI.ChangeAmno("");
        }
        else if (Input.GetKey(KeyCode.Alpha3) && weaponState != 0)
        {
            weaponState = 0; //sword
            playerAnimator.SetInteger("WeaponState", weaponState); //plays
            gunState = 0;
            swordState = 0;
            WeaponUI.ChangeWeapon("NONE");
            AmnoUI.ChangeAmno("");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            switch (weaponState)
            {

                case 1:
                    if (amno > 0 && gunState == 1 && gunReady == true)
                    {
                        Shoot();
                    }
                    break;

                case 2:
                    Slash();
                    break;

                default:
                    break;
            }
        }

        if (Input.GetKey(KeyCode.R) && gunState == 1)
        {
            Reload();
        }
    }

    void GunIdle()
    { 
        gunState = 1;
        playerAnimator.SetInteger("GunState", gunState);
        WeaponUI.ChangeWeapon("PISTOL");
        AmnoUI.ChangeAmno(amno.ToString());
        gunReady = true;
    }

    void Shoot()
    {
        gunReady = false;
        gunState = 2;
        playerAnimator.SetInteger("GunState", gunState);
        amno -= 1;
        AmnoUI.ChangeAmno(amno.ToString());
        RaycastHit hit;

        Invoke("GunIdle", rate);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hit,range))
        {
            TakeDamage target = hit.transform.GetComponent<TakeDamage>();

            if (target != null)
            {
                target.Damage(damage);
            }
        }
    }

    void Reload()
    {
        gunReady = false;
        gunState = 3;
        playerAnimator.SetInteger("GunState", gunState);
        WeaponUI.ChangeWeapon("RELOADING");

        Invoke("GunIdle", 2f);
        amno = 8;
    }

    void Slash()
    {
         swordState = 2;
         playerAnimator.SetInteger("SwordState", swordState);


        RaycastHit[] hit = Physics.SphereCastAll(transform.position, 5, transform.forward);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.tag == "Enemy")
            {
                TakeDamage target = hit[i].transform.GetComponent<TakeDamage>();
                target.Damage(meleeDamage);
                Debug.Log("Enemy Hit!");
            }
        }

            Invoke("ResetCombo", 0.5f);     
    }

    void ResetCombo()
    { 
            swordState = 1;
            playerAnimator.SetInteger("SwordState", swordState);
    }

}
