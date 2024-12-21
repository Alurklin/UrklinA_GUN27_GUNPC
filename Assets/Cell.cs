using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Netologia.Necro
{
    public class Cell : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField]
        private MeshRenderer _focus;

        [SerializeField]
        private MeshRenderer _select;


        public event Action<Cell> OnPointerClickEvent;

        public void SetSelect(Material material)
            => (_select.enabled, _select.sharedMaterial) = (true, material);

        public void ResetSelect()
            => (_select.enabled) = (false);

        public void OnPointerClick(PointerEventData eventData)
            => OnPointerClickEvent.Invoke(this);

        public void OnPointerEnter(PointerEventData eventData)
        {
            _focus.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _focus.enabled = false;
        }
    }
}
