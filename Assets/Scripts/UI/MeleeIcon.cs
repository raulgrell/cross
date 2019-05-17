using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeIcon : MonoBehaviour
{
    private Image icon;
    public Sprite[] iconImages;
    private UnitAttack currentAttack;

    private void Start()
    {
        icon = GetComponent<Image>();
    }

    public void onChangeWeapon(UnitAttack attack)
    {
        currentAttack = attack;
    
        if(currentAttack.name == "BasicAttack")
            icon.sprite = iconImages[0];
        
        if (currentAttack.name == "SlashAttack")
            icon.sprite = iconImages[1];

        if (currentAttack.name == "KnifeAttack")
            icon.sprite = iconImages[2];
    }
}
