using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
     
    public GameObject inventoryPanel;
    public Button fire;
    public Button water;
    public Button ground;
    public Button smoke;
    public Button iron;
    public Button nature;
    public int fire_count;
    public int water_count;
    public int ground_count;
    public int smoke_count ;
    public int iron_count;
    public int nature_count;

    int chosen_element_count;

    string firstclick;
    string secondclick;

    Color unactive_color;
    Color active_color;//new Color(130, 93, 0);

    bool activeInventory = false;
    bool smoke_active = false;
    bool iron_active = false;
    bool nature_active = false;
    private void Start()
    {
        unactive_color = new Color(1, 175/255f, 0,1);
        active_color = new Color(1, 1, 0,1);
        fire_count = 5;
        water_count = 5;
        ground_count = 5;
        smoke_count = 0;
        iron_count = 0;
        nature_count = 0;

    }
    private void Update()
    {
       

        fire.GetComponentInChildren<TextMeshProUGUI>().text = fire_count.ToString();
        water.GetComponentInChildren<TextMeshProUGUI>().text = water_count.ToString();
        ground.GetComponentInChildren<TextMeshProUGUI>().text = ground_count.ToString();
        smoke.GetComponentInChildren<TextMeshProUGUI>().text = smoke_count.ToString();
        iron.GetComponentInChildren<TextMeshProUGUI>().text = iron_count.ToString();
        nature.GetComponentInChildren<TextMeshProUGUI>().text = nature_count.ToString();

        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }

        

        if(firstclick != null && secondclick != null) {
            
            if (firstclick == secondclick)
            {
                //속성 변환 적용
                

                fire.image.color = unactive_color;
                water.image.color = unactive_color;
                ground.image.color = unactive_color;
                smoke.image.color = unactive_color;
                iron.image.color = unactive_color;
                nature.image.color = unactive_color;
                Debug.Log("슬라임의 속성이 바뀌었다.");
                firstclick = secondclick = null;
            }
            else {
                //first fire
                if (firstclick == "fire")
                {
                    if (secondclick == "water")
                    {
                        make_smoke();
                    }
                    else if (secondclick == "ground")
                    {
                        make_iron();
                    }
                }
                //first water
                if (firstclick == "water")
                {
                    if (secondclick == "fire")
                    {
                        make_smoke();
                    }
                    else if (secondclick == "ground")
                    {
                        make_nature();
                    }
                }
                //first ground
                if (firstclick == "ground")
                {
                    if (secondclick == "fire")
                    {
                        make_iron();
                    }
                    else if (secondclick == "water")
                    {
                        make_nature();
                    }
                }

                firstclick = secondclick = null;
            }
        }
        

    }

    public void FireOn() { 
        if(firstclick == null) {
            firstclick = "fire";
        }
        else if(secondclick == null)
        {
            secondclick = "fire";
        }
        fire.image.color = active_color;
    }
    public void WaterOn()
    {
        if (firstclick == null)
        {
            firstclick = "water";
            
        }
        else if (secondclick == null)
        {
            secondclick = "water";
            
        }
        water.image.color = active_color;
    }
    public void GroundOn()
    {
        if (firstclick == null)
        {
            firstclick = "ground";
            
        }
        else if (secondclick == null)
        {
            secondclick = "ground";
            
        }
        ground.image.color = active_color;
    }
    public void SmokeOn()
    {
        if (firstclick == null)
        {
            firstclick = "smoke";
            
        }
        else if (secondclick == null)
        {
            secondclick = "smoke";
            
        }
        smoke.image.color = active_color;
    }
    public void IronOn()
    {
        if (firstclick == null)
        {
            firstclick = "iron";
            
        }
        else if (secondclick == null)
        {
            secondclick = "iron";
            
        }
        iron.image.color = active_color;
    }
    public void NatureOn()
    {
        if (firstclick == null)
        {
            firstclick = "nature";
            
        }
        else if (secondclick == null)
        {
            secondclick = "nature";
            
        }
        nature.image.color = active_color;
    }

    void make_smoke() {
        if (fire_count != 0 && water_count != 0)
        {
            Debug.Log("연기속성석 획득");
            fire_count -= 1;
            water_count -= 1;
            smoke_count += 1;

            fire.image.color = unactive_color;
            water.image.color = unactive_color;
        }
        else { Debug.Log("속성석이 부족합니다"); }
    }

    void make_iron()
    {
        if (fire_count != 0 && ground_count != 0)
        {
            Debug.Log("강철 속성석 획득");
            fire_count -= 1;
            ground_count -= 1;
            iron_count += 1;

            fire.image.color = unactive_color;
            ground.image.color = unactive_color;
        }
        else { Debug.Log("속성석이 부족합니다"); }
    }

    void make_nature()
    {
        if (water_count != 0 && ground_count != 0)
        {
            Debug.Log("자연 속성석 획득");
            water_count -= 1;
            ground_count -= 1;
            nature_count += 1;

            water.image.color = unactive_color;
            ground.image.color = unactive_color;
        }
        else { Debug.Log("속성석이 부족합니다"); }
    }

}
