using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �������� ������ ������ �ִ�.
    public Item[] items { get; private set; }

    private void Start()
    {
        items = new Item[16];
    }


    public bool AddItem(Item item)
    {
        // ����� ����ִ� ĭ�� �������� �����Ѵ�.
        // true: �������� ���������� ȹ���Ѵ�.
        // false: �κ��丮�� �� �� �־� ȹ������ ���ߴ�.
        for(int i = 0; i < items.Length; i++)
        {
            // items�� ���� �ʱⰪ�� null�� ������
            // ����ȭ �Ӽ��� ���� �ν����Ϳ� ������°� �Ǹ� null�� �ƴϰԵȴ�.
            // ���� ���� ������ ǥ���Ҷ��� id�� -1�� ǥ���Ѵ�.
            if(items[i] == null || items[i].id == -1)
            {
                items[i] = item;
                return true;
            }
        }
        return false;
    }
}
