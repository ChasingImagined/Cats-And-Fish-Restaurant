using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//QueueData

//Üretilmeyi bekleyen sipriþleri depolar.
public class OrderQD:IQueueable
{
    protected Customer _customer;
    public Customer GetCustomer()
    { 
        return _customer; 
    }
}

//Hazýrlanan yiyecekklerin stýþ fiyatýný ve kime gidecegini depolar.
public class PlateQD : OrderQD
{
    public PlateQD(double price,OrderQD order)
    {

        base._customer = order.GetCustomer();
    }
    
}

//Henüz sipriþ vermemiþ olan  müþterileri deploar.
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

