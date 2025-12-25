using _Source.Scripts.GameLogic.Ships.ShipEnemy;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
   private Health _health;

   public void Kill()
   {
      FindObjectOfType<EnemyShip>().GetComponent<Health>().TakeDamage(1000);
   }
}
