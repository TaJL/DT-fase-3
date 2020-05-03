using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorOpener : MonoBehaviour {
  void Update () {
    if (Input.GetButtonDown("Fire1")) {
      Events.OnBark(transform.position);
    }
  }
}
