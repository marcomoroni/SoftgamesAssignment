using UnityEngine;

namespace SoftgamesAssignment.AceOfShadows
{
    [AddComponentMenu("Softgames Assignment/Phoenix Flame/Fire"), DisallowMultipleComponent]
    public class Fire : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem fireParticleSystem;
        [SerializeField] private ParticleSystem exctinctionSmokeParticleSystem;

        private bool fireIsOn = true;

        public void TurnFireOn()
        {
            fireIsOn = true;
            animator.Play("On");
        }

        public void TurnFireOff()
        {
            fireIsOn = false;
            animator.Play("Off");
        }

        public void ToggleFire()
        {
            if (fireIsOn)
            {
                TurnFireOff();
            }
            else
            {
                TurnFireOn();
            }
        }

        /// <summary>
        /// For animation events.
        /// </summary>
        public void TurnFireParticlesOn()
        {
            fireParticleSystem.Play();
        }

        /// <summary>
        /// For animation events.
        /// </summary>
        public void TurnFireParticlesOff()
        {
            fireParticleSystem.Stop();
        }

        /// <summary>
        /// For animation events.
        /// </summary>
        public void PlayExctinctionSmokeParticles()
        {
            exctinctionSmokeParticleSystem.Clear();
            exctinctionSmokeParticleSystem.Play();
        }
    }
}
