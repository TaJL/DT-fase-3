using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDamager : MonoBehaviour {
  public int damage;

  void OnTriggerEnter (Collider c) {
    Attackable attackable = c.GetComponentInParent<Attackable>();
    if (attackable && attackable != GetComponentInParent<Attackable>()) {
      attackable.GetDamage(damage, transform.position, 0);
    }
  }
}
