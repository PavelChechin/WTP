using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTPCore.Comparers
{
    public class WTPIndexComparer : IComparer<string>, IComparer
    {

        public static WTPIndexComparer Instance
        {
            get;
            private set;
        }

         static WTPIndexComparer()
        {
            Instance = new WTPIndexComparer();
        }

        public int Compare(object x, object y)
        {
            return Compare((string)x, (string)y);
        }

        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (string.IsNullOrWhiteSpace(x)) return +1;
            if (string.IsNullOrWhiteSpace(y)) return -1;

            int charCnt = x.Length < y.Length ? x.Length : y.Length;
            bool isDigit = false;
            bool lastIsDigit = false;
            int res = 0;
            for (int i = 0; i < charCnt; i++)
            {
                lastIsDigit = false;
                if (res == 0 || !isDigit)
                    res = x[i].CompareTo(y[i]);
                //Если на предыдущей итерации сравнивались числа, то инвертируем результат
                if (char.IsDigit(x, i) && !char.IsDigit(y, i))
                {
                    return isDigit ? 1 : res;
                    return 0;//Символы точно разные (одно число, другое нет).  но до этого сравнивались числа, и на этой итерации одно все еще число. инвертируем и выходим
                }
                else if (!char.IsDigit(x, i) && char.IsDigit(y, i))
                {
                    return isDigit ? -1 : res;
                    return 0;//Символы точно разные (одно число, другое нет).  но до этого сравнивались числа, и на этой итерации одно все еще число. инвертируем и выходим
                }
                else if (char.IsDigit(x, i) && char.IsDigit(y, i))
                {
                    isDigit = true;
                    lastIsDigit = true;
                }
                else
                {
                    isDigit = false;
                }

                if (res != 0 && !isDigit)
                {
                    break;
                }
            }
            //Если добрались до этой строки и все еще равно, то сравниваем через длину
            if (res == 0)
            {
                res = x.Length < y.Length ? -1 : 1;
                isDigit = false;
            }
            if (isDigit)
                if (res > 0 && x.Length < y.Length || res < 0 && x.Length > y.Length)
                    if (!(lastIsDigit && x.Length == y.Length))
                        res *= -1;
            return res;
        }
    }
}
