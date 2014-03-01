using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    public enum Weapon { Lasers, Torpedo, Missiles, Railgun, MiningLaser }
    

    public Weapon hardpoint1;
    public Weapon hardpoint2;
    public Weapon hardpoint3;
    public Weapon hardpoint4;

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
