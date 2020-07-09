using UnityEngine;


public class CraftTabsSwitch : MonoBehaviour
{
    public GameObject Cubes;
    public GameObject Weapons;
    public GameObject Engines;
    public GameObject Cosmetics;


    public void PressCubes()
    {
        Cubes.SetActive(true);
        Weapons.SetActive(false);
        Engines.SetActive(false);
        Cosmetics.SetActive(false);
    }


    public void PressWeapons()
    {
        Weapons.SetActive(true);
        Cubes.SetActive(false);
        Engines.SetActive(false);
        Cosmetics.SetActive(false);
    }


    public void PressEngines()
    {
        Engines.SetActive(true);
        Weapons.SetActive(false);
        Cubes.SetActive(false);
        Cosmetics.SetActive(false);
    }


    public void PressCosmetics()
    {
        Cosmetics.SetActive(true);
        Weapons.SetActive(false);
        Engines.SetActive(false);
        Cubes.SetActive(false);
    }
}