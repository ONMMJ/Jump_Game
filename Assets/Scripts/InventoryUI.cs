using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] GameObject panel;
    [SerializeField] ItemSlotUI[] slots;

    [Header("Context")]
    [SerializeField] RectTransform contextPanel;
    [SerializeField] TMP_Text contextText;

    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        panel.SetActive(false);
        Close();
    }

    public void UpdateItem(Inventory inventory)
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].Setup(inventory.items[i]);
    }

    public bool SwitchInventory()
    {
        panel.SetActive(!panel.activeSelf);     // ���� ������ �ݴ�� �����Ѵ�.
        return panel.activeSelf;                // ���� ���¸� ��ȯ�Ѵ�.
    }

    public void ShowContext(string context, Vector3 position)
    {
        // �ؽ�Ʈ ����.
        contextText.text = context;

        // �г��� ũ�� �� ��ġ ����.
        contextPanel.gameObject.SetActive(true);
        contextPanel.position = position;
        contextPanel.sizeDelta = new Vector2(contextText.preferredWidth, contextPanel.sizeDelta.y);
    }
    public void Close()
    {
        contextPanel.gameObject.SetActive(false);
    }
}
