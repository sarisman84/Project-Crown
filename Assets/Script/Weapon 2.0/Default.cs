using UnityEngine;

public class Default : IWeaponStructure {

    [SerializeField] protected float damage = 10f, bulletSpeed = 5f, fireRate = 0.3f;

    public float Damage => damage;

    public float BulletVeloclity =>
        bulletSpeed;

    public float FireRate =>
        fireRate;

    public WeaponPattern Trigger => OnAwakeTrigger;

    int id;

    public string WeaponName {
        get => "Default";

    }
    public int WeaponID {
        get => id;
        set => id = value;
    }

    void OnAwakeTrigger (int ownerID, Transform barrel) {
        Bullet obj = ObjectPooler.GetPooledObject<Bullet> ();
        obj.gameObject.SetActive (true);
        obj.UpdateBulletOwner = new Bullet.Info (damage, bulletSpeed, 5f, ownerID);
        OnTrigger (barrel, obj);

    }

    protected virtual void OnTrigger (Transform barrel, Bullet obj) {
        obj.transform.position = barrel.transform.position;
        obj.transform.rotation = barrel.transform.parent.rotation;
    }
}