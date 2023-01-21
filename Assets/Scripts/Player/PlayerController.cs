using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerLifes _playerLifes;

    public bool isAlive = true;
    public bool isImmortal = false;

    private Animator _animator;
    private enum _positionsVariants
    {
        top, middle, bottom
    }

    _positionsVariants PlayerPosition = _positionsVariants.middle;

    private void Start()
    {
        _animator = gameObject.GetComponentInParent<Animator>();
    }

    private void Update()
    {
        MovePLayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out BombScript bomb))
        {
            if (isImmortal) return;
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (!isAlive) return;

        _playerLifes.playerLifesCount--;
        _playerLifes.UpdateLifesCount();

        isImmortal = true;
        _animator.SetBool("isImmortal", true);
        
        StartCoroutine(SetImmortalTime());

        if (_playerLifes.playerLifesCount == 0)
        {
            isAlive = false;
            _animator.SetBool("isDead", true);
            StartCoroutine(RestartGame());
        }
    }

    IEnumerator SetImmortalTime()
    {
        yield return new WaitForSeconds(2f);

        isImmortal = false;
        _animator.SetBool("isImmortal", false);
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MovePLayer()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (PlayerPosition == _positionsVariants.middle)
            {
                transform.position = new Vector2(transform.position.x, 3f);
                PlayerPosition = _positionsVariants.top;
            }
            else if (PlayerPosition == _positionsVariants.bottom)
            {
                transform.position = new Vector2(transform.position.x, 0);
                PlayerPosition = _positionsVariants.middle;
            }

            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PlayerPosition == _positionsVariants.middle)
            {
                transform.position = new Vector2(transform.position.x, -3f);
                PlayerPosition = _positionsVariants.bottom;

            }
            else if (PlayerPosition == _positionsVariants.top)
            {
                transform.position = new Vector2(transform.position.x, 0);
                PlayerPosition = _positionsVariants.middle;
            }

            return;
        }
    }

    private void MovePlayerWithTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                
                if (touch.deltaPosition.y > 10f)
                {
                    if (PlayerPosition == _positionsVariants.middle)
                    {
                        transform.position = new Vector2(transform.position.x, -3f);
                        PlayerPosition = _positionsVariants.bottom;
                        
                    }
                    else if (PlayerPosition == _positionsVariants.top)
                    {
                        transform.position = new Vector2(transform.position.x, 0);
                        PlayerPosition = _positionsVariants.middle;
                    }
                }
                else if (touch.deltaPosition.y < -10f)
                {
                    if (PlayerPosition == _positionsVariants.middle)
                    {
                        transform.position = new Vector2(transform.position.x, 3f);
                        PlayerPosition = _positionsVariants.top;

                    }
                    else if (PlayerPosition == _positionsVariants.bottom)
                    {
                        transform.position = new Vector2(transform.position.x, 0);
                        PlayerPosition = _positionsVariants.middle;
                    }
                }
            }
        }
    }
}
