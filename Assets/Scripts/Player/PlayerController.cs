using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerLifes _playerLifes;

    private Camera _camera;
    private Animator _animator;

    public bool isAlive = true;
    public bool isImmortal = false;

    private float _shakeRangeWithMove = 0.05f;
    private float _shakeRangeWithDamage = 0.3f;

    private enum _positionsVariants
    {
        top, middle, bottom
    }

    _positionsVariants PlayerPosition = _positionsVariants.middle;

    private void Start()
    {
        _animator = gameObject.GetComponentInParent<Animator>();
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (isAlive) MovePLayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out BombScript bomb))
        {
            if (isImmortal) return;
            TakeDamage();
        }
    }
    IEnumerator ShakeCamre(float shakeRange, float myDurantion = 0.5f)
    {
        float xPos;
        float yPos;

        float timeLeft = Time.time;
        float duration = myDurantion;

        Vector3 originalPosition = _camera.transform.position;

        while ((timeLeft + duration) > Time.time)
        {
            xPos = Random.Range(-shakeRange, shakeRange);
            yPos = Random.Range(-shakeRange, shakeRange);

            _camera.transform.position = new Vector3(xPos, yPos, _camera.transform.position.z); yield return new WaitForSeconds(0.025f);
        }

        _camera.transform.position = originalPosition;
    }
    private void TakeDamage()
    {
        if (!isAlive) return;

        _playerLifes.playerLifesCount--;
        _playerLifes.UpdateLifesCount();

        isImmortal = true;
        _animator.SetBool("isImmortal", true);

        StartCoroutine(ShakeCamre(_shakeRangeWithDamage));

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
            StartCoroutine(ShakeCamre(_shakeRangeWithMove, 0.15f));
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
            StartCoroutine(ShakeCamre(_shakeRangeWithMove, 0.15f));
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
