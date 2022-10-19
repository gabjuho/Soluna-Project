using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3FManager : MonoBehaviour
{
    public ClearImage clear_image;
    public Inventory inv;
    public Item[] item;
    public Light[] planet_light;
    public Light[] crystal_light;
    public bool isClear = false; //3�� Ŭ���� ����

    public int count = 0; //���� ���� �༺�� ���� -> ���߿� private�� ����

    void Start()
    {
        Invoke("AddTestItem", 0.5f);
    }

    private void AddTestItem()
    {
        for (int i = 0; i < 9; i++)
            inv.AddItem(item[i]);
    }

    public bool CheckPlanet(ItemType itemType) //����� �������� �´� �༺���� üũ
    {
        if(item[count].itemType == itemType)
            return true;

        return false;
    }

    public void ActivePlanet() //���� �༺ Ȱ��ȭ
    {
        planet_light[count].enabled = true;

        if(count > 0)
            crystal_light[count - 1].enabled = true;

        count++; //���� �༺ ���� 1����

        if (count == 9)
            PuzzleComplete();
    }

    public void ResetPlanet() //�༺ ���� �ʱ�ȭ
    {
        for (int i = 0; i < count; i++)
        {
            planet_light[i].enabled = false;
            crystal_light[i].enabled = false;
        }

        for (int i = 0; i < count; i++)
            inv.AddItem(item[i]);

        count = 0;
    }

    public void PuzzleComplete()
    {
        isClear = true;
        GameObject.Find("ObjectTriger").GetComponent<ObjectEventTriger>().GetText(210, true);
        clear_image.FadeReady();
    }
}
