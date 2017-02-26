using UnityEngine;
using System.Collections;

public class TreeRemover : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Tree" || other.tag == "Rock")
          Destroy(other.gameObject);
    }
}
