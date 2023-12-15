using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    private void multiplyBullets(Vector2 originalDirection, GameObject bullet)
    {
        bullet.GetComponent<BaseBulletBehavior>().isMultiplied = true;
        int bulletMultiplier = PlayerManager.GetInstance().bulletMultiplier;
        Rigidbody2D baseRb= bullet.GetComponent<Rigidbody2D>();
        float baseRotation = baseRb.transform.rotation.eulerAngles.z;

        float offset = 0f;

        for (int i = 0; i < bulletMultiplier; i++)
        {
            float newRotation;

            if (i % 2 == 0)
            {
                offset += 2;
                newRotation = baseRotation + offset;
            }
            else
            {
                newRotation = baseRotation - offset;
            }
            
            Vector3 eulerRotation = new Vector3(0f, 0f, newRotation);
            Quaternion rotationQuaternion = Quaternion.Euler(eulerRotation);

            GameObject childbullet = Instantiate(bullet, bullet.transform.position, rotationQuaternion);
            Rigidbody2D childRb = childbullet.GetComponent<Rigidbody2D>();

            Vector2 spreadAngle = Quaternion.Euler(0f, 0f, newRotation) * Vector2.up;

            childRb.velocity = spreadAngle * bullet.GetComponent<BaseBulletBehavior>().bulletSpeed;
            childbullet.transform.right = spreadAngle;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if(PlayerManager.GetInstance().multiply && !collision.GetComponent<BaseBulletBehavior>().isMultiplied)
            {
                Vector2 collisionVelocity = collision.GetComponent<Rigidbody2D>().velocity;
                multiplyBullets(collisionVelocity.normalized, collision.gameObject);
            }
        }
    }
}