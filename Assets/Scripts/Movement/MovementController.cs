using UnityEngine;

public class MovementController : Singleton<MovementController>
{
    [SerializeField] private float _limitX = 2;
    [SerializeField] private float _xSpeed = 25;
    [SerializeField] private float _forwardSpeed = 2;
    private float _lastTouchedX;

    public float ForwardSpeed { get => _forwardSpeed; set => _forwardSpeed = value; }

    void Update()
    {
        // We check game state, if state in game we can move 
        if (StateManager.Instance.State == State.InGame)
        {
            float _touchXDelta = 0;
            float _newX = 0;

            #region Unity Editör Movement
#if UNITY_EDITOR
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _lastTouchedX = Input.GetTouch(0).position.x;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _touchXDelta = 5 * (Input.GetTouch(0).position.x - _lastTouchedX) / Screen.width;
                    _lastTouchedX = Input.GetTouch(0).position.x;
                }
            }
#endif
            #endregion

            #region Android Movement
            else if (Input.GetMouseButton(0))
            {
                _touchXDelta = Input.GetAxis("Mouse X");
            }
            #endregion

            _newX = transform.position.x + _xSpeed * _touchXDelta * Time.deltaTime;
            // we limited horzontal movement
            _newX = Mathf.Clamp(_newX, -_limitX, _limitX);

            Vector3 newPosition = new Vector3(_newX, transform.position.y, transform.position.z + _forwardSpeed * Time.deltaTime);
            transform.position = newPosition;

            GameManager.Instance.MoveToHorizontalWithDelay(transform);

        }
    }

}
