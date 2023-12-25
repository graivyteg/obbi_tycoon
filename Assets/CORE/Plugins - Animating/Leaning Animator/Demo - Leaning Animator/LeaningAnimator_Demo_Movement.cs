using FIMSpace.Basics;
using UnityEngine;

namespace FIMSpace
{
    public class LeaningAnimator_Demo_Movement : FIMSpace.Basics.FBasics_RigidbodyMover
    {
        public bool AutoAnimate = true;
        public float SprintMul = 2f;
        private FAnimator animating;
        public LeaningAnimator ManualInformLeaning;

        protected override void Start()
        {
            animating = new FAnimator(GetComponent<Animator>());
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            bool turbo = true;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                turbo = false;
                moveSpeed = MovementSpeed;
            }
            else
            {
                moveSpeed = MovementSpeed * SprintMul;
            }

            if (AutoAnimate)
            {
                if (smoothedAcceleration.sqrMagnitude > 0.05f)
                {
                    animating.CrossFadeInFixedTime("Movement", 0.1f);
                }
                else
                {
                    animating.CrossFadeInFixedTime("Idle");
                }

                animating.SetFloat("Speed", smoothedAcceleration.magnitude * (turbo ? 2f : 1f), 5f);
            }

            if (ManualInformLeaning)
            {
                if (smoothedAcceleration.magnitude > 0.1f)
                    ManualInformLeaning.SetIsAccelerating = true;
                else
                    ManualInformLeaning.SetIsAccelerating = false;
            }
        }
    }
}