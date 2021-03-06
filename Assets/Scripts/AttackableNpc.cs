using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackableNpc : Attackable {
  public static event System.Action<AttackableNpc> onDamageTaken;

  public Animator animator;
  public int maxHp = 10;
  public int hp;
  public Rigidbody body;
  public bool isDead = false;
  public float invulnerabilityAfterDamage = 0.5f;
  public float cooldown = 0;

  void OnEnable () {
    hp = maxHp;
    // if (PlayerPrefs.HasKey("npc" + JsonUtility.ToJson(transform.position))) {
      
    // }
  }

  void Update () {
    cooldown -= Time.deltaTime;
    animator.SetBool("invulnerable", cooldown > 0);
  }

  public override void GetDamage (int damage, Vector3 source, float pushBack) {
    if (cooldown <= 0 && !isDead) {
      cooldown = invulnerabilityAfterDamage;
      hp = Mathf.Max(0, hp - damage);
      body.AddForce((transform.position - source).normalized * pushBack, ForceMode.Impulse);

      if (hp <= 0) {
        Die();
      }

      if (onDamageTaken != null) onDamageTaken(this);
      PlayerPrefs.SetString("npc" + JsonUtility.ToJson(transform.position), "ded");
    }
  }

  public void Die () {
    hp = 0;
    isDead = true;
    Events.OnBossDeath(this.GetComponentInParent<Npc>());
  }
}
