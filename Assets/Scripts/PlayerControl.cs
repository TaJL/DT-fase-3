using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
  public float speed = 8;
  public Transform controlPov;
  public Transform cameraTarget;
  public float maxCameraDistance = 2;
  public Transform visuals;

  public Vector3 direction;

  void Awake () {
    controlPov = Camera.main.GetComponentInParent<FollowingCamera>().controlPov;
  }

  void FixedUpdate () {
    direction = (controlPov.right * Input.GetAxisRaw("Horizontal") +
                 controlPov.forward * Input.GetAxisRaw("Vertical"));

    transform.position += direction * speed * Time.deltaTime;

    if (direction != Vector3.zero) {
      cameraTarget.localPosition = direction.normalized * maxCameraDistance;
      if (direction.x != 0) {
        visuals.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1) *
          visuals.transform.localScale.z;
      }
    }
  }
}
