using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Item i1 = new Item("Apple", 10);
        Item i2 = new Item("Banana", 5);
        Item i3 = new Item("Orange", 8);
        Debug.Log("添加操作");
        ItemManager.Instance.addList(i1);
        ItemManager.Instance.addList(i2);
        ItemManager.Instance.addList(i3);
        Debug.Log("查询操作");
        ItemManager.Instance.findList();
        Debug.Log("移除操作");
        ItemManager.Instance.removeList(i2);
        ItemManager.Instance.findList();
        Debug.Log("更改操作");
        ItemManager.Instance.changeList(i1, "Green Apple", 15);
        ItemManager.Instance.findList();
    }

}
//物品类，如果想要拓展可以配合继承，还可以写一个接口
public class Item
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public Item(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}
//单例，物品管理器，统一管理
public class ItemManager
{
    public ArrayList ArrayList = new ArrayList();
    private ItemManager() { }
    private static ItemManager _instance;
    public static ItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ItemManager();
            }
            return _instance;
        }
    }
    public void addList(Item item)
    {
        ArrayList.Add(item);
        Debug.Log($"已添加:{item.Name}");
    }
    public void removeList(Item item)
    {
        foreach (Item item1 in ItemManager.Instance.ArrayList)
        {
            if (ItemManager.Instance.ArrayList.Contains(item))
            {
        
                ItemManager.Instance.ArrayList.Remove(item);
                return;
            }
        }
    }
    public void changeList(Item item,string Name, int Quantity)
    {
        foreach(Item item1 in ItemManager.Instance.ArrayList)
        {
            if (ItemManager.Instance.ArrayList.Contains(item))
            {
                item.Name = Name;
                item.Quantity = Quantity;
                Debug.Log($"已修改:{item.Name}为{item1.Name},数量为{item1.Quantity}");
                return;
            }
        }
    }
    public void findList()
    {
        foreach (Item item in ItemManager.Instance.ArrayList)
        {
            Debug.Log($"物品名称: {item.Name}, 数量: {item.Quantity}");
        }
    }
}
