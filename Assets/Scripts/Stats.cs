using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour {
  public Image bossHelath;
  public Image playerHealth;

  void OnEnable () {
    AttackableNpc.onDamageTaken += HandleBossDamage;
    AttackablePlayer.onDamageTaken += HandlePlayerDamage;
  }

  public void HandleBossDamage(AttackableNpc npc) {
    bossHelath.fillAmount = npc.hp / (float) npc.maxHp;
  }

  public void HandlePlayerDamage (AttackablePlayer player) {
    playerHealth.fillAmount = player.hp / (float) player.maxHp;
  }
}
