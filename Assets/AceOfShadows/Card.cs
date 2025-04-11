using System;
using NUnit.Framework;
using UnityEngine;

namespace SoftgamesAssignment.AceOfShadows
{
    [AddComponentMenu("Softgames Assignment/Ace Of Shadows/Card"), DisallowMultipleComponent]
    public class Card : MonoBehaviour
    {
        private class MovementAnimation
        {
            private readonly float cardMovementDurationSec = 2;
            private readonly AnimationCurve cardMovementEasing;
            private readonly Func<Vector2> getLocalPosition;
            private readonly Action<Vector2> setLocalPosition;
            private readonly Action<int> setSortingOrder;

            private Vector2 startLocalPosition;
            private Vector2 targetLocalPosition;
            private float animationElapsedTime;
            private bool sortingOrderWasChangedDuringAnimation;
            private int targetSortingOrder;

            public MovementAnimation(
                float cardMovementDurationSec, 
                AnimationCurve cardMovementEasing,
                Func<Vector2> getLocalPosition,
                Action<Vector2> setLocalPosition,
                Action<int> setSortingOrder)
            {
                this.cardMovementDurationSec = cardMovementDurationSec;
                this.cardMovementEasing = cardMovementEasing;
                this.getLocalPosition = getLocalPosition;
                this.setLocalPosition = setLocalPosition;
                this.setSortingOrder = setSortingOrder;
            }

            public void StartAnimation(Vector2 targetLocalPosition, int targetSortingOrder)
            {
                startLocalPosition = getLocalPosition();
                this.targetLocalPosition = targetLocalPosition;
                animationElapsedTime = 0;
                sortingOrderWasChangedDuringAnimation = false;
                this.targetSortingOrder = targetSortingOrder;
            }

            public void SetImmediate(Vector2 targetLocalPosition, int targetSortingOrder)
            {
                // End animation if any.
                animationElapsedTime = cardMovementDurationSec;

                setSortingOrder(targetSortingOrder);
                setLocalPosition(targetLocalPosition);
            }

            /// <summary>
            /// Call this evety frame.
            /// </summary>
            public void Tick()
            {
                var IsAnimating = animationElapsedTime < cardMovementDurationSec;
                if (IsAnimating) {
                    animationElapsedTime += Time.deltaTime;

                    if (!sortingOrderWasChangedDuringAnimation && ShouldUpdateSortingOrder()) {
                        setSortingOrder(targetSortingOrder);
                    }

                    var animationShouldEnd = animationElapsedTime >= cardMovementDurationSec;
                    if (animationShouldEnd) {
                        setLocalPosition(targetLocalPosition);
                    }
                    else {
                        var i = animationElapsedTime / cardMovementDurationSec;
                        var easedI = cardMovementEasing.Evaluate(i);
                        var pos = Vector2.Lerp(startLocalPosition, targetLocalPosition, easedI);
                        setLocalPosition(pos);
                    }
                }
            }

            /// <summary>
            /// At some point during the animation, the card needs to update the sorting order. Use this method
            /// to check when this should happen.
            /// </summary>
            private bool ShouldUpdateSortingOrder()
            {
                // Halfway through the animation.
                return animationElapsedTime >= cardMovementDurationSec / 2;
            }
        }

        [SerializeField] private AnimationCurve cardMovementEasing;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float cardMovementDurationSec = 2;
        
        private MovementAnimation movementAnimation;

        private void OnEnable()
        {
            Assert.IsNotNull(cardMovementEasing);
            Assert.IsNotNull(spriteRenderer);

            movementAnimation = new MovementAnimation(
                cardMovementDurationSec,
                cardMovementEasing,
                getLocalPosition: () => transform.localPosition,
                setLocalPosition: (lp) => transform.localPosition = lp,
                setSortingOrder: (l) => spriteRenderer.sortingOrder = l);
        }

        /// <summary>
        /// Call this method once the deck parent has been changed while keep world position.
        /// </summary>
        public void AnimateToNewPosition(Vector2 targetLocalPosition, int targetSortingOrder, bool immediate = false)
        {
            if (immediate)
            {
                movementAnimation.SetImmediate(targetLocalPosition, targetSortingOrder);
            }
            else
            {
                movementAnimation.StartAnimation(targetLocalPosition, targetSortingOrder);
            }
        }

        private void Update()
        {
            movementAnimation.Tick();
        }
    }
}