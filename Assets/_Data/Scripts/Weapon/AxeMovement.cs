using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement : WeaponMovement
{
    protected override IEnumerator MoveAndRotateWeapon(Transform targetBot, float throwSpeed, float rotationSpeed)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetBot.position;

        while (true)
        {
            elapsedTime += Time.deltaTime * throwSpeed;
            float speed = Mathf.Clamp01(elapsedTime / 5f);

            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, speed);
            newPosition.y = 1;
            targetPosition.y = 1;
            transform.SetPositionAndRotation(newPosition, Quaternion.Euler(-90, rotationSpeed * elapsedTime, 0));

            if (elapsedTime % 0.1f < Time.deltaTime)
            {
                float distance = Vector3.Distance(newPosition, targetPosition);
                if (distance <= 0.1f)
                {
                    DespawnWeapon();
                    yield break;
                }
            }

            yield return null;
        }

    }
}
