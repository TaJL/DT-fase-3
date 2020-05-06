using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour {
  public Image bossHelath;
  public Image playerHealth;
  public Image playerStamina;
  public PlayerDodge dodge;
  public Animator animator;

  void OnEnable () {
    AttackableNpc.onDamageTaken += HandleBossDamage;
    AttackablePlayer.onDamageTaken += HandlePlayerDamage;
    Npc.onFightTriggered += HandleFightTriggered;
    dodge = Player.Instance.gameObject.GetComponentInChildren<PlayerDodge>();
  }

  void OnDisable () {
    AttackableNpc.onDamageTaken -= HandleBossDamage;
    AttackablePlayer.onDamageTaken -= HandlePlayerDamage;
    Npc.onFightTriggered -= HandleFightTriggered;
  }

  void Update () {
    playerStamina.fillAmount = dodge.stamina / dodge.maxStamina;
  }

  public void HandleBossDamage(AttackableNpc npc) {
    bossHelath.fillAmount = npc.hp / (float) npc.maxHp;
  }

  public void HandlePlayerDamage (AttackablePlayer player) {
    playerHealth.fillAmount = player.hp / (float) player.maxHp;
  }

  public void HandleFightTriggered (Npc npc) {
    animator.SetBool("is visible", true);
  }
}
