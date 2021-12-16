using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGrid : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] GridLayoutGroup _grid;
    [SerializeField] ClickableCard _baseCardPrefab;
    [Header("Events")]
    [SerializeField] UnityEventCard _onCardClicked;

    List<ClickableCard> _activeCards = new List<ClickableCard>();

    public void CreateNewCardGrid(List<CardInfo> cards, int numberOfCols)
    {
        DestroyActiveCards();

        _grid.constraintCount = numberOfCols;

        foreach (var card in cards)
        {
            CreateNewActiveCard(card);
        }
    }

    public void PlaySpawnAnimation()
    {
        foreach(var card in _activeCards)
        {
            card.PlaySpawnAnimation();
        }
    }

    public void CardClicked(ClickableCard clickedCard)
    {
        _onCardClicked?.Invoke(clickedCard);
    }

    private void DestroyActiveCards()
    {
        foreach(var card in _activeCards)
        {
            Destroy(card.gameObject);
        }

        _activeCards.Clear();
    }

    private void CreateNewActiveCard(CardInfo cardInfo)
    {
        var newCard = Instantiate(_baseCardPrefab, transform);
        newCard.InitializeCard(this, cardInfo);

        _activeCards.Add(newCard);
    }

}
