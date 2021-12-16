using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCreator
{
    Dictionary<CardSetInfo, List<CardInfo>> _possibleAnswers = new Dictionary<CardSetInfo, List<CardInfo>>();

    public DeckCreator(List<CardSetInfo> possibleSets)
    {
        foreach (var cardSet in possibleSets)
        {
            _possibleAnswers[cardSet] = new List<CardInfo>(cardSet.Cards);
        }
    }

    public void CreateLevelDeck(CardSetInfo currentCardSet, int deckSize, out List<CardInfo> levelDeck, out CardInfo answer)
    {
        if (_possibleAnswers[currentCardSet].Count <= 0)
        {
            Debug.LogWarning("!!! No eligible answers left !!! Resetting the answers pool!");
            _possibleAnswers[currentCardSet] = new List<CardInfo>(currentCardSet.Cards);
        }

        answer = _possibleAnswers[currentCardSet].PopRandomElement();

        int numberOfCardsToCreate = deckSize;

        if (numberOfCardsToCreate > currentCardSet.Cards.Count)
        {
            Debug.LogWarning("!!! Too few cards in the set to create the level !!!");
            numberOfCardsToCreate = Mathf.Min(numberOfCardsToCreate, currentCardSet.Cards.Count);
        }

        levelDeck = new List<CardInfo>(currentCardSet.Cards);

        // Randomize order of cards in the deck
        levelDeck.Shuffle();
        // Remove excessive elements
        levelDeck.RemoveRange(numberOfCardsToCreate, levelDeck.Count - numberOfCardsToCreate);
        // If the answer was removed - put it back in random place
        if (!levelDeck.Contains(answer))
        {
            levelDeck.ReplaceRandomElement(answer);
        }
    }
}
