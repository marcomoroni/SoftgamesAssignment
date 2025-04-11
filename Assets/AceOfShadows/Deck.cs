using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SoftgamesAssignment.AceOfShadows
{
    [AddComponentMenu("Softgames Assignment/Ace Of Shadows/Deck"), DisallowMultipleComponent]
    public class Deck : MonoBehaviour
    {
        [SerializeField] private float gap = 0.03f;

        private readonly Stack<Card> cards = new();

        public bool IsEmpty() => cards.Count <= 0;

        /// <summary>
        /// Place cards on this deck immesiately, without any animation.
        /// </summary>
        public void InitializeWithCards(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                this.cards.Push(card);
                card.transform.SetParent(transform, true);
                var targetLocalPosition = new Vector2(0, CardPositionInDeck(i, cards.Count, gap));
                var targetSortingOrder = i;
                card.AnimateToNewPosition(targetLocalPosition, targetSortingOrder, true);
            }
        }

        /// <summary>
        /// Make the card a child of this deck and animate its position from its current position.
        /// </summary>
        public void AddCardToTop(Card card)
        {
            cards.Push(card);
            card.transform.SetParent(transform, true);
            var targetLocalPosition = new Vector2(0, CardPositionInDeck(cards.Count - 1, cards.Count, gap));
            var targetSortingOrder = cards.Count - 1;
            card.AnimateToNewPosition(targetLocalPosition, targetSortingOrder);
        }

        public Card RemoveTopCard()
        {
            var card = cards.Pop();
            card.transform.SetParent(null, true);
            return card;
        }

        /// <summary>
        /// Cards in a deck are positioned in a row. Use this method to calculate the layout of each card.
        /// </summary>
        private static float CardPositionInDeck(int cardIndex, int cardsCount, float gap)
        {
            Assert.IsTrue(cardIndex < cardsCount);

            if (cardsCount == 1) {
                return 0;
            }

            var deckLenght = gap * (cardsCount - 1);
            return Mathf.Lerp(0, deckLenght, (float)cardIndex / (cardsCount - 1));
        }
    }
}
