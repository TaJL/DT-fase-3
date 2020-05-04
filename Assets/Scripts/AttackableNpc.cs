using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackableNpc : Attackable {
  public int hp;
  public Rigidbody body;
  public bool isDead = false;

  public override void GetDamage (int damage, Vector3 source, float pushBack) {
    hp = Mathf.Max(0, hp - damage);
    body.AddForce((transform.position - source).normalized * pushBack, ForceMode.Impulse);

    if (hp <= 0 && !isDead) {
      Events.OnBossDeath(this.GetComponentInParent<Npc>());
    }
  }
}
