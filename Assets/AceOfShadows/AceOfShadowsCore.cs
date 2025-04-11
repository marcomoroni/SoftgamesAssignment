using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace SoftgamesAssignment.AceOfShadows
{
    [AddComponentMenu("Softgames Assignment/Ace Of Shadows/Core"), DisallowMultipleComponent]
    public class AceOfShadowsCore : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private int cardsCount = 144;
        [SerializeField] private float cardsMoveEverySec = 1;
        [SerializeField] private float firstCardsMovesAfterSec = 2;
        [SerializeField] private Deck deck1;
        [SerializeField] private Deck deck2;

        private float moveNextCardInSec;

        private void Start()
        {
            Assert.IsNotNull(cardPrefab);

            var cards = new List<Card>();
            for (int i = 0; i < cardsCount; i++) {
                var card = InstantiateCard(i);
                cards.Add(card);
            }
            deck1.InitializeWithCards(cards);

            moveNextCardInSec = firstCardsMovesAfterSec;
        }

        private Card InstantiateCard(int index)
        {
            var cardGameObject = GameObject.Instantiate(cardPrefab);
            cardGameObject.name = $"Card{index}";
            var card = cardGameObject.GetComponent<Card>();
            Assert.IsNotNull(card);
            return card;
        }

        private void MoveTopCardBetweenDecks() 
        {
            Assert.IsTrue(!deck1.IsEmpty());
            var card = deck1.RemoveTopCard();
            deck2.AddCardToTop(card);
        }

        private void Update()
        {
            if (!deck1.IsEmpty())
            {
                moveNextCardInSec -= Time.deltaTime;
                if (moveNextCardInSec <= 0)
                {
                    MoveTopCardBetweenDecks();
                    moveNextCardInSec = cardsMoveEverySec;
                }
            }
        }

    }
}
