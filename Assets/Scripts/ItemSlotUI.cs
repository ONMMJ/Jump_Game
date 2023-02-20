using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Image selectedImage;

    Item item;
    RectTransform rectTransform;

    // ������Ʈ�� Ȱ��Ȱ �� ������ ȣ��.
    private void OnEnable()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        OnDeselectedSlot();
    }

    public void Setup(Item item)
    {
        // ������ �ʱ�ȭ �Լ�.
        this.item = item;
        bool isEmpty = (item == null) || (item.id == -1);

        if (isEmpty)
        {
            itemImage.enabled = false;
        }
        else
        {
            itemImage.sprite = item.sprite;
            itemImage.enabled = item.sprite != null;
        }
    }
    public void OnSelectedSlot()
    {
        // ���� �̹��� Ȱ��ȭ.
        selectedImage.enabled = true;
        if(item != null)
        {
            Vector3 position = rectTransform.position;
            position.y -= rectTransform.sizeDelta.y / 2f;
            InventoryUI.Instance.ShowContext(item.content, position);
        }
    }
    public void OnDeselectedSlot()
    {
        // ���� �̹��� ��Ȱ��ȭ.
        if (selectedImage.enabled)
        {
            selectedImage.enabled = false;

            if (item != null)
                InventoryUI.Instance.Close();
        }
    }
}
