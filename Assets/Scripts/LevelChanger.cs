using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelChanger : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] CardGrid _cardGrid;
    [SerializeField] TextMeshProUGUI _headerText;

    [Header("Levels content")]
    [SerializeField] List<CardSetInfo> _possibleSets;
    [SerializeField] List<LevelInfo> _levels;

    [Header("Events")]
    [SerializeField] UnityEvent _onFirstLevelActivation;
    [SerializeField] UnityEvent _onNoLevelsLeft;

    public CardInfo CurrentLevelAnswer => _currentLevelAnswer;

    DeckCreator _deckCreator;

    CardInfo _currentLevelAnswer;
    int _currentLevelIndex = 0;

    private void Awake()
    {
        _deckCreator = new DeckCreator(_possibleSets);
    }

    public void ActivateLevel(int levelIndex)
    {
        if (levelIndex < _levels.Count)
        {
            int numberOfCards = _levels[levelIndex].NumberOfCols * _levels[levelIndex].NumberOfRows;

            _deckCreator.CreateLevelDeck(_possibleSets.GetRandomElement(), numberOfCards, out List<CardInfo> levelDeck, out _currentLevelAnswer);

            _cardGrid.CreateNewCardGrid(levelDeck, _levels[levelIndex].NumberOfCols);

            string levelObjective = "Click <color=green>" + _currentLevelAnswer.Identifier;
            _headerText.SetText(levelObjective);
        }
        else
        {
            Debug.LogWarning("Level index is out of range!");
        }
    }

    public void ActivateFirstLevel()
    {
        ActivateLevel(0);
        _onFirstLevelActivation?.Invoke();
    }

    public void ActivateNextLevel()
    {
        if (_currentLevelIndex < _levels.Count - 1)
        {
            _currentLevelIndex++;
            ActivateLevel(_currentLevelIndex);
        }
        else
        {
            _onNoLevelsLeft?.Invoke();
        }
    }

    public void StartOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
