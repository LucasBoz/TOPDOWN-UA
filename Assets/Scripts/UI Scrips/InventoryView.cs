using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryView : StorageView
{
    
    [SerializeField] string panelName = "Inventory";
    
    public override IEnumerator InitializeView(int size = 20)
    {
        Slot[] slots = new Slot[size];

        root = document.rootVisualElement;
        root.Clear();
        
        root.styleSheets.Add(styleSheet);

        container = new VisualElement();
        container.AddToClassList("container");
        root.Add(container);

        var inventory = new VisualElement();
        inventory.AddToClassList("inventory");
        container.Add(inventory);
        
        var inventoryFrame = new VisualElement();
        inventoryFrame.AddToClassList("inventoryFrame");
        inventory.Add(inventoryFrame);

        var inventoryHeader = new VisualElement();
        inventoryHeader.AddToClassList("inventoryHeader");
        inventoryHeader.Add(inventoryHeader);
        
        var slotsContainer = new VisualElement();
        slotsContainer.AddToClassList("slotsContainer");
        inventoryFrame.Add(slotsContainer);

        for (int i = 0; i < size; i++)
        {
            var slot = new VisualElement();
            slot.AddToClassList("slot");

            slotsContainer.Add(slot);

            slots[i] = slot;
        }
        
        yield return null;

    }
}