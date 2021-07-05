using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class RaycastTarget : MonoBehaviour
    {
        public ClickHandler clickHandler;
        public DragHandler dragHandler;
        public Transform target;
        public float xClamp;

        private int _pointerId = -1;
        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }
    
        private void RegisterEvents()
        {
            clickHandler.onDown += MakeRayCast;
            dragHandler.onBeginDrag += MakeRayCast;
            dragHandler.onDrag += MakeRayCast;
            
            clickHandler.onUp += RemovePointerId;
            dragHandler.onEndDrag += RemovePointerId;
        }

        private void UnregisterEvents()
        {
            clickHandler.onDown -= MakeRayCast;
            dragHandler.onBeginDrag -= MakeRayCast;
            dragHandler.onDrag -= MakeRayCast;
            
            clickHandler.onUp -= RemovePointerId;
            dragHandler.onEndDrag -= RemovePointerId;
        }

        private void RemovePointerId(PointerEventData ped)
        {
            if (_pointerId == ped.pointerId)
            {
                _pointerId = -1;
            }
        }
        
        private void MakeRayCast(PointerEventData ped)
        {
            if (_pointerId == -1)
            {
                _pointerId = ped.pointerId;
            }
            
            RaycastMoveTarget(ped);
        }
        
        private void RaycastMoveTarget(PointerEventData ped)
        {
            if (_pointerId == ped.pointerId)
            {
                var layer = 1 << LayerMask.NameToLayer("Ground");
                var ray = Camera.main.ScreenPointToRay(ped.position);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                if (Physics.Raycast(ray, out var hit, 100, layer))
                {
                    var pos = hit.point;
                    pos.x = Mathf.Clamp(pos.x, -xClamp, xClamp);
                    pos.y = target.position.y;
                    pos.z = target.position.z;
                    target.position = pos;
                }
            }
        }
    }
}