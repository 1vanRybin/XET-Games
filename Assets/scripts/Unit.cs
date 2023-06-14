using System;
using UnityEngine;

public class Unit: MonoBehaviour
{ 
        public float MaxHP { get; } = 100;
        public float MaxMana { get; } = 100;
        public string UnitName { get; }
        public float CurrentHP { get; private set; }
        public float CurrentMana { get; private set; }
        public float Defence { get; }
        public float Skill { get; }
        public float Weakness { get; set; }

    public bool TakeDamage(float dmg)
        {
                CurrentHP -= (float)Math.Round(dmg);
                if (CurrentHP <= 0)
                        CurrentHP = 0;
                return CurrentHP <= 0;
        }

        public void Heal(float heal)
        {
                CurrentHP += (float)Math.Round(heal);
                if (CurrentHP > MaxHP)
                        CurrentHP = MaxHP;
        }

        public void Mana(float helMana)
        {
                CurrentMana += (float)Math.Round(helMana);
                if (CurrentMana > MaxMana)
                        CurrentHP = MaxHP;
        }
} 
