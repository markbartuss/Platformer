using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoThroughPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlatform;
    private bool _isDropping;

    [Header("Input System")]
    [Tooltip("InputActionReference Player/GoDown (Button)")]
    [SerializeField] private InputActionReference goDownAction;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        if (goDownAction != null)
            goDownAction.action.Enable();
    }

    private void OnDisable()
    {
        if (goDownAction != null)
            goDownAction.action.Disable();
    }

    private void Update()
    {
        if (!_playerOnPlatform || _isDropping || goDownAction == null)
            return;

        if (goDownAction.action.IsPressed())
        {
            StartCoroutine(DisableColliderForAWhile());
        }

    }

    private IEnumerator DisableColliderForAWhile()
    {
        _isDropping = true;
        _collider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        _collider.enabled = true;
        _isDropping = false;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, false);
    }
}
