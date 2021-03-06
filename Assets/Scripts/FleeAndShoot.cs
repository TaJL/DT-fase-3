using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FleeAndShoot : AggressiveBehaviour {
  public event System.Action onFinished;
  public Animator animator;
  public List<ProjectileSource> sources;
  public Vector2 projectileBurstAmount = new Vector2(4, 6);
  public float timeBetweenProjectiles = 0.25f;
  public float initialRest = 2;
  public float fallSpeed = 0.1f;
  ProjectileSource _chosenSource;

  void OnEnable () {
    agent.speed = target.control.speed * 1.5f;
    agent.acceleration = agent.speed * 10;
    StopAllCoroutines();
    StartCoroutine(_ArriveAndShoot());
  }

  void OnDisable () {
    animator.SetBool("is attacking", false);
  }

  public override void CustomOnDisable () {
    agent.ResetPath();
    StopAllCoroutines();
  }

  IEnumerator _ArriveAndShoot () {
    while (true) {
      agent.SetDestination(target.transform.position);
      yield return new WaitForSeconds(0.1f);
      agent.ResetPath();
      ChangeSource();
      yield return StartCoroutine(_WaitForArrival());
      yield return new WaitForSeconds(0.4f);
      animator.SetBool("is attacking", true);

      int shots = (int) Mathf.Round(Random.Range(projectileBurstAmount.x,
                                                 projectileBurstAmount.y));

      Utility.MakeScaleFaceTarget(manager.visuals.transform.parent,
                                  Player.Instance.transform);
        
      for (int i=0; i<shots; i++) {
        Projectile shot = Instantiate(_chosenSource.projectile);
        shot.transform.position = transform.position + Vector3.up * 0.2f;
        shot.direction = (target.transform.position - parent.position).normalized;
        shot.caster = GetComponentInParent<Npc>().attackable;
        yield return new WaitForSeconds(timeBetweenProjectiles);
      }
      animator.SetBool("is attacking", false);
      yield return new WaitForSeconds(initialRest);
      initialRest -= fallSpeed;

      if (onFinished != null) onFinished();
    }
  }

  IEnumerator _WaitForArrival () {
    while (!agent.hasPath) yield return null;
    while (agent.remainingDistance > agent.stoppingDistance &&
           !(!agent.hasPath && agent.velocity.sqrMagnitude == 0)) yield return null;
  }

  void ChangeSource () {
    _chosenSource = sources[Random.Range(0, sources.Count)];
    agent.SetDestination(_chosenSource.destination.position);
  }
}
