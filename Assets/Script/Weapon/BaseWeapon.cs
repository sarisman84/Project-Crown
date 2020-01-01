using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void WeaponBehaivour ();
public abstract class BaseWeapon : MonoBehaviour, IWeapon {

    protected float fireRate, time;
    public abstract void OnActivationTrigger ();

}
public abstract class BaseAmmo : MonoBehaviour, IAmmo {

    protected float damage, velocity;

    public abstract float BulletVelocity { get; }

    public abstract void OnCollisionEnter2D (Collision2D other);
}

public interface IAmmo {

    float BulletVelocity { get; }
    void OnCollisionEnter2D (Collision2D other);
}
public interface IWeapon {

    void OnActivationTrigger ();
}