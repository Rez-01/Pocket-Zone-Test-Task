using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        private void Update()
        {
            if (_joystick.Direction != Vector2.zero)
            {
                float endX = transform.position.x + _joystick.Direction.x;
                float endY = transform.position.y + _joystick.Direction.y;
                transform.DOMove(new Vector3(endX, endY, 0f), 1f);
            }
        }
    }
}