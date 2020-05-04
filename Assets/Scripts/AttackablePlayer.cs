using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackablePlayer : Attackable {
  public static event System.Action onPlayerDead;

  public float invulnerabilityAfterDamage = 0.5f;
  public float cooldown = 0;
  public PlayerControl control;
  public int maxHp = 10;
  public int hp = 10;
  public SpriteRenderer visual;

  void Update () {
    cooldown -= Time.deltaTime;
    visual.color = cooldown >= 0? Color.gray: Color.white;
  }

  public override void GetDamage (int damage, Vector3 source, float pushBack) {
    if (cooldown <= 0) {
      cooldown = invulnerabilityAfterDamage;
      hp = Mathf.Max(0, hp - damage);
    }

    if (hp <= 0) {
      if (onPlayerDead != null) onPlayerDead();
    }
  }
}
