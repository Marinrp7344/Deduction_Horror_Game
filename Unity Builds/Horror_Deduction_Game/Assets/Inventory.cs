using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> inventorySlots;
    public InventorySlot currentlySelectedSlot;
    public Inventory_UI inventoryUI;
    public bool clicked;
    public bool liftedButton = true;
    public float chargeDecayRate;
    public bool inventoryActive;
    public GameObject inventoryUIObject;

    public Camera playerCam;
    public float shootRange;
    public LayerMask shootingMask;
    public LayerMask crucifixMask;
    public LayerMask frequencyMask;

    public FrequencyDevice frequencyDevice;


    private void Start()
    {
        UpdateInventoryUISlots();
    }

    private void Update()
    {

        if(currentlySelectedSlot != null)
        {
            if(currentlySelectedSlot.index == 2 && inventoryActive)
            {
                frequencyDevice.gameObject.SetActive(true);
            }
            else
            {
                frequencyDevice.gameObject.SetActive(false);
            }
        }
        if (clicked && currentlySelectedSlot != null && inventoryActive)
        {
            UseItem();
            UpdateInventoryUISlots();
        }
    }

    public void ActivateInventory()
    {
        inventoryActive = true;
        inventoryUIObject.SetActive(true);
    }

    public void DeactivateInventory()
    {
        inventoryActive = false;
        inventoryUIObject.SetActive(false);
        frequencyDevice.gameObject.SetActive(false);
    }

    public void UpdateInventoryUISlots()
    {
        inventoryUI.UpdateUI(inventorySlots);
    }

    public void UseItem()
    {
        switch(currentlySelectedSlot.itemType)
        {
            case InventorySlot.Item.Bullet:
                UseBullet();
                break;
            case InventorySlot.Item.Crucifix:
                UseCrucifix();
                break;
            case InventorySlot.Item.FrequencyDisruptor:
                UseFrequencyDisruptor();
                break;
        }
    }

    public void UseBullet()
    {
        if(currentlySelectedSlot.amount > 0 && liftedButton)
        {
            currentlySelectedSlot.amount -= 1;
            ShootBullet();
            liftedButton = false;
        }
    }

    public void ShootBullet()
    {
        Ray ray = playerCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange, shootingMask))
        {
            GameObject enemyBody = hit.collider.gameObject.transform.parent.gameObject;
            Enemy enemyScript = enemyBody.GetComponent<Enemy>();
            enemyScript.HitBullet();
        }

    }

    public void ChooseFrequencyTarget()
    {
        Ray ray = playerCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange, frequencyMask))
        {
            GameObject enemyBody = hit.collider.gameObject.transform.parent.gameObject;
            Enemy enemyScript = enemyBody.GetComponent<Enemy>();
            frequencyDevice.SetTargetFrequency(enemyScript);
        }

    }

    public void ShowCrucifix()
    {
        Ray ray = playerCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange, crucifixMask))
        {
            GameObject enemyBody = hit.collider.gameObject.transform.parent.gameObject;
            Enemy enemyScript = enemyBody.GetComponent<Enemy>();
            enemyScript.HitCrucifix();
        }

    }

    public void UseCrucifix()
    {
        if(currentlySelectedSlot.charge > 0)
        {
            currentlySelectedSlot.charge -= chargeDecayRate * Time.deltaTime;
            ShowCrucifix();
        }
        else
        {
            currentlySelectedSlot.charge = 0;
        }
    }

    public void UseFrequencyDisruptor()
    {
        if (liftedButton)
        {
            ChooseFrequencyTarget();
            liftedButton = false;
        }
    }

    public void OnAttack(InputValue input)
    {

        if(input.Get<float>() == 1)
        {
            clicked = true;
        }
        else
        {
            clicked = false;
            liftedButton = true;
        }
    }

    public void OnScrollInventory(InputValue input)
    {
        float scroll = input.Get<float>();

        if(currentlySelectedSlot == null)
        {
            currentlySelectedSlot = inventorySlots[0];
        }
        else
        {
            float currentIndex = currentlySelectedSlot.index + scroll;

            if(currentIndex < 0)
            {
                currentIndex = 2;
            }
            else if(currentIndex > 2)
            {
                currentIndex = 0;
            }

            currentlySelectedSlot = inventorySlots[(int)currentIndex];
        }

        UpdateInventoryUISlots();
        
    }

    public void OnChangeInventory(InputValue input)
    {
        float numPressed = input.Get<float>();
        Debug.Log(numPressed);

        switch ((int)numPressed)
        {
            case 1:
                CheckKeyInventoryInteraction(0);
                break;
            case 2:
                CheckKeyInventoryInteraction(1);
                break;
            case 3:
                CheckKeyInventoryInteraction(2);
                break;
        }

        UpdateInventoryUISlots();
    }

    private void CheckKeyInventoryInteraction(int num)
    {
        if (currentlySelectedSlot.index == inventorySlots[num].index)
        {
            currentlySelectedSlot = null;
        }
        else
        {
            currentlySelectedSlot = inventorySlots[num];
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public int index;
    public Sprite icon;
    public int amount;
    public float charge;
    public enum Item { None, Bullet, Crucifix, FrequencyDisruptor }
    public Item itemType;
}
