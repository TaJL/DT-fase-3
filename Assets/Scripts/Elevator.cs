using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Elevator : MonoBehaviour {
  public bool entered = false;
  public Animator animator;
  public string nextLevelSceneName;

  void OnTriggerEnter (Collider c) {
    if (entered) return;
    PlayerControl control = c.GetComponentInParent<PlayerControl>();
    if (control) {
      control.GetComponent<NavMeshAgent>().enabled = false;
      animator.SetTrigger("lift");
      control.transform.parent = transform.GetChild(0);
    }
  }

  public void NextLevel () {
    SceneManager.LoadScene(nextLevelSceneName);
  }
}
