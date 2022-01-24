using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace UI
{
    public class UIScaleAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private void OnEnable()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.25f, 0.3f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1, 0.3f);
        }
    }

}
