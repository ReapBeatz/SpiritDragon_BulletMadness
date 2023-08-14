using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Main : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
   [SerializeField] private Image _image;
   [SerializeField] private Sprite _default, _pressed;

   private void Start()
   {
        _image.sprite = _default;
   }

   public void OnPointerDown(PointerEventData eventData)
   {
        _image.sprite = _pressed;
   }

   public void OnPointerUp(PointerEventData eventData)
   {
        _image.sprite = _default;
   }
}
