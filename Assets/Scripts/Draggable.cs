using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
  private bool dragging = false;
  private bool inShelves = false;
  private float distance;
  private float dragZ = -1.0f;
  private Vector3 home;

  public GameObject shelves;

  void Start() {
    home = transform.position;
  }

  public void Reset() {
    transform.position = home;
    transform.rotation = Quaternion.identity;
  }

  void OnMouseDown()
  {
    Vector3 newPos = transform.position;
    newPos.z = dragZ;
    transform.position = newPos;

      distance = Vector3.Distance(transform.position, Camera.main.transform.position);
      dragging = true;
  }

  void OnTriggerEnter(Collider c) {
    Debug.Log("collide: " + c.gameObject);
    if (c.gameObject.name == "Shelves") {
      inShelves = true;
    }
  }

  void OnTriggerExit(Collider c) {
    if (c.gameObject.name == "Shelves") {
      inShelves = false;
    }
  }

  void OnMouseUp()
  {
      dragging = false;
      if (inShelves) {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = home;
      }
  }

  public bool isDragging() {
    return dragging;
  }

  void Update()
  {
      if (dragging)
      {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          Vector3 rayPoint = ray.GetPoint(distance);
          Vector3 newPos = new Vector3(rayPoint.x, rayPoint.y, dragZ);
          transform.position = newPos;
          transform.rotation = Quaternion.identity;
      }
  }
}
