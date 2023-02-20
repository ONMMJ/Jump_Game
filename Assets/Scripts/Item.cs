using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ȭ: ����Ƽ ���ο��� ������Ʈ �ʵ忡 ������ �� �ִ� ���·� ��ȯ�Ѵ�.
[System.Serializable]
public class Item
{
    public int id;          // �������� �����ϴ� ���� ��ȣ.
    public Sprite sprite;   // �̹���.
    public string name;     // �̸�.
    public string content;  // ����.
    public int count;       // ����.

    public Item()
    {
        id = -1;            // �⺻ �������� id ���� -1�̴�.
    }

    public Item GetCopy()
    {
        // ���� ������ ���� ������ ���ο� ��ü�� ��ȯ.
        Item copy = new Item();
        copy.id = id;
        copy.sprite = sprite;
        copy.name = name;
        copy.content = content;
        copy.count = 1;
        return copy;
    }
}
