using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleController : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    private List<Collider> colliders = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Reset() {
      foreach (GameObject flask in GameObject.FindGameObjectsWithTag("Flask")) {
        flask.GetComponent<Draggable>().Reset();
      }
    }

    // Update is called once per frame
    void Update()
    {
        double sum = 0.0;
        foreach (Collider c in colliders) {
          if (!c.gameObject.GetComponent<Draggable>().isDragging()) {
            sum += c.gameObject.GetComponent<Properties>().mass;
          }
        }
        displayText.text = string.Format("{0:0.00}", sum);
    }

    private void OnTriggerEnter (Collider other) {
      if (!colliders.Contains(other)) {
        colliders.Add(other);
        Debug.Log("inside: " + other);
      }
    }

    private void OnTriggerExit (Collider other) {
      colliders.Remove(other);
      Debug.Log("outside: " + other);
    }
}
