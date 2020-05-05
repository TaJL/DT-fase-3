using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDamager : MonoBehaviour {
  public int damage;

  void Awake () {
    Events.OnBossDeath += HandleDeath;
  }

  void OnDestroy () {
    Events.OnBossDeath -= HandleDeath;
  }

  void OnTriggerEnter (Collider c) {
    Attackable attackable = c.GetComponentInParent<Attackable>();
    if (attackable && attackable != GetComponentInParent<Npc>().attackable) {
      attackable.GetDamage(damage, transform.position, 0);
    }
  }

  public void HandleDeath (Npc npc) {
    Destroy(gameObject);
  }
}
