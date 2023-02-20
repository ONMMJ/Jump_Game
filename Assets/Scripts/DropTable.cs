using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [System.Serializable]
    public struct TableElement
    {
        public int id;              // ������ �ڵ�(ID).
        public float weight;        // ����ġ.
    }
    // ���� � �������� ����� �� �ִ°��� �����ϰ�
    // Ȯ���� ���� Ư�� �������� �����Ѵ�.
    [SerializeField] Transform createPosition;
    [SerializeField] TableElement[] elements;

    public void OnCreateItem()
    {
        // ��� ���̺��� ����ġ�� ���� ����ġ�� ���ϰ�
        // Random.value�� ���� Ư�� ��ġ�� ��´�.
        float totalWeight = elements.Select(element => element.weight).Sum();
        float pick = Random.value * totalWeight;

        int itemID = 0;

        // �ݺ����� ��ȸ�ϸ� ���� � ���̺� ���ԵǴ��� ����Ѵ�.
        float current = 0f;
        for(int i = 0; i < elements.Length; i++)
        {
            current += elements[i].weight;
            if (pick < current)
            {
                itemID = elements[i].id;
                break;
            }
        }
        // Ȯ�������� ���õ� ID�� ���� ������ ������Ʈ�� DB���Լ� �����´�.
        // ���� ���� ��ġ�� �̵���Ų��.
        ItemObject itemObject = ItemDB.Instance.GetItemObject(itemID);
        itemObject.transform.position = createPosition.position;
    }

}
