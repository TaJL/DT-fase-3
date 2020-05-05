using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class NpcAnimations : MonoBehaviour {
  public Animator animator;
  public NavMeshAgent agent;

  void Reset () {
    animator = GetComponentInChildren<Animator>();
    agent = GetComponentInChildren<NavMeshAgent>();
  }

  void Update () {
    if (animator.gameObject.activeInHierarchy) {
      animator.SetFloat("speed", agent.velocity.magnitude);
    }
  }
}
