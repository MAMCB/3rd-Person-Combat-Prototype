using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject WeaponLogicSword;
    [SerializeField] private GameObject WeaponLogicAxe;
    [SerializeField] private GameObject[] multipleWeaponLogics;

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

    public void EnableMultipleWeaponLogics()
    {
        foreach(GameObject gameObject in multipleWeaponLogics)
        {
            gameObject.SetActive(true);
        }
    }

    public void DisableMultipleWeaponLogics()
    {
        foreach (GameObject gameObject in multipleWeaponLogics)
        {
            gameObject.SetActive(false);
        }
    }

    
}
