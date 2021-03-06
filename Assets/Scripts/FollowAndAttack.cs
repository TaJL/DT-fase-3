using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowAndAttack : AggressiveBehaviour {
  public event System.Action onAttack;

  public Animator animator;
  public float elapsed = 0;
  [HideInInspector]
  public float speed;
  [HideInInspector]
  public float dashAttackSpeed;
  public SpriteRenderer visual;

  public float chargingTime = 0.6f;
  public float minimumChargingTime = 0.25f;

  public float recoveryTime = 1.5f;
  public float minimumRecoveryTime = 0.5f;

  public float difficultyStep = 0.1f;
  public EnemyDamager damager;

  Coroutine _attack;

  public override void CustomAwake () {
    speed = target.control.speed * 1.8f;
    dashAttackSpeed = speed * 2;
  }

  void OnEnable () {
    agent.speed = speed;
    elapsed = 0;
  }

  void OnDisable () {
    animator.SetBool("charging melee", false);
    animator.SetBool("melee attack", false);
    animator.SetBool("recovering", false);
  }

  public override void CustomOnDisable () {
    StopAttack();
  }

  void Update () {
    elapsed += Time.deltaTime;
    if (!manager.close.target) {
      if (_attack == null) {
        animator.SetBool("charging melee", false);
        animator.SetBool("melee attack", false);
        animator.SetBool("recovering", false);
        agent.SetDestination(target.transform.position);
        // parent.position +=
        // (target.transform.position - parent.position).normalized * speed * Time.deltaTime;
      }
    } else if (_attack == null) {
      agent.ResetPath();
      _attack = StartCoroutine(_Attack());
    }
  }

  IEnumerator _Attack () {
    Vector3 attackTarget = target.transform.position;
    visual.color = Color.yellow;
    Utility.MakeScaleFaceTarget(visual.transform.parent, Player.Instance.transform);

    animator.SetBool("charging melee", true);
    yield return new WaitForSeconds(chargingTime);
    animator.SetBool("charging melee", false);
    animator.SetBool("melee attack", true);
    chargingTime = Mathf.Max(minimumChargingTime, chargingTime - difficultyStep);

    damager.gameObject.SetActive(true);
    visual.color = Color.red;
    float requiredTime = Vector3.Distance(parent.position, attackTarget) / dashAttackSpeed;
    float elapsed = 0;
    while (elapsed < requiredTime) {
      parent.position =
        Vector3.MoveTowards(parent.position, attackTarget,
                            dashAttackSpeed * Time.deltaTime);
      elapsed += Time.deltaTime;
      yield return null;
    }

    damager.gameObject.SetActive(false);
    visual.color = Color.black;
    animator.SetBool("melee attack", false);
    animator.SetBool("recovering", true);
    yield return new WaitForSeconds(recoveryTime);
    animator.SetBool("recovering", false);
    recoveryTime = Mathf.Max(minimumRecoveryTime, recoveryTime-difficultyStep);
    _attack = null;

    if (onAttack != null) onAttack();
  }

  public void StopAttack () {
    StopAllCoroutines();
    _attack = null;
    visual.color = Color.green;
    animator.SetBool("recovering", false);
  }
}
