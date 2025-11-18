using UnityEngine;

public class tRIGER : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      Debug.Log(other.gameObject.name);
   }
}
