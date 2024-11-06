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


    // ResourceAndCount refrans�n�n i�indeki caount sy�y�n� ABC tipli say�  degrini kulanrk hesplar;
    private void AbcNumberToNumber(ref ResourceAndCount<T> rc)
    {
        (double number, bool numberCheck) = ABCNumbers.AbcToNumber(rc.abcCount);
        if (numberCheck)// Deger ge�erli ise e�itle;
        {
            rc.count = number;
        }
        //���������ii��
        else// degil ise s�f�rla;
        {
            ResetValue(ref rc);
        }
        
    }

    // ResourceAndCount refrans�n�n i�indeki ABCtipli sy�y� count degrini kulanrk hesplar;
    private void NumberToAbcNumber(ref ResourceAndCount<T> rc)
    {
        
        (string abc, bool abcCheck) = ABCNumbers.NumberToAbc(rc.count);
        if (abcCheck) //deger ge�erli ise e�itle
        {
            rc.abcCount = abc;
        }
        else //Degilse s�f�rla.
        {
            ResetValue(ref rc);
        }
        
    }

    // Gelen ResourceAndCount Refrans�nn�n i�indeki degrleri s�f�rlar.
    private void ResetValue( ref ResourceAndCount<T> resorceAndPrice)
    {
        resorceAndPrice.abcCount = "";
        resorceAndPrice.count = 0;
    }

    //t�m AbC tipli miktar� yeniden hesplar.
    private void RecalculateAbcCountAll()
    {
        for (int i = 0;i < _resourceAndCount.Count;i++)
        {
            // Ge�ici bir de�i�ken olu�turup, ref ile metodu �a��r�yoruz.
            ResourceAndCount<T> tempRc = _resourceAndCount[i];
            NumberToAbcNumber(ref tempRc);
            // Ge�ici de�i�ken �zerinde yap�lan de�i�iklikleri tekrar listeye yans�t�yoruz.
            _resourceAndCount[i] = tempRc;
        }
    }

    //t�m  Double tipili miktarlar� yeniden hesplar.
    private void RecalculateCountAll()
    {
        for (int i = 0; i < _resourceAndCount.Count; i++)
        {
            // Ge�ici bir de�i�ken olu�turup, ref ile metodu �a��r�yoruz.
            ResourceAndCount<T> tempRc = _resourceAndCount[i];
            AbcNumberToNumber(ref tempRc);
            // Ge�ici de�i�ken �zerinde yap�lan de�i�iklikleri tekrar listeye yans�t�yoruz.
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

    // Belirtlen t�rdeki kaynag�  listeden belirtlen miktarda eklemeyi dener.
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

    // Belirtlen t�rdeki kaynag�  listeden belirtlen miktarda ��karmay� dener.
    public bool ExtractResource(Resource resource, double count)
    {
        for (int i = 0; i < _resourceAndCount.Count; i++)
        {
            if (_resourceAndCount[i].resource == resource)
            {
                if (_resourceAndCount[i].count >= count)
                {
                    _resourceAndCount[i].count -= count;

                    // Ge�ici bir de�i�ken olu�turup, ref ile metodu �a��r�yoruz.
                    ResourceAndCount<T> tempRc = _resourceAndCount[i];
                    NumberToAbcNumber(ref tempRc);

                    // Ge�ici de�i�ken �zerinde yap�lan de�i�iklikleri tekrar listeye yans�t�yoruz.
                    _resourceAndCount[i] = tempRc;

                    return true;
                }
                break;
            }
        }
        return false;
    }

}

// kaynak ve mikatr�  i�eren s�n�f.
// Neden struct deilde class kulnad�m ��k� liste i�inde refranstipli bir yap� olmas� degrleri direkt degi�trmeyi kolayl�t�r�yor
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