using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CassualMusic : MonoBehaviour {
  public AudioSource music;
  public float timeToStart = 5;
  public float timeToStop = 0.3f;
  public float initial;

  void OnEnable () {
    initial = music.volume;

    Npc.onFightTriggered += (Npc npc) => {
      StartCoroutine(_SmoothStop());
    };

    BossMusic.onMusicOver += () => {
      StartCoroutine(_SmoothStart());
    };
  }

  IEnumerator _SmoothStop () {
    float elapsed = 0;

    while (elapsed < timeToStop) {
      elapsed += Time.deltaTime;
      music.volume = Mathf.Lerp(initial, 0, elapsed / timeToStop);
      yield return null;
    }

    music.Stop();
  }

  IEnumerator _SmoothStart () {
    float elapsed = 0;
    music.Play();

    while (elapsed < timeToStart) {
      elapsed += Time.deltaTime;
      music.volume = Mathf.Lerp(0, initial, elapsed / timeToStart);
      yield return null;
    }

  }
}
