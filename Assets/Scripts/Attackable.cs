using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attackable : MonoBehaviour {
  public virtual void GetDamage (int damage, Vector3 source, float pushBack) {}
}
