using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Абстрактный класс "Носитель информации"
abstract class StorageDevice
{
    public string Name { get; set; }             
    public string Manufacturer { get; set; }     
    public string Model { get; set; }            
    public int Quantity { get; set; }            
    public decimal Price { get; set; }           

    public StorageDevice(string name, string manufacturer, string model, int quantity, decimal price)
    {
        Name = name;
        Manufacturer = manufacturer;
        Model = model;
        Quantity = quantity;
        Price = price;
    }

    public virtual void Print()
    {
        Console.WriteLine($"Имя: {Name}, Производитель: {Manufacturer}, Модель: {Model}, Кол-во: {Quantity}, Цена: {Price}");
    }

    public virtual void LoadFromFile(string filename)
    {
        Console.WriteLine("Загрузка из файла...");
    }

    public virtual void SaveToFile(string filename)
    {
        Console.WriteLine("Сохранение в файл...");
    }
}

class FlashMemory : StorageDevice
{
    public int MemorySize { get; set; }          // Объем памяти
    public int UsbSpeed { get; set; }            // Скорость USB

    public FlashMemory(string name, string manufacturer, string model, int quantity, decimal price, int memorySize, int usbSpeed)
        : base(name, manufacturer, model, quantity, price)
    {
        MemorySize = memorySize;
        UsbSpeed = usbSpeed;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Объём памяти: {MemorySize} GB, USB Скоррость: {UsbSpeed} MB/s");
    }
}

class DVD : StorageDevice
{
    public int ReadSpeed { get; set; }           // Скорость чтения
    public int WriteSpeed { get; set; }          // Скорость записи

    public DVD(string name, string manufacturer, string model, int quantity, decimal price, int readSpeed, int writeSpeed)
        : base(name, manufacturer, model, quantity, price)
    {
        ReadSpeed = readSpeed;
        WriteSpeed = writeSpeed;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Скорость чтения: {ReadSpeed}, Скорость записи: {WriteSpeed}");
    }
}

class HDD : StorageDevice
{
    public int DiskSize { get; set; }            // Размер диска
    public int UsbSpeed { get; set; }            // Скорость USB

    public HDD(string name, string manufacturer, string model, int quantity, decimal price, int diskSize, int usbSpeed)
        : base(name, manufacturer, model, quantity, price)
    {
        DiskSize = diskSize;
        UsbSpeed = usbSpeed;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Размер диска: {DiskSize} GB, USB Скорость: {UsbSpeed} MB/s");
    }
}

class Program
{
    static List<StorageDevice> devices = new List<StorageDevice>();

    static void Main(string[] args)
    {
        AddDevice(new FlashMemory("Flash Drive", "Kingston", "DT100G3", 10, 500, 64, 150));
        AddDevice(new DVD("DVD-RW", "Sony", "DRU-190A", 5, 150, 16, 8));
        AddDevice(new HDD("External HDD", "Seagate", "Expansion", 2, 2000, 1000, 500));

        Console.WriteLine("Перечень устройств:");
        PrintDevices();

        // Пример удаления устройства по критерию (по имени)
        RemoveDevice("Flash Drive");

        Console.WriteLine("\nПосле удаления Flash Drive:");
        PrintDevices();
    }

    static void AddDevice(StorageDevice device)
    {
        devices.Add(device);
    }

    static void RemoveDevice(string name)
    {
        devices.RemoveAll(d => d.Name == name);
    }

    static void PrintDevices()
    {
        foreach (var device in devices)
        {
            device.Print();
            Console.WriteLine();
        }
    }

    static StorageDevice FindDevice(string name)
    {
        return devices.FirstOrDefault(d => d.Name == name);
    }

     static void UpdateDevice(string name, string newModel, int newQuantity, decimal newPrice)
    {
        var device = FindDevice(name);
        if (device != null)
        {
            device.Model = newModel;
            device.Quantity = newQuantity;
            device.Price = newPrice;
            Console.WriteLine($"Обновлено {name}");
        }
        else
        {
            Console.WriteLine($"Устройство {name} не найдено");
        }
    }
}
