using UnityEngine;
using System;
using RPG.Attributes;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Make New Weapon", order = 0)]
    public class SO_Weapons : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController AnimatorOverride = null;
        [SerializeField] GameObject EquippedPrefab = null;
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] float weaponRange = 2f ;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        

        const string weaponName = "Weapon";

        public float GetWeaponRange ()
        {
            return weaponRange;
        }

        public float GetWeaponDomage ()
        {
            return weaponDamage;
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if(EquippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(EquippedPrefab, handTransform);
                weapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (AnimatorOverride != null)
            {
                animator.runtimeAnimatorController = AnimatorOverride;    
            }
            else if (overrideController != null)
            {         
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController; 
            }
            
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null) 
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if(oldWeapon == null) return;

            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);

        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public void LaunchProjectile(Transform rightHand, Transform LeftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, LeftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
            Debug.Log(target.tag);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
    }

}