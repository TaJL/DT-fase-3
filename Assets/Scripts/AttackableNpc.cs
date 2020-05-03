using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackableNpc : Attackable {
  public int hp;
  public Rigidbody body;

  public override void GetDamage (int damage, Vector3 source, float pushBack) {
    hp -= damage;
    body.AddForce((transform.position - source).normalized * pushBack, ForceMode.Impulse);
  }
}
