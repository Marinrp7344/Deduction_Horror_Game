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
    public List<GameObject> inventoryObjects;

    public AudioSource crucifixAudio;
    public GameObject gunShotPrefab;
    public Camera_Animator player;
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
                //frequencyDevice.gameObject.SetActive(true);
            }
            else
            {
                //frequencyDevice.gameObject.SetActive(false);
            }
        }
        if (clicked && currentlySelectedSlot != null && inventoryActive)
        {
            if (currentlySelectedSlot.itemType != InventorySlot.Item.None)
            {
                UseItem();
                UpdateHeldItem();
                UpdateInventoryUISlots();
            }
        }
        else
        {
            if(crucifixAudio.isPlaying)
            {
                crucifixAudio.Stop();
            }
        }

    }

    public void UpdateHeldItem()
    {
        if (inventoryActive && currentlySelectedSlot != null)
        {
            switch (currentlySelectedSlot.itemType)
            {
                case InventorySlot.Item.Bullet:
                    inventoryObjects[0].SetActive(true);
                    inventoryObjects[1].SetActive(false);
                    inventoryObjects[2].SetActive(false);
                    break;
                case InventorySlot.Item.Crucifix:
                    inventoryObjects[0].SetActive(false);
                    inventoryObjects[1].SetActive(true);
                    inventoryObjects[2].SetActive(false);
                    break;
                case InventorySlot.Item.FrequencyDisruptor:
                    inventoryObjects[0].SetActive(false);
                    inventoryObjects[1].SetActive(false);
                    inventoryObjects[2].SetActive(true);
                    break;
            }
            player.viewsButtons.DisableButtons();
        }
        else
        {
            inventoryObjects[0].SetActive(false);
            inventoryObjects[1].SetActive(false);
            inventoryObjects[2].SetActive(false);
            player.viewsButtons.ChangeButtons(player.GetCurrentView());
        }
    }

    public void ActivateInventory()
    {
        inventoryActive = true;
        
        //inventoryUIObject.SetActive(true);
    }

    public void DeactivateInventory()
    {
        inventoryActive = false;
        inventoryUIObject.SetActive(false);
        //frequencyDevice.gameObject.SetActive(false);
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
        Instantiate(gunShotPrefab, transform.position, Quaternion.identity);

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
        if (!crucifixAudio.isPlaying)
        {
            crucifixAudio.Play();
        }

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
        UpdateHeldItem();
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

        UpdateHeldItem();
        UpdateInventoryUISlots();
    }

    private void CheckKeyInventoryInteraction(int num)
    {
        if (currentlySelectedSlot != null)
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
        else
        {
            currentlySelectedSlot = inventorySlots[num];
        }
    }

    public void LiftButton()
    {

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
