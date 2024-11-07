using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class Damagable : MonoBehaviour
{
    public int MaxHealth = 100;
    [SerializeField]
    private int health;
    GameObject aiDetector;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / MaxHealth);
        }
    }

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHeal;
    public UnityEvent OnHit;
    public GameObject canvas;


    private void Start()
    {
        Health = MaxHealth;
    }

    internal void Hit(int damagePoints)
    {
        Health -= damagePoints;
        if (Health <= 0)
        {
            string nameTag =  transform.gameObject.tag;
            OnDead?.Invoke();
            if (nameTag == "Nave1"  || nameTag == "Nave2")
            {
                if(nameTag.Contains("1"))
                {
                    aiDetector = GameObject.FindGameObjectWithTag("Nave2").transform.GetChild(2).gameObject;
                    aiDetector.GetComponent<AIDetector>().Target = null;
                    nameTag = "Player 2";
                }
                else
                {
                    aiDetector = GameObject.FindGameObjectWithTag("Nave1").transform.GetChild(2).gameObject;
                    aiDetector.GetComponent<AIDetector>().Target = null;
                    nameTag = "Player 1";
                }
                //canvas.GetComponent<TMP_Text>().text = "Game Over";
                GameObject text =  canvas.transform.GetChild(0).transform.GetChild(0).gameObject;
                text.GetComponent<TextMeshProUGUI>().text = "Gano " + nameTag ;
                canvas.SetActive(true);
            }

        }
        else 
        {
            OnHit?.Invoke();
        }
    }

    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health, 0 , MaxHealth);
        OnHeal?.Invoke();
    }

}
