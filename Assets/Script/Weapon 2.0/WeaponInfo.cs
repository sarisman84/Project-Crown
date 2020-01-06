using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Default }

public delegate void WeaponPattern (int ownerID, Transform barrel);

[System.Serializable]
public class WeaponHolder {
    [SerializeField]
    int ownerID;

    [SerializeField]
    List<IWeaponStructure> weaponInventory = new List<IWeaponStructure> ();
    [SerializeField]
    IWeaponStructure selectedWeapon;

    public int SelectAWeapon {
        set {
            value = Mathf.Clamp (value, 0, weaponInventory.Count - 1);
            selectedWeapon = weaponInventory[value];
        }
    }
    public IWeaponStructure AddWeaponToInventory {
        set {
            weaponInventory.Add (value);
        }
    }
    public void RemoveWeaponFromInventory (int index) {
        weaponInventory.RemoveAt (index);
    }
    public List<IWeaponStructure> WeaponInventory => weaponInventory;

    public WeaponHolder (MonoBehaviour owner) {
        ownerID = owner.gameObject.GetInstanceID ();
        owner.StartCoroutine (LocalUpdate ());
    }

    private IEnumerator LocalUpdate () {
        while (true) {

            time += Time.deltaTime;
            if (selectedWeapon == null) {
                yield return new WaitForEndOfFrame ();
                continue;
            }
            time = Mathf.Clamp (time, 0, selectedWeapon.FireRate);
            yield return new WaitForEndOfFrame ();
        }
    }

    [SerializeField]
    float time;
    public void TriggerAttackFunction (GameObject owner, Transform barrel) {
        if (selectedWeapon == null && weaponInventory.Count != 0) {
            SelectAWeapon = 0;
        }
        if (selectedWeapon == null || time != selectedWeapon.FireRate) return;
        selectedWeapon.Trigger (owner.GetInstanceID (), barrel);
        time = 0;

    }

}

public static partial class WeaponBlueprints {

    [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void SetupPlayerWeapon () {
        if (ObjectPooler.ObjectPoolerInventory.Count == 0)
            SetupPooler ();

        GameSystems.CreateWeapon (new Default ());
        // player.weaponHolder.AddWeaponToInventory = GameSystems.GetWeaponFromLibrary ("Default");
        // player.weaponHolder.SelectAWeapon = 0;
    }

    static void SetupPooler () {
        OffensivePlayerSystems player = GameObject.FindObjectOfType<OffensivePlayerSystems> ();
        Bullet instance = new GameObject ("Bullet").AddComponent<Bullet> ();
        instance.transform.localScale = Vector3.one / 2;
        SpriteRenderer bulletImage = instance.gameObject.AddComponent<SpriteRenderer> ();
        Texture2D texture = Resources.Load ("bulletCircle") as Texture2D;
        TextureScale.Point (texture, 40, 40);
        bulletImage.sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5f, 0.5f));
        ObjectPooler.PoolObject (instance.gameObject, 30, new GameObject ("List").transform);
        MonoBehaviour.Destroy (instance.gameObject);
    }
}