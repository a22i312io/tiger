using UnityEngine;

namespace Car.Move
{
    public class Drift
    {
        

        public void OnDrift(Steering steering)
        {
            steering.TurnSpeed = 100f;
        }

        public void OffDrift(Steering steering)
        {
            steering.TurnSpeed = 50f;
        }
    }
}
