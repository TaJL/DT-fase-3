using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clear : MonoBehaviour {
  void Start () {
    PlayerPrefs.DeleteAll();
  }
}
