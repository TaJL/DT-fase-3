using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AoeDetector<T> : MonoBehaviour where T: MonoBehaviour {
  public T target;

  void OnTriggerStay (Collider c) {
    if (!gameObject.activeSelf) return;

    T found = c.GetComponentInParent<T>();
    if (found != target && found) {
      target = found;
      Detect();
    }
  }

  void OnTriggerExit (Collider c) {
    if (!gameObject.activeSelf) return;

    if (c.GetComponentInParent<T>() == target) {
      if (target != null) Undetect(target);
      target = null;
    }
  }

  public virtual void Undetect (T old) {}
  public virtual void Detect () {}
}
