using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{

    public Text weaponText;
   public void ChangeWeapon(string weapon)
    {
        weaponText.text = weapon;
    }
}
