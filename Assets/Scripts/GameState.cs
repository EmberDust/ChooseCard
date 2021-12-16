using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

public class GameState : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] LevelChanger _levelChanger;
    [SerializeField] FadeInComponent _finalScreen;

    [Header("Events")]
    [SerializeField] UnityEvent _onGameStart;
    [SerializeField] UnityEventCard _onWrongClick;
    [SerializeField] UnityEventCard _onCorrectClick;
    [SerializeField] UnityEvent _onObjectiveSuccess;
    [SerializeField] UnityEvent _onGameFinish;

    bool _waitingForAnimation = false;

    private void Start()
    {
        _onGameStart?.Invoke();
    }

    public async void ProcessAnswerAsync(ClickableCard clickedCard)
    {
        if (!_waitingForAnimation)
        {
            if (clickedCard.Identifier == _levelChanger.CurrentLevelAnswer.Identifier)
            {
                _onCorrectClick?.Invoke(clickedCard);

                clickedCard.PlayRightAnswerAnimation();
                _waitingForAnimation = true;
                await clickedCard.CurrentAnimationTask;
                _waitingForAnimation = false;

                _onObjectiveSuccess?.Invoke();
            }
            else
            {
                _onWrongClick?.Invoke(clickedCard);

                clickedCard.PlayWrongAnswerAnimation();
            }
        }
    }

    public async void FinishGame()
    {
        await _finalScreen.TaskFadeIn();
        _onGameFinish?.Invoke();
    }
}
