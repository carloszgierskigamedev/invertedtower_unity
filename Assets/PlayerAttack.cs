using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack variables")]
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private float _attackDamage = 25f;
    [Header("Layer")]
    [SerializeField] private LayerMask _enemyLayer = default;

    // Update is called once per frame
    void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Attack!");
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 attackDirection = (mousePosition - transform.position).normalized;
            Vector3 attackPosition = transform.position + attackDirection * _attackRange; 
            Debug.Log(attackPosition);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, _attackRadius, _enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit!");
                EnemyHealth enemyHealth = enemy.gameObject.GetComponent<EnemyHealth>();

                if (enemyHealth == null) return;
                else
                {
                    enemyHealth.HandleDamage(_attackDamage);
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 attackDirection = (mousePosition - transform.position).normalized;
        Vector3 attackPosition = transform.position + attackDirection * _attackRange; 
        Gizmos.DrawWireSphere(attackPosition, _attackRadius);
    }

    // Get mouse position in world with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
