using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowingCamera : SmoothFollow {
  public Transform controlPov;

  void OnEnable () {
    this.target =
      GameObject.FindWithTag("Player").GetComponent<PlayerControl>().cameraTarget;
  }
}
