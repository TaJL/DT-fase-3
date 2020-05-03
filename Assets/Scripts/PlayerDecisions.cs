using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDecisions : NonPersistantSingleton<PlayerDecisions> {
  public static event System.Action<Decision, Npc> onDecisionMade;
  public static bool isActive = false;
  public Animator animator;

  public Animator positive;
  public Animator negative;

  void Awake () {
    isActive = false;
  }

  void Update () {
    if (!isActive) return;

    if (Input.GetButtonDown("Fire1")) {
      StartCoroutine(_Decide(Decision.Positive));
    }

    if (Input.GetButtonDown("Fire2")) {
      StartCoroutine(_Decide(Decision.Negative));
    }
  }

  public void Activate () {
    animator.SetBool("visible", true);
    isActive = true;
  }

  public void Hide () {
    animator.SetBool("visible", false);
    isActive = false;
  }

  IEnumerator _Decide (Decision decision) {
    (decision == Decision.Positive? positive: negative).SetTrigger("blink");

    yield return new WaitForSeconds(0.2f);

    Hide();
    if (onDecisionMade != null)
      onDecisionMade(decision, Player.Instance.dialogues.target);
  }
}
