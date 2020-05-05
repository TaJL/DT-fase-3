using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GudvoiAnimations : MonoBehaviour {
  public AttackablePlayer attackable;
  public PlayerDodge dodger;
  public PlayerControl control;
  public PlayerAttack attacker;
  public Animator animator;

  void Reset () {
    attackable = GetComponentInChildren<AttackablePlayer>();
    dodger = GetComponentInChildren<PlayerDodge>();
    control = GetComponentInChildren<PlayerControl>();
    attacker = GetComponentInChildren<PlayerAttack>();
  }

  void OnEnable () {
    attacker.onAttack += HandleAttack;
    AttackablePlayer.onPlayerDead += HandleDeath;
  }

  void OnDisable () {
    attacker.onAttack -= HandleAttack;
    AttackablePlayer.onPlayerDead -= HandleDeath;
  }

  void Update () {
    animator.SetFloat("speed", control.direction.magnitude);
    animator.SetBool("is dashing", dodger.IsDodgeing);
    animator.SetBool("after hit", attackable.IsInvulnerable);
  }

  public void HandleAttack () {
    animator.SetTrigger("attack");
  }

  public void HandleDeath () {
    animator.SetBool("ded", true);
  }
}
