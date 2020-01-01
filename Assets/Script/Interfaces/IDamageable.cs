using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void OnCollisionHit(float damage);
    void OnCollisionHit();

    bool Damageable { get; }
}
