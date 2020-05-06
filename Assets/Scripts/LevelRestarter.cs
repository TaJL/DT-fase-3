using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelRestarter : MonoBehaviour {
  void OnEnable () {
    AttackablePlayer.onPlayerDead += RestartLevel;
  }

  void OnDisable () {
    AttackablePlayer.onPlayerDead -= RestartLevel;
  }

  public void RestartLevel () {
    StartCoroutine(_EventuallyReload());
  }

  IEnumerator _EventuallyReload () {
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene(gameObject.scene.name);
  }
}
