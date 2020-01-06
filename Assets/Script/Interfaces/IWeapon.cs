using UnityEngine;
public interface IWeaponStructure {

    float Damage { get; }
    float BulletVeloclity { get; }
    float FireRate { get; }

    string WeaponName { get; }
    int WeaponID { set; get; }
    WeaponPattern Trigger { get; }
}