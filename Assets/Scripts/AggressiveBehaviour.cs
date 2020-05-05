using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AggressiveBehaviour : MonoBehaviour {
  [HideInInspector]
  public AttackablePlayer target;
  [HideInInspector]
  public AgressiveNpc manager;
  [HideInInspector]
  public NavMeshAgent agent;

  public Transform parent { get => agent.transform; }

  Coroutine _attackBlink;

  void Awake () {
    target = GameObject.FindWithTag("Player").GetComponentInChildren<AttackablePlayer>();
    manager = GetComponentInParent<AgressiveNpc>();
    agent = GetComponentInParent<NavMeshAgent>();
    agent.updateRotation = false;
    CustomAwake();
  }

  void OnDisable () {
    agent.ResetPath();
    CustomOnDisable();
  }

  public virtual void CustomOnDisable () {}
  public virtual void CustomAwake () {}
}
