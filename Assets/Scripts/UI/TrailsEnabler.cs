using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FroguesFramework
{
    public class TrailsEnabler : MonoBehaviour
    {
        [SerializeField] private List<Trail> trails;

        public void EnableTrail(Vector2 direction)
        {
            trails.FirstOrDefault(trail => trail.TrailDirection.CloseEnoughTo(direction))?.Enable(true);
        }

        public void DisableTrails()
        {
            trails.ForEach(trail => trail.Enable(false));
        }
    }
}