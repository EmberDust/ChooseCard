using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]
public class CardInfo : ScriptableObject
{
    [SerializeField] Sprite _cardImage;
    [SerializeField] string _identifier;

    public string Identifier => _identifier;
    public Sprite CardImage  => _cardImage;
}
