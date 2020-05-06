using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameWinner : MonoBehaviour {
  void OnEnable () {
    Events.OnBossDeath += (Npc np) => {
      SceneManager.LoadScene("u won");
    };
  }
}
