using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BulletEffect (ref Vector2 velocity, ref float desiredVelocity);
public delegate void BulletAfterEffect ();

[RequireComponent (typeof (Rigidbody2D))]
public class Bullet : MonoBehaviour {

    public struct Info {
        public float damage, desiredVelocity, lifeSpan;
        public int ownerID;

        public Info (
            float _damage, float _desiredVelocity, float _lifeSpan,
            int _ownerID) {
            damage = _damage;
            desiredVelocity = _desiredVelocity;
            lifeSpan = _lifeSpan;
            ownerID = _ownerID;
        }
    }

    public Info UpdateBulletOwner {
        set {
            info = value;
            Debug.Log ($" Bullet Damage: {info.damage}, Bullet Velocity: {info.desiredVelocity}, Bullet LifeSpan: {info.lifeSpan}");

        }
    }

    public BulletEffect CreateEffect {
        set => effect = value;
    }
    public BulletAfterEffect CreateAfterEffect {
        set => afterEffect = value;
    }

    Info info;
    Vector2 velocity;
    Rigidbody2D physics2D;

    BulletEffect effect;
    BulletAfterEffect afterEffect;

    float time;

    private void OnEnable () {
        PhysicsSetup ();
        time = 0;
    }

    private void OnDisable () {
        Debug.Log ($"{gameObject.name} got Disabled");
        if (afterEffect != null)
            Invoke ("afterEffect", 0);
    }

    private void Update () {
        time += Time.deltaTime;
        time = Mathf.Clamp (time, 0, info.lifeSpan);
        if (time == info.lifeSpan) gameObject.SetActive (false);
    }

    private void PhysicsSetup () {
        physics2D = physics2D ?? gameObject.GetComponent<Rigidbody2D> ();
        physics2D.gravityScale = (physics2D.gravityScale != 0) ? 0 : physics2D.gravityScale;
    }

    private void FixedUpdate () {
        Vector2 currentVelocity = physics2D.velocity;
        velocity = transform.up.normalized;
        if (effect != null)
            effect (ref currentVelocity, ref info.desiredVelocity);
        float finalDesiredVelocity = Mathf.Clamp (info.desiredVelocity, 2, Mathf.Infinity);
        currentVelocity += velocity * finalDesiredVelocity;
        physics2D.velocity = currentVelocity;

    }

    private void OnCollisionEnter2D (Collision2D other) {
        IInteractable entity = other.collider.GetComponent<IInteractable> ();
        if (other.collider.GetInstanceID () == info.ownerID) return;
        if (entity == null) {
            gameObject.SetActive (false);
            return;
        }
        entity.TakeDamage (info.damage);
        gameObject.SetActive (false);
    }

}