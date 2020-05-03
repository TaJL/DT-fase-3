using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgrammedDeath : MonoBehaviour {
  public ParticleSystem particles;

  IEnumerator Start () {
    yield return null;
    yield return new WaitUntil(() => !particles.IsAlive());
    Destroy(gameObject);
  }
}
