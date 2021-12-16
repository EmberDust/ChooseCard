using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Set", menuName = "Card Set", order = 0)]
public class CardSetInfo : ScriptableObject
{
    [SerializeField] List<CardInfo> _cards;

    public List<CardInfo> Cards => _cards;
}
