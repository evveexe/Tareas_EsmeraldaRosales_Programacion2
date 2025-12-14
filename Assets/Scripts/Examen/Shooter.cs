using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;

    [Header("Mouse Aiming")]
    public float maxShootDistance = 100f;

    private float nextFireTime = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {        
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            ShootAtMouse();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootAtMouse()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Falta Projectile Prefab o Fire Point");
            return;
        }
       
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;
        
        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
            targetPoint = hit.point;
        }
        else
        {            
            targetPoint = ray.GetPoint(50f);
        }
                
        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;
                
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        Debug.Log($"Disparando-");

        // Tracer porque las pelotas no se veianxd
        Debug.DrawLine(firePoint.position, targetPoint, Color.red, 1f);
    }
}