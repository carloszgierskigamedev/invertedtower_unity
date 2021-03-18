using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float _totalHp = 100;
    private float _currentHp = default;
    private Rigidbody2D _rb;
    private Color _originalColor;
    private Coroutine _damageTakenDisplayCoroutine;
    private SpriteRenderer _spriteRenderer;

    void Awake() 
    {
        _currentHp = _totalHp;
        _rb = GetComponent<Rigidbody2D>();   
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDamage(float attackDamage)
    {
        _currentHp -= attackDamage;

        Debug.Log("Ataque certeiro! Hp atual: " + _currentHp);

        if (_currentHp <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if(_damageTakenDisplayCoroutine != null) StopCoroutine(_damageTakenDisplayCoroutine);
            _damageTakenDisplayCoroutine = StartCoroutine(DamageTakenDisplay());
        }
    }

    IEnumerator DamageTakenDisplay()
    {
        //enemyMovement.enabled = false;
        //rigidbody2d.velocity = Vector3.zero;
        
        for (int i = 0; i < 5; i++)
        {
            if(i % 2 == 0) _spriteRenderer.color = Color.red;
            else _spriteRenderer.color = _originalColor;

            yield return new WaitForSeconds(.1f);
        }
        
        //enemyMovement.enabled = true;
        _spriteRenderer.color = _originalColor;

        _damageTakenDisplayCoroutine = null;
    }
}
