using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    private string GameOverScene;
    [SerializeField]
    private string WinScene;
    private LayerMask tileLayer;
    private float rayDistance = 0.55f;
    private Vector2 moveDirection = Vector2.right;
    private Direction direction = Direction.Right;
    private AudioSource audioSource;

    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    private float maxCoin = 182;
    private float currentCoin;

    private float maxHp = 5;
    private float currentHP;

    public float MaxHP => maxHp;
    public float CurrentHP => currentHP;

    private Movement2D movement2D;
    private AroundWrap aroundWrap;
    private SpriteRenderer spriteRenderer;

    private void Awake(){

        tileLayer = 1<<LayerMask.NameToLayer("Tile");
        currentHP = maxHp;
        currentCoin = maxCoin;
        audioSource = GetComponent<AudioSource>();
        movement2D = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    
    private void Update(){
        if( Input.GetKeyDown(KeyCode.UpArrow)){
            moveDirection = Vector2.up;
            direction = Direction.Up;
        }
        else if( Input.GetKeyDown(KeyCode.LeftArrow)){
            moveDirection = Vector2.left;
            direction = Direction.Left;
        }
        else if( Input.GetKeyDown(KeyCode.RightArrow)){
            moveDirection = Vector2.right;
            direction = Direction.Right;
        }
        else if( Input.GetKeyDown(KeyCode.DownArrow)){
            moveDirection = Vector2.down;
            direction = Direction.Down;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        if( hit.transform == null ){

            bool movePossible = movement2D.MoveTo(moveDirection);
            if ( movePossible ){
                transform.localEulerAngles = Vector3.forward * 90 * (int)direction;
            }
            aroundWrap.UpdateAroundWrap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if ( collision.CompareTag("Item")){
            score += 10;
            currentCoin -=1;
            Destroy(collision.gameObject);
            audioSource.Play();
            if( currentCoin <= 0 ) {

                PlayerPrefs.SetInt("Score", score);

                SceneManager.LoadScene(WinScene);
            }
        }

        if ( collision.CompareTag("Enemy")){
            Score -= 50;
            currentHP -= 1;
            StopCoroutine("OnHit");
            StartCoroutine("OnHit");
            if(currentHP <= 0 ){
                SceneManager.LoadScene(GameOverScene);
            }
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator OnHit(){
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}
