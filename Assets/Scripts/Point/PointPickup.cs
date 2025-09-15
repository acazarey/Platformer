using UnityEngine;

namespace Point
{
   public class PointPickup : MonoBehaviour
   {
      [SerializeField] private int point_amount;

      private void OnTriggerEnter2D(Collider2D other)
      {
         if (other.CompareTag("Player"))
         {
            Debug.Log("Coin collected by Player!");
            PointManager.Instance.AddPoint(point_amount);
            Destroy(gameObject);
         }
      }


   }
}
