using UnityEngine;

namespace Assets.Scripts.Abilities
{
    public class EvilBloodBallAbility : Ability
    {

        public GameObject bloodBall;

        private readonly float offsetDistance = 0;


        protected override bool Do()
        {
            Quaternion quanternion = GetQuaternion(transform.position, skill.GetTargetPosition());
            return Do(quanternion);
        }

        private bool Do(Quaternion quanternion)
        {

            // increase the offset position to avoid collision with the player
            Vector2 offsetPosition = transform.position;

            offsetPosition += skill.GetOffSet();

            offsetPosition = new(
                offsetPosition.x + (offsetDistance * Mathf.Cos(quanternion.eulerAngles.z * Mathf.Deg2Rad)),
                offsetPosition.y + (offsetDistance * Mathf.Sin(quanternion.eulerAngles.z * Mathf.Deg2Rad))
            );

            GameObject resourceGameObject = Instantiate(bloodBall, offsetPosition, quanternion);

            resourceGameObject.GetComponent<Projectile>().SetOrigin(skill.OriginName);

            return true;
        }


        public static Quaternion GetQuaternion(Vector2 from, Vector2 to)
        {
            var rotation = to - from; // Calculate the rotation vector
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Calculate the angle in degrees
            return Quaternion.Euler(0f, 0f, rotZ); // Set the rotation of the weapon
        }

  

    }
}