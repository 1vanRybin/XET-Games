using UnityEngine;

public class Unit: MonoBehaviour
{ 
        public float MaxHP = 100;
        public float MaxMana = 100;
        public string UnitName;
        public  float CurrentHP;
        public  float CurrentMana;
        public  float Defence;
        public  float Skill;
        public float Weaknes;

        public bool TakeDamage(float dmg)
        {
                CurrentHP -= dmg;
                
                return CurrentHP <= 0;
        }

        public void Heal(float heal)
        {
                CurrentHP += heal;
                if (CurrentHP > MaxHP)
                        CurrentHP = MaxHP;
        }

        public void Mana(float helMana)
        {
                CurrentMana += helMana;
                if (CurrentMana > MaxMana)
                        CurrentHP = MaxHP;
        }
} 
