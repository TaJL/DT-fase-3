using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PlayerDodge : MonoBehaviour {
  public TrailRenderer trail;
  public Transform initialMotionTarget;
  public Transform motionTarget;
  public Transform player;
  public PlayerControl control;
  public float distance;
  public float dodgeDuration;
  public float stamina = 10;
  public GameObject dust;
  public Transform visual;

  Coroutine _dodge;

  void FixedUpdate () {
    if (PlayerDecisions.isActive || NpcDialoguePlaceholder.Instance.IsVisible) return;

    if (Input.GetButtonDown("Fire2") && stamina > 0) {
      StopDodging();
      _dodge = StartCoroutine(_Dodge());
    }
  }

  public void StopDodging () {
    StopAllCoroutines();
    control.enabled = true;
    trail.emitting = false;
    _dodge = null;
  }

  IEnumerator _Dodge () {
    trail.emitting = true;
    control.enabled = false;
    motionTarget.position = initialMotionTarget.position;

    if (motionTarget.localPosition == Vector3.zero) {
      motionTarget.localPosition = visual.right;
    }

    float elapsed = 0;
    float deltaDistance = this.distance;
    while (elapsed < dodgeDuration && stamina > 0 && Input.GetButton("Fire2")) {
      stamina -= Time.deltaTime;
      deltaDistance = (distance/dodgeDuration) * Time.deltaTime;
      // prevents from crossing walls
      _SlideTo(motionTarget.localPosition, deltaDistance);
      elapsed += Time.deltaTime;

      Vector3 direction =
        (control.controlPov.right * Input.GetAxisRaw("Horizontal") +
         control.controlPov.forward * Input.GetAxisRaw("Vertical"));
      if (direction.magnitude > 0.1f) {
        motionTarget.transform.localPosition = direction.normalized;
      }

      yield return null;
    }

    StopDodging();
  }

  void _SlideTo (Vector3 direction, float distance) {
    NavMeshHit hit;
    Vector3 target = player.transform.position + direction * distance;
    Vector3 oldPosition = player.transform.position;
    if (NavMesh.Raycast(player.transform.position, target,
                        out hit, NavMesh.AllAreas)) {
      target = hit.position;
      if ((oldPosition - player.transform.position).magnitude < (distance * 0.8f)) {
        GameObject created = Instantiate(dust);
        created.transform.forward = -direction + new Vector3(0,0.7f,0);
        created.transform.position = hit.position + Vector3.up * 0.2f -
          Vector3.forward * 0.5f;
      }
    }

    player.transform.position = target;
  }
}
