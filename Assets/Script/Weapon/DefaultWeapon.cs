using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class DefaultWeapon : BaseWeapon {
    [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void CreateBullets () {
        DefaultAmmo testSubject = new GameObject ("Bullet").AddComponent<DefaultAmmo> ();

        DefaultWeapon weapon = GameObject.FindObjectOfType<DefaultWeapon> ();
        weapon.bulletId = testSubject.gameObject.GetInstanceID ();
        testSubject.gameObject.AddComponent<SpriteRenderer> ().sprite = weapon.ammoSprite;
        testSubject.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
        ObjectPooler.PoolObject (testSubject.gameObject, 30, new GameObject ("List").transform);
        weapon.InvokeRepeating ("OnActivationTrigger", 0, 0.01f);
        weapon.InvokeRepeating ("RemoveBullet", 1, 5f);
        Destroy (testSubject.gameObject);

    }

    [SerializeField] new protected float fireRate;
    new protected float time;

    [HideInInspector] public int bulletId;
    public Sprite ammoSprite;
    List<DefaultAmmo> ammos = new List<DefaultAmmo> ();
    public override void OnActivationTrigger () {
        time += Time.deltaTime;
        time = Mathf.Clamp (time, 0, fireRate);
        if (time == fireRate && Input.GetKey (KeyCode.Space)) {

            for (int i = 0; i < 3; i++) {
                GameObject obj = ObjectPooler.GetPooledObject (bulletId);
                obj.SetActive (true);
                ammos.Add (obj.GetComponent<DefaultAmmo> ());
            }
            time = 0;
        }

    }

    public void RemoveBullet () {
        if (ammos.Count == 0) return;
        ammos[UnityEngine.Random.Range (0, ammos.Count)].gameObject.SetActive (false);
    }
}

public class DefaultAmmo : BaseAmmo {

    [SerializeField] new protected float damage = 5, velocity = 10;

    public override float BulletVelocity => velocity;

    public override void OnCollisionEnter2D (Collision2D other) {
        IDamageable entity = other.collider.GetComponent<IDamageable> ();
        if (entity == null) return;
        if (!entity.Damageable) {
            entity.OnCollisionHit ();
            return;
        }

        entity.OnCollisionHit (damage);

    }
}