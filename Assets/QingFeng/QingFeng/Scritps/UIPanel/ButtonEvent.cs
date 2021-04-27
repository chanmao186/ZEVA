using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public GameRoot game;

    public void Awake()
    {
        game = GameObject.Find("UIManager").GetComponent<GameRoot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        game.MouseEnter();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        game.MouseDown();
    }

}
