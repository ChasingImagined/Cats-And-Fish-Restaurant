using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ChasingImagined
{
    namespace Uitls
    {
        public static class ABCNumbers
        {
            private static char[] _alfbet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
                                      'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            private static char[] _numeric = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.' };


            // abc formatl� sy�y�  normal double sy�ya �evirir
            public static (double, bool) AbcToNumber(string abc)
            {

                double number = 0;
                double number2 = 0;

                //�n eki say�ya d�n��t�rmeyi dene
                bool numcontrol = double.TryParse(RemoveCharacters(abc, _alfbet), out number);
                if (!numcontrol)
                {
                    Debug.Log("number i�in parse hatas�");
                    return (number, false);
                }

                //sadece son abc li eki al
                string lit = RemoveCharacters(abc, _numeric);

                //abcli eki binlik katsy� sy�sna d�n��t�r
                int pow = AbcToPow(lit);


                if (pow > 0)
                {
                   
                    bool numcontrol2 = double.TryParse("1" + new string('0', pow * 3), out number2);

                    if (!numcontrol2)
                    {
                        Debug.Log("number2 i�in parse hatas�");
                        return (number2, false);
                    }

                    return (number2 * number, true);
                }
                else
                {
                    return (number, true);
                }


            }

            //double sy�y�y� abc sy� formt�a d�n��t�r�r
            public static (string, bool) NumberToAbc(double number)
            {
                if (number == Mathf.Infinity) number = double.MaxValue;
                int pow = 0;
                while (number > 1000)
                {
                    if (number == Mathf.Infinity) return ("err", false);
                    number /= 1000;
                    pow++;
                }

                string abc = PowToabc(pow);

                return (ABCNumbers.CelenNumber(number) + abc, true);

            }

            // abc son ekini 3l� basmk sy�s�s� sy�s�n�� a d�n��tr�r 
            public static int AbcToPow(string abc)
            {
                int pow = 0;
                int baseLength = _alfbet.Length;

                for (int i = 0; i < abc.Length; i++)
                {
                    int charValue = Array.IndexOf(_alfbet, abc[i]);
                    if (charValue == -1) // E�er abc i�inde alfabede olmayan bir karakter varsa hata ver
                    {
                        Debug.LogError("Ge�ersiz karakter: " + abc[i]);
                        return -1;
                    }

                    pow = pow * baseLength + (charValue + 1); // +1 ekliyoruz ��nk� 1'den ba�lamal�
                }

                return pow;
            }


            // 3l� basmk sy�s�s� sy�s�n� abc sonekine d�n��t�r�r
            public static string PowToabc(int pow)
            {
                string abc = "";
                int baseLength = _alfbet.Length;

                // basmk sy�s� yok sa bo� ek g�nder
                if (pow <= 0)
                {
                    return "";
                }

                // Basamak hesaplamas�: a->0, b->1 ... z->25
                pow--; // ��nk� "a" asl�nda 1 de�il, 0'dan ba�lamal�

                while (pow >= 0)
                {
                    abc = _alfbet[pow % baseLength] + abc;
                    pow = pow / baseLength - 1;
                }

                return abc;
            }

            // bir stringten verilen birdizideki t�m kakterleri siler
            public static string RemoveCharacters(string input, char[] charactersToRemove)
            {
                foreach (char c in charactersToRemove)
                {
                    input = input.Replace(c.ToString(), string.Empty); // Karakteri sil
                }

                return input;
            }

            public static string CelenNumber(double number)
            {
                return number.ToString("F2").TrimEnd('0').TrimEnd(',');
            }
             
        }
    }
}