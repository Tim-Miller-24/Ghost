using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerLifes _playerLifes;

    private Camera _camera;
    private Animator _animator;

    private float _shakeRangeWithMove = 0.05f;
    private float _shakeRangeWithDamage = 0.3f;

    public bool isAlive { get; private set; }
    public bool isImmortal { get; private set; }

    private WaitForSeconds _immortalTime;

    private enum PositionsVariants
    {
        top, middle, bottom
    }

    private PositionsVariants PlayerPosition = PositionsVariants.middle;

    private void Start()
    {
        isAlive = true;
        isImmortal = false;

        _immortalTime = new WaitForSeconds(2f);

        _animator = gameObject.GetComponentInParent<Animator>();
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (isAlive) MovePLayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag is "Bomb")
        {
            if (isImmortal) return;
            TakeDamage();
        }
    }

    IEnumerator ShakeCamera(float shakeRange, float myDurantion = 0.5f)
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

            _camera.transform.position = new Vector3(xPos, yPos, _camera.transform.position.z);
            yield return null;
        }

        _camera.transform.position = originalPosition;
    }

    private void TakeDamage()
    {
        if (!isAlive) return;

        _playerLifes.IncreaseHealth(1);
        _playerLifes.UpdateLifesCount();

        isImmortal = true;
        _animator.SetBool("isImmortal", true);

        StartCoroutine(ShakeCamera(_shakeRangeWithDamage));

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
        yield return _immortalTime;

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
            if (PlayerPosition == PositionsVariants.middle)
            {
                transform.position = new Vector2(transform.position.x, 3f);
                PlayerPosition = PositionsVariants.top;
            }
            else if (PlayerPosition == PositionsVariants.bottom)
            {
                transform.position = new Vector2(transform.position.x, 0);
                PlayerPosition = PositionsVariants.middle;
            }
            StartCoroutine(ShakeCamera(_shakeRangeWithMove, 0.15f));
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PlayerPosition == PositionsVariants.middle)
            {
                transform.position = new Vector2(transform.position.x, -3f);
                PlayerPosition = PositionsVariants.bottom;

            }
            else if (PlayerPosition == PositionsVariants.top)
            {
                transform.position = new Vector2(transform.position.x, 0);
                PlayerPosition = PositionsVariants.middle;
            }
            StartCoroutine(ShakeCamera(_shakeRangeWithMove, 0.15f));
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
                    if (PlayerPosition == PositionsVariants.middle)
                    {
                        transform.position = new Vector2(transform.position.x, -3f);
                        PlayerPosition = PositionsVariants.bottom;
                        
                    }
                    else if (PlayerPosition == PositionsVariants.top)
                    {
                        transform.position = new Vector2(transform.position.x, 0);
                        PlayerPosition = PositionsVariants.middle;
                    }
                }
                else if (touch.deltaPosition.y < -10f)
                {
                    if (PlayerPosition == PositionsVariants.middle)
                    {
                        transform.position = new Vector2(transform.position.x, 3f);
                        PlayerPosition = PositionsVariants.top;

                    }
                    else if (PlayerPosition == PositionsVariants.bottom)
                    {
                        transform.position = new Vector2(transform.position.x, 0);
                        PlayerPosition = PositionsVariants.middle;
                    }
                }
            }
        }
    }
}
