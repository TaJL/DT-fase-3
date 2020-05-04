using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackablePlayer : Attackable {
  public PlayerControl control;

  public override void GetDamage (int damage, Vector3 source, float pushBack) {
  }
}
