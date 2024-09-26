using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class AbilitySo : ScriptableObject
    {
        [Header("General Settings")]
        [SerializeField] private string abilityName;
        [SerializeField] private Sprite abilityIcon;

        [SerializeField] private float maxDuration;
        protected float duration;

        [SerializeField] private float maxCoolDownTime;
        protected float coolDownTime;

        public float MaxDuration => maxDuration;
        public float Duration => duration;

        public float CoolDownTime => coolDownTime;
        public float MaxCoolDownTime => maxCoolDownTime;

        public string AbilityName => abilityName;

        public Sprite AbilityIcon => abilityIcon;


        private void Awake()
        {
            InitializeAbility();
        }

        public void InitializeAbility()
        {
            duration = maxDuration;
            coolDownTime = maxCoolDownTime;
        }

        public bool InUse()
        {
            return duration < maxDuration;
        }

        public bool IsReadyToUse()
        {
            return (coolDownTime >= maxCoolDownTime && duration >= MaxDuration);
        }

        public bool IsInCoolDown()
        {
            return coolDownTime < maxCoolDownTime;
        }

        public abstract void UpdateDuration();

        public abstract void UpdateCoolDownTime();

        public abstract void UseAbility(Transform spawnPoint);
        public abstract void ForceCancelAbility();
        
    }
}