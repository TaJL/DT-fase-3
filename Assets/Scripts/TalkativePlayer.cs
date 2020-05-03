using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkativePlayer : AoeDetector<Npc> {
  public override void Undetect (Npc old) {
    old.Stop();
  }

  public override void Detect () {
    target.IndicateActiveForTalk();
  }

  void Update () {
    if (Input.GetButtonDown("Fire1") && target && !PlayerDecisions.isActive) {
      target.Speak();
    }
  }
}
