using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public static class TargetHelper
    {
        /// <summary>
        /// Gets all enemies in range.
        /// </summary>
        /// <param name="sourceObject">Object that tries to target something</param>
        /// <param name="targetRange">Target seek range</param>
        public static List<Collider> GetTargets(GameObject sourceObject, float targetRange)
        {
            var hitColliders = Physics.OverlapSphere(sourceObject.transform.position, targetRange);
            return hitColliders.Where(col => col.CompareTag("Enemy")).ToList();
        }

        /// <summary>
        /// Gets nearest target in range.
        /// </summary>
        /// <param name="sourceObject">Object that tries to target something.</param>
        /// <param name="targetRange">Target seek range</param>
        /// <returns>Returns null if target not found.</returns>
        public static GameObject TryToGetTarget(GameObject sourceObject, float targetRange)
        {
            var hitColliders = Physics.OverlapSphere(sourceObject.transform.position, targetRange);
            var enemies = hitColliders.Where(col => col.CompareTag("Enemy")).ToList();
            if (enemies.Any())
                return enemies.OrderBy(en => Vector3.Distance(sourceObject.transform.position, en.transform.position))
                    .First()
                    .gameObject;

            return null;
        }

        /// <summary>
        /// True if target is in range
        /// </summary>
        public static bool TargetIsInRange(GameObject sourceObject, GameObject target, float range)
        {
            var distance = Vector3.Distance(sourceObject.transform.position, target.transform.position);
            return distance <= range;
        }
    }
}
