using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject WeaponLogicSword;
    [SerializeField] private GameObject WeaponLogicAxe;

    public void EnableWeaponSword()
    {
        WeaponLogicSword.SetActive(true);
    }

    public void EnableWeaponAxe()
    {
        WeaponLogicAxe.SetActive(true);
    }

    public void DisableWeaponSword()
    {
        WeaponLogicSword.SetActive(false);
    }

    public void DisableWeaponAxe()
    {
        WeaponLogicAxe.SetActive(false);
    }
}
