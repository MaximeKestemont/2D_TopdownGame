using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class is used to store the resources loaded, so that the same texture is not loaded twice.
// Constants are also stored here.
public static class ResourceManager {



    /*
    ========================
    Player
    ========================
    */
    private static HeroScript mainPlayer;
    public static HeroScript MainPlayer { get { return mainPlayer; } }
	public static void RegisterMainPlayer(HeroScript player) {
		mainPlayer = player; 
	}


    /*
    ========================
    Inventory
    ========================
    */
    private static Inventory inventory;
    public static Inventory Inventory { get { return inventory; } }
    public static void RegisterInventory(Inventory inv) {
        inventory = inv; 
    }

    /*
    ========================
    Drugs
    ========================
    */
	public enum DrugEnum {SPEED_DRUG, DRUG2};
	public static Dictionary<DrugEnum, Drug> drugs = new Dictionary<DrugEnum, Drug> ();






	/*
    ========================
	Hallucinated objects
    ========================
    */
	public static int fadingDuration = 5;

	private static ArrayList hallucinatedObjects = new ArrayList();
	public static ArrayList HallucinatedObjects { get { return hallucinatedObjects; } }
	public static void AddHallucinatedObjects(HallucinatedObject obj) {
		hallucinatedObjects.Add(obj);
	}   



}
