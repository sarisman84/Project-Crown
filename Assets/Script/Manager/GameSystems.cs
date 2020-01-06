using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSystems {

    static Dictionary<string, int> weaponEnum = new Dictionary<string, int> ();
    static Dictionary<int, IWeaponStructure> weaponLibrary = new Dictionary<int, IWeaponStructure> ();

    public static void CreateWeapon<W> (W value) where W : IWeaponStructure {

        value.WeaponID = GenerateWeaponID;
        weaponEnum.Add (value.WeaponName, value.WeaponID);
        weaponLibrary.Add (weaponEnum[value.WeaponName], value);

    }

    public static IWeaponStructure GetWeaponFromLibrary (string weaponName) {
        return weaponLibrary[weaponEnum[weaponName]];
    }

    static int GenerateWeaponID {
        get {
            int id = 0;
            while (weaponLibrary.ContainsKey (id)) {
                id++;
            }
            return id;
        }
    }

}