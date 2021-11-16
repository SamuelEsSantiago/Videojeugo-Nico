using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Item", menuName = "Equipment/Jetpack")]
public class Jetpack : Equipment
{
    Vector2 empuje;
    public int uses;
    [SerializeField] int maxUses = 5;
    [SerializeField] const float defaultForce = 10f;
    [SerializeField] float speed;
    public override void Rutina(){
        if(uses < 0) return;
        uses --;
        Rigidbody2D body = PlayerManager.instance.GetComponent<Rigidbody2D>();
        Debug.Log("Usando " + this.name);
        empuje = new Vector2(0f,defaultForce);
        //body.gravityScale *= -1;
        body.velocity = new Vector2(0f,0f);
        body.AddForce(empuje * speed,ForceMode2D.Impulse);
        PlayerManager.instance.isJumping = true;
        
    }

    public override void StartEquip(){
    }
    public override void EndEquip(){

    }
    public void RestablecerUsos()
    {
        if(PlayerManager.instance.isGrounded){
            uses = maxUses;
        }
    }

    public override void Use()
    {
        if(isPasive){
            //solo se equipan los equipamientos pasivos y los activos solo ejecutan su rutina...
            if(EquipmentManager.instance.GetCurrentEquipment()[(int)equipmentSlot] == this){
                EquipmentManager.instance.Unequip((int) equipmentSlot);
                return;
            }   
            EquipmentManager.instance.Equip(this);
        }else{
            Rutina();
        }
    }
}
