using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : AoeDetector<Attackable> {
  public int damage;
  public float pushBackStrength = 5;
  public Transform pushBackSource;

  void Update () {
    if (Input.GetButtonDown("Fire1") && target) {
      target.GetDamage(damage, pushBackSource.position, pushBackStrength);
    }
  }
}
