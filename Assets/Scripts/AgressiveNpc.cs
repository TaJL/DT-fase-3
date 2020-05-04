using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgressiveNpc : MonoBehaviour {
  public EnemyPlayerDetector close;
  public EnemyPlayerDetector mid;

  public FleeAndShoot ranged;
  public FollowAndAttack melee;

  public int meleeCounter = 0;
  public int rangedCounter = 0;

  void Awake () {
    melee.onAttack += () => { meleeCounter++; };
    ranged.onFinished += () => { rangedCounter++; };

    melee.enabled = Random.Range(0,1f) < 0.5;
    ranged.enabled = !melee.enabled;
  }

  void Update () {
    if (melee.enabled && (meleeCounter > 3 || melee.elapsed > 5)) {
      SwapBehaviours();
    } else if (ranged.enabled && (rangedCounter > 2)) {
      SwapBehaviours();
    }
  }

  public void SwapBehaviours () {
    melee.enabled = !melee.enabled;
    ranged.enabled = !ranged.enabled;
    rangedCounter = 0;
    meleeCounter = 0;
  }
}
