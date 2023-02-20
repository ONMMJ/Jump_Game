using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item�� �������̱� ������ ���� ������ �� ����.
// ���� ������ �����͸� ������ �ִ� ������Ʈ�� ������ �Ѵ�.
public class ItemObject : MonoBehaviour
{
    [SerializeField] Item item;

    public string Name => (item == null) ? "�̸� ����" : item.name;

    public void OnInteraction(GameObject order)
    {
        // ��û�ڰ� �κ��丮�� ��� �ִ��� Ȯ���Ѵ�.
        Inventory inven = order.GetComponent<Inventory>();

        // ��� �ִٸ� �κ��丮�� ������ ������ �õ��ϰ�
        // ���Կ� �����ߴٸ� ������Ʈ�� �����Ѵ�.
        if(inven != null && inven.AddItem(item))
            Destroy(gameObject);
    }

    public void Setup(Item item)
    {
        this.item = item;
    }
}
