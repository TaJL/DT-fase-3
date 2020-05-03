using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkativePlayer : NonPersistantSingleton<TalkativePlayer> {
  public Npc target;

  void OnTriggerStay (Collider c) {
    Npc found = c.GetComponentInParent<Npc>();
    if (found) {
      target = found;
      target.IndicateActiveForTalk();
    }
  }

  void OnTriggerExit (Collider c) {
    if (c.GetComponentInParent<Npc>() == target) {
      if (target != null) target.Stop();
      target = null;
    }
  }

  void Update () {
    if (Input.GetButtonDown("Fire1") && target && !PlayerDecisions.isActive) {
      target.Speak();
    }
  }
}
