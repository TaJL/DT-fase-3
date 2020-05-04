using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {
  public Vector3 direction;
  public GameObject dustPrototype;
  public float speed = 10;
  public float rotationSpeed = 360;

  void OnCollisionEnter (Collision c) {
    GameObject dust = Instantiate(dustPrototype);
    dust.transform.position = c.GetContact(0).point;
    dust.transform.forward = c.GetContact(0).normal;
    Debug.Log(c.collider, c.collider);
    Destroy(gameObject);
  }

  void Update () {
    transform.position += direction * speed * Time.deltaTime;
    transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
  }
}
