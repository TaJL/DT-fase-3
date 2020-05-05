using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class DoorSounds : MonoBehaviour {
  public AudioSource speaker;

  void Reset () {
    speaker = GetComponentInChildren<AudioSource>();
  }

  void OnEnable () {
    GetComponent<Door>().onDoorOpen += () => { speaker.Play(); };
  }
}
