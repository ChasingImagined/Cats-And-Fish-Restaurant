using ChasingImagined.Uitls;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class ResourceBankBase<T> : BankBase  where T : Resource
{
    [ContextMenuItem("Price To ABC price", nameof(RecalculateAbcCountAll))]
    [ContextMenuItem("ABC Price To Price", nameof(RecalculateCountAll))]
    [SerializeField] private List<ResourceAndCount<T>> _resourceAndCount = new List<ResourceAndCount<T>>();


    public override Type GetStoredType()
    {
        return typeof(T);
    }


    // ResourceAndCount refransýnýn içindeki caount syýyýný ABC tipli sayý  degrini kulanrk hesplar;
    private void AbcNumberToNumber(ref ResourceAndCount<T> rc)
    {
        (double number, bool numberCheck) = ABCNumbers.AbcToNumber(rc.abcCount);
        if (numberCheck)// Deger geçerli ise eþitle;
        {
            rc.count = number;
        }
        //ççððþþüüüiiöö
        else// degil ise sýfýrla;
        {
            ResetValue(ref rc);
        }
        
    }

    // ResourceAndCount refransýnýn içindeki ABCtipli syýyý count degrini kulanrk hesplar;
    private void NumberToAbcNumber(ref ResourceAndCount<T> rc)
    {
        
        (string abc, bool abcCheck) = ABCNumbers.NumberToAbc(rc.count);
        if (abcCheck) //deger geçerli ise eþitle
        {
            rc.abcCount = abc;
        }
        else //Degilse sýfýrla.
        {
            ResetValue(ref rc);
        }
        
    }

    // Gelen ResourceAndCount Refransýnnýn içindeki degrleri sýfýrlar.
    private void ResetValue( ref ResourceAndCount<T> resorceAndPrice)
    {
        resorceAndPrice.abcCount = "";
        resorceAndPrice.count = 0;
    }

    //tüm AbC tipli miktarý yeniden hesplar.
    private void RecalculateAbcCountAll()
    {
        for (int i = 0;i < _resourceAndCount.Count;i++)
        {
            // Geçici bir deðiþken oluþturup, ref ile metodu çaðýrýyoruz.
            ResourceAndCount<T> tempRc = _resourceAndCount[i];
            NumberToAbcNumber(ref tempRc);
            // Geçici deðiþken üzerinde yapýlan deðiþiklikleri tekrar listeye yansýtýyoruz.
            _resourceAndCount[i] = tempRc;
        }
    }

    //tüm  Double tipili miktarlarý yeniden hesplar.
    private void RecalculateCountAll()
    {
        for (int i = 0; i < _resourceAndCount.Count; i++)
        {
            // Geçici bir deðiþken oluþturup, ref ile metodu çaðýrýyoruz.
            ResourceAndCount<T> tempRc = _resourceAndCount[i];
            AbcNumberToNumber(ref tempRc);
            // Geçici deðiþken üzerinde yapýlan deðiþiklikleri tekrar listeye yansýtýyoruz.
            _resourceAndCount[i] = tempRc;
        }
    }


    public double GetResorgeCount(Resource resorge)
    {
        foreach (ResourceAndCount<T> rc in _resourceAndCount)
        {
            if(rc.resource == resorge) return(rc.count);
        }

        return 0;
    }

    // Belirtlen türdeki kaynagý  listeden belirtlen miktarda eklemeyi dener.
    public bool AddResorge(Resource resource,double count)
    {
        int index = -1;
     
        foreach (ResourceAndCount<T> rc in _resourceAndCount)
        {
            index ++;
            if (rc.resource == resource)
            {
                break;
            }
        }
        if(index >= 0)
        {
            _resourceAndCount[index].count = count;
            return true;

        }

        return false;
    }

    // Belirtlen türdeki kaynagý  listeden belirtlen miktarda çýkarmayý dener.
    public bool ExtractResource(Resource resource, double count)
    {
        for (int i = 0; i < _resourceAndCount.Count; i++)
        {
            if (_resourceAndCount[i].resource == resource)
            {
                if (_resourceAndCount[i].count >= count)
                {
                    _resourceAndCount[i].count -= count;

                    // Geçici bir deðiþken oluþturup, ref ile metodu çaðýrýyoruz.
                    ResourceAndCount<T> tempRc = _resourceAndCount[i];
                    NumberToAbcNumber(ref tempRc);

                    // Geçici deðiþken üzerinde yapýlan deðiþiklikleri tekrar listeye yansýtýyoruz.
                    _resourceAndCount[i] = tempRc;

                    return true;
                }
                break;
            }
        }
        return false;
    }

}

// kaynak ve mikatrý  içeren sýnýf.
// Neden struct deilde class kulnadým çükü liste içinde refranstipli bir yapý olmasý degrleri direkt degiþtrmeyi kolaylþtýrýyor
[System.Serializable]
public class ResourceAndCount<T> where T : Resource
{
    public T resource;
    public double count;
    public string abcCount;
}

public abstract class BankBase : MonoBehaviour
{
    abstract public Type GetStoredType();

}