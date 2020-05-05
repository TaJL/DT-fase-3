using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GudvoiSounds : MonoBehaviour {
  public GudvoiAudioSource footsteps;
  public GudvoiAudioSource bark;

  public void Step () {
    footsteps.Play();
  }

  public void Bark () {
    bark.Play();
  }
}
