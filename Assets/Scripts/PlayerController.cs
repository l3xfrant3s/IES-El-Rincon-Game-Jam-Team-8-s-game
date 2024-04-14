using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct CreatureItemStruct
{
    public string name;
    public GameObject prefab;
}

public class PlayerController : MonoBehaviour
{

    public int mana = 100;
    public int manaMax = 100;
    public LayerMask gridLayer;
    public string selectedCreature = "";
    public TextMeshProUGUI selectedCreatureText;
    public TextMeshProUGUI manaText;
    public List<CreatureItemStruct> creatureItems;
    public float timerPerOneMana = 0f;
    public float secondsPerOneMana = 0.5f;

    private void Start()
    {
        UpdateManaText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !selectedCreature.Equals(""))
        {
            Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(posicionMouse, Vector2.zero, Mathf.Infinity, gridLayer);
            if (hit.collider != null && hit.collider.gameObject.transform.childCount == 0)
            {
                GameObject hitGameObject = hit.collider.gameObject;
                GameObject creature = GetCreatureSelected();
                summonedBehaviour summonedComponent = creature.GetComponent<summonedBehaviour>();
                if (summonedComponent.manaCost <= mana)
                {
                    Transform hitTransform = hitGameObject.transform;
                    Instantiate(creature, new Vector2(hitTransform.position.x, hitTransform.position.y), Quaternion.identity, hitTransform);
                    mana -= summonedComponent.manaCost;
                    UpdateManaText();
                }
            }
        }

        timerPerOneMana += Time.deltaTime;
        if (timerPerOneMana > secondsPerOneMana)
        {
            mana += 1;
            if (mana > manaMax)
            {
                mana = manaMax;
            }
            timerPerOneMana -= secondsPerOneMana;
            UpdateManaText();
        }
    }

    void UpdateManaText()
    {
        manaText.text = "Mana: " + mana + "/" + manaMax;
    }

    public void ChangeSelectedCreature(string creature)
    {
        selectedCreature = creature;
        selectedCreatureText.text = "Criatura seleccionada: " + creature;
    }

    public GameObject GetCreatureSelected()
    {
        return creatureItems.Find(element => element.name == selectedCreature).prefab;
    }
}
