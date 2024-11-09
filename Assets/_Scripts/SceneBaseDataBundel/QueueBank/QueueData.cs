using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//QueueData

//�retilmeyi bekleyen sipri�leri depolar.
public class OrderQD:IQueueable
{
    protected Customer _customer;
    public Customer GetCustomer()
    { 
        return _customer; 
    }
}

//Haz�rlanan yiyecekklerin st�� fiyat�n� ve kime gidecegini depolar.
public class PlateQD : OrderQD
{
    public PlateQD(double price,OrderQD order)
    {

        base._customer = order.GetCustomer();
    }
    
}

//Hen�z sipri� vermemi� olan  m��terileri deploar.
public class WithoutOrderCustomerQD: IQueueable
{
    protected Customer _customer;
    WithoutOrderCustomerQD(Customer customer) {
        _customer = customer;
    }

    public Customer GetCustomer()
    {
        return _customer;
    }
}

