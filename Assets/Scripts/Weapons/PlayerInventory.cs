using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{

    //equipped weapons
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public Weapon tertiaryWeapon;
    public Weapon utilityWeapon;
    public Weapon[] allWeapons;

    private int credits;

    
    public int GetCredits()
    {
        return credits;
    }

	// Use this for initialization
	void Start () {
        credits = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public bool CreateTransaction(int amount)
    {
        if(credits+amount>=0)
        {
            credits+=amount;
            return true;
        }
        else
            return false;
    }
    


}
