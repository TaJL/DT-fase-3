using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AgressiveNpc : MonoBehaviour {
  public EnemyPlayerDetector close;
  public EnemyPlayerDetector mid;

  public NavMeshAgent agent;
  public FleeAndShoot ranged;
  public FollowAndAttack melee;

  public int meleeCounter = 0;
  public int rangedCounter = 0;

  public SpriteRenderer visuals;
  public bool onlyRanged = false;

  void Awake () {
    melee.onAttack += () => { meleeCounter++; };
    ranged.onFinished += () => { rangedCounter++; };
    Events.OnBossDeath += HandleDeath;

    if (onlyRanged) {
      ranged.enabled = true;
    } else {
      melee.enabled = Random.Range(0,1f) < 0.5;
      ranged.enabled = !melee.enabled;
    }
  }

  void OnDestroy () {
    Events.OnBossDeath -= HandleDeath;
  }

  void Update () {
    if (onlyRanged) return;

    if (melee.enabled && (meleeCounter > 3 || melee.elapsed > 5)) {
      SwapBehaviours();
    } else if (ranged.enabled && (rangedCounter > 2)) {
      SwapBehaviours();
    }

    if (agent.velocity.x != 0) {
      Vector3 scale = visuals.transform.localScale;
      scale.x = Mathf.Abs(scale.x) * (agent.velocity.x > 0? 1: -1);
      visuals.transform.localScale = scale;
    }
  }

  public void SwapBehaviours () {
    melee.enabled = !melee.enabled;
    ranged.enabled = !ranged.enabled;
    rangedCounter = 0;
    meleeCounter = 0;
  }

  public void HandleDeath (Npc npc) {
    this.enabled = false;
    ranged.enabled = melee.enabled = false;
    agent.ResetPath();
  }
}
