using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothFollow : MonoBehaviour {
  public float smoothTime = 1;
  public Transform target;

  Vector3 _velocity;

  void FixedUpdate () {
    transform.position =
      Vector3.SmoothDamp(transform.position, target.position,
                         ref _velocity, smoothTime);
  }
}
