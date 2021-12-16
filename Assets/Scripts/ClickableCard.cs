using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Threading.Tasks;

public class ClickableCard : MonoBehaviour, IPointerClickHandler
{
    [Header("Card Components")]
    [SerializeField] RectTransform _cardRectTransform;
    [SerializeField] Image _cardImage;

    [Header("Wrong Answer")]
    [SerializeField] float _shakeDuration = 0.75f;
    [SerializeField] float _shakeStrength = 7.5f;
    [Header("Correct Answer")]
    [SerializeField] float _imageBounceDuration = 0.25f;
    [SerializeField] float _imageStartingScale = 0.75f;
    [SerializeField] float _imageFinalScale = 1.2f;
    [Header("Spawn")]
    [SerializeField] float _cardBounceDuration = 0.6f;
    [SerializeField] float _cardStartingScale = 0.2f;
    [SerializeField] float _spawnMaxDelay = 0.075f;

    public Task CurrentAnimationTask => _currentAnimation.AsyncWaitForCompletion();

    public string Identifier => _identifier;

    string _identifier;
    CardGrid _cellManager;

    Tweener _currentAnimation;

    Vector3 _cardBasePosition;
    Vector3 _cardBaseScale;
    Vector3 _baseScale;

    public void InitializeCard(CardGrid cellManager, CardInfo cardInfo)
    {
        _cellManager = cellManager;

        _cardBasePosition = _cardRectTransform.localPosition;
        _cardBaseScale = _cardRectTransform.localScale;
        _baseScale = transform.localScale;

        // Get info out of passed scriptable object
        _cardImage.sprite = cardInfo.CardImage;
        _identifier = cardInfo.Identifier;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _cellManager.CardClicked(this);
    }

    public void PlaySpawnAnimation()
    {
        ResetAnimation();

        transform.localScale = new Vector3(_cardStartingScale, _cardStartingScale, _cardStartingScale);

        _currentAnimation = transform.
            DOScale(1.0f, _cardBounceDuration).
            SetDelay(Random.Range(0.01f, _spawnMaxDelay)).
            SetEase(Ease.InOutElastic);
    }

    public void PlayWrongAnswerAnimation()
    {
        ResetAnimation();

        _currentAnimation = _cardRectTransform.
            DOShakePosition(_shakeDuration, _shakeStrength).
            SetEase(Ease.InBounce);
    }

    public void PlayRightAnswerAnimation()
    {
        ResetAnimation();

        _cardRectTransform.localScale = new Vector3(_imageStartingScale, _imageStartingScale, _imageStartingScale);

        _currentAnimation = _cardRectTransform.
            DOScale(_imageFinalScale, _imageBounceDuration).
            SetEase(Ease.OutElastic);
    }

    private void ResetAnimation()
    {
        if (_currentAnimation.IsActive())
        {
            // Revert animation effect
            _cardRectTransform.localPosition = _cardBasePosition;
            _cardRectTransform.localScale = _cardBaseScale;
            transform.localScale = _baseScale;

            _currentAnimation.Kill();
        }
    }

    private void OnDestroy()
    {
        _currentAnimation.Kill();
    }
}
