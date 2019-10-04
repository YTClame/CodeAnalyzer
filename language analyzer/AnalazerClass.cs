using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace language_analyzer
{
    public class AnalazerClass
    {
        private static Errors Error(string ErrorMessage, int countError)
        {
            return new Errors(false, ErrorMessage, countError);
        }
        static int LastSymbolId = 0, FirstSymbolId = 0;
        static string str;
        static int ErrorCountOnIdInNext = 0;
        enum Enums { S, E, F, For1, For2, For3, Space1, Space2, Space3, Space4, Space5, Space6, Space7, Space8, Space9, Space10, Space11, Space12, Id1, Id2, Id3, Id4, Id5, Id6, Id7, EnterMinus1, EnterInt1, EnterMinus2, EnterInt2, EnterMinus3, EnterInt3, EnterMinus4, EnterInt4, To1, To2, Second, Step1, Step2, Step3, Step4, Thrity, Exit1, Exit2, Exit3, Exit4, Exit5, Exit6, Exit7, Next1, Next2, Next3, Next4 };
        public static Errors GoAnalyze(String str2)
        {
            Enums enums = Enums.S;
            str = str2;
            int count = 0;
            enums = Enums.S;
            int firstIDf = -1, firstIDl = -1, secondIDf = -1, secondIDl = -1, stepIDf = -1, stepIDl = -1;
            bool checkStep = false;
            bool checkExitFor = false;
            LinkedList<string> IDS = new LinkedList<string>();
            int countIDS = 0;
            LinkedList<string> CONSTS = new LinkedList<string>();
            int countCONSTS = 0, countError = 0;
            int f1 = -1, f2 = -1, l1 = -1, l2 = -1;
            str = str.ToUpper();
            str = str.TrimEnd(' ');
            string ErrorMessage;
            if (str == "")
            {
                enums = Enums.E;
                ErrorMessage = "Не найдено начальное слово FOR";
                return Error(ErrorMessage, countError);
            }
            else
            {

            }
            if (str.Contains('#')) return Error("Входные данные содержат запрещённый символ \'#\', удалите его.", str.IndexOf('#'));
            str = str + '#';
            while (count< str.Length && enums != Enums.F)
            {
                switch (enums)
                {
                    case Enums.S:
                        if (str[count] == 'F')
                        {
                            enums = Enums.For1;
                        }
                        else
                        {
                            ErrorMessage = "Не найдено начальное слово FOR";
                            enums = Enums.E;
                            countError = count;
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.For1:
                        if (str[count] == 'O')
                        {
                            enums = Enums.For2;
                        }
                        else
                        {
                            ErrorMessage = "Не найдено начальное слово FOR";
                            enums = Enums.E;
                            countError = count;
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.For2:
                        if (str[count] == 'R')
                        {
                            enums = Enums.For3;
                        }
                        else
                        {
                            ErrorMessage = "Не найдено начальное слово FOR";
                            enums = Enums.E;
                            countError = count;
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.For3:
                        if (str[count] == ' ')
                        {
                            enums = Enums.Space1;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Отсутствует пробел после FOR";
                            return Error(ErrorMessage, countError);
                        }
                        break;
                    case Enums.Space1:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (Char.IsLetter(str[count]))
                            {
                                enums = Enums.Id1;
                                FirstSymbolId = count;
                                f1 = count;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался идентификатор, начинающийся с буквы";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;
                    case Enums.Id1:
                        if (Char.IsLetter(str[count]) || Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            //Проверка идентификатора
                            LastSymbolId = count - 1;
                            l1 = LastSymbolId;
                            bool tempCheck = false;
                            if (IDS.Contains(getString(FirstSymbolId, LastSymbolId))) tempCheck = true;

                            if (!tempCheck)
                            {
                                IDS.AddLast(getString(FirstSymbolId, LastSymbolId));
                                countIDS++;
                            }
                            else { }
                            if (!CheckID(count).IsGood)
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckID(count).ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                            else { }
                            if (str[count] == '=')
                            {
                                enums = Enums.Id2;
                            }
                            else
                            {
                                if (str[count] == ' ')
                                {
                                    enums = Enums.Space3;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался знак \'=\' или пробел";
                                    return Error(ErrorMessage, countError);
                                }

                            }


                        }
                        break;

                    case Enums.Space3:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == '=')
                            {
                                enums = Enums.Id2;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался знак \'=\'";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Id2:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {

                            if (str[count] == '-')
                            {
                                FirstSymbolId = count;
                                firstIDf = count;
                                enums = Enums.EnterMinus1;
                            }
                            else
                            {
                                if (Char.IsNumber(str[count]))
                                {
                                    enums = Enums.EnterInt1;
                                    FirstSymbolId = count;
                                    firstIDf = count;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался ввод числа";
                                    return Error(ErrorMessage, countError);
                                }
                            }

                        }
                        break;

                    case Enums.EnterMinus1:
                        if (Char.IsNumber(str[count]))
                        {
                            enums = Enums.EnterInt1;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался ввод числа";
                            return Error(ErrorMessage, countError);
                        }
                        break;
                    case Enums.EnterInt1:
                        if (Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            LastSymbolId = count - 1;
                            firstIDl = LastSymbolId;
                            CONSTS.AddLast(getString(FirstSymbolId, LastSymbolId));
                            countCONSTS++;
                            if (CheckInt().IsGood)
                            {
                                if (str[count] == ' ')
                                {
                                    enums = Enums.Space4;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался пробел";
                                    return Error(ErrorMessage, countError);
                                }
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckInt().ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Space4:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == 'T')
                            {
                                enums = Enums.To1;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался оператор \"TO\"";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.To1:
                        if (str[count] == 'O')
                        {
                            enums = Enums.To2;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"TO\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.To2:
                        if (str[count] == ' ')
                        {
                            enums = Enums.Space5;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался пробел";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Space5:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == '-')
                            {
                                FirstSymbolId = count;
                                secondIDf = count;
                                enums = Enums.EnterMinus2;
                            }
                            else
                            {
                                if (Char.IsNumber(str[count]))
                                {
                                    enums = Enums.EnterInt2;
                                    FirstSymbolId = count;
                                    secondIDf = count;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался ввод числа";
                                    return Error(ErrorMessage, countError);
                                }
                            }
                        }
                        break;

                    case Enums.EnterMinus2:
                        if (Char.IsNumber(str[count]))
                        {
                            enums = Enums.EnterInt2;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался ввод числа";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.EnterInt2:
                        if (Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            LastSymbolId = count - 1;
                            secondIDl = LastSymbolId;
                            CONSTS.AddLast(getString(FirstSymbolId, LastSymbolId));
                            countCONSTS++;
                            if (CheckInt().IsGood)
                            {
                                if (str[count] == ' ')
                                {
                                    enums = Enums.Space6;
                                }
                                else
                                {
                                    if (str[count] == '\r')
                                    {
                                        //////////
                                        while (str[count] == '\r') count++;
                                        /////////
                                        //count++;
                                        if (str[count] == '\n')
                                        {
                                            while (str[count] == '\n') count++;
                                            count--;
                                            enums = Enums.Second;
                                        }
                                        else
                                        {
                                            enums = Enums.E;
                                            countError = count;
                                            ErrorMessage = "Ожидался пробел или переход на новую строку";
                                            return Error(ErrorMessage, countError);
                                        }
                                    }
                                    else
                                    {
                                        enums = Enums.E;
                                        countError = count;
                                        ErrorMessage = "Ожидался пробел или переход на новую строку";
                                        return Error(ErrorMessage, countError);
                                    }

                                }
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckInt().ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Space6:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == 'S')
                            {
                                enums = Enums.Step1;
                            }
                            else
                            {
                                if (str[count] == '\r')
                                {
                                    //////////
                                    while (str[count] == '\r') count++;
                                    /////////
                                    //count++;
                                    if (str[count] == '\n')
                                    {
                                        while (str[count] == '\n') count++;
                                        count--;
                                        enums = Enums.Second;
                                    }
                                    else
                                    {
                                        enums = Enums.E;
                                        countError = count;
                                        ErrorMessage = "Ожидался переход на новую строку или оператор STEP";
                                        return Error(ErrorMessage, countError);
                                    }
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался переход на новую строку или оператор STEP";
                                    return Error(ErrorMessage, countError);
                                }
                            }
                        }
                        break;

                    case Enums.Step1:
                        if (str[count] == 'T')
                        {
                            enums = Enums.Step2;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор STEP";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Step2:
                        if (str[count] == 'E')
                        {
                            enums = Enums.Step3;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор STEP";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Step3:
                        if (str[count] == 'P')
                        {
                            enums = Enums.Step4;
                            checkStep = true;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор STEP";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Step4:
                        if (str[count] == ' ')
                        {
                            enums = Enums.Space7;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался пробел";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Space7:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == '-')
                            {
                                FirstSymbolId = count;
                                stepIDf = count;
                                enums = Enums.EnterMinus3;
                            }
                            else
                            {
                                if (Char.IsNumber(str[count]))
                                {
                                    enums = Enums.EnterInt3;
                                    FirstSymbolId = count;
                                    stepIDf = count;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался ввод числа";
                                    return Error(ErrorMessage, countError);
                                }
                            }
                        }
                        break;
                    case Enums.EnterMinus3:
                        if (Char.IsNumber(str[count]))
                        {
                            enums = Enums.EnterInt3;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался ввод числа";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.EnterInt3:
                        if (Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            LastSymbolId = count - 1;
                            stepIDl = LastSymbolId;
                            CONSTS.AddLast(getString(FirstSymbolId, LastSymbolId));
                            countCONSTS++;
                            if (CheckInt().IsGood)
                            {
                                if (str[count] == ' ')
                                {
                                    enums = Enums.Space8;
                                }
                                else
                                {
                                    if (str[count] == '\r')
                                    {
                                        //////////
                                        while (str[count] == '\r') count++;
                                        /////////
                                        //count++;
                                        if (str[count] == '\n')
                                        {
                                            while (str[count] == '\n') count++;
                                            count--;
                                            enums = Enums.Second;
                                        }
                                        else
                                        {
                                            enums = Enums.E;
                                            countError = count;
                                            ErrorMessage = "Ожидался пробел или переход на новую строку";
                                            return Error(ErrorMessage, countError);
                                        }
                                    }
                                    else
                                    {
                                        enums = Enums.E;
                                        countError = count;
                                        ErrorMessage = "Ожидался пробел или переход на новую строку";
                                        return Error(ErrorMessage, countError);
                                    }

                                }
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckInt().ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Space8:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == '\r')
                            {
                                //////////
                                while (str[count] == '\r') count++;
                                /////////
                                //count++;
                                if (str[count] == '\n')
                                {
                                    while (str[count] == '\n') count++;
                                    count--;
                                    enums = Enums.Second;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался переход на новую строку";
                                    return Error(ErrorMessage, countError);
                                }

                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался переход на новую строку";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Second:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (Char.IsLetter(str[count]))
                            {
                                enums = Enums.Id3;
                                FirstSymbolId = count;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался идентификатор, начинающийся с буквы";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Id3:
                        if (Char.IsLetter(str[count]) || Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            //Проверка идентификатора
                            LastSymbolId = count - 1;
                            bool tempCheck = false;

                            if (IDS.Contains(getString(FirstSymbolId, LastSymbolId))) tempCheck = true;

                            if (!tempCheck)
                            {
                                IDS.AddLast(getString(FirstSymbolId, LastSymbolId));
                                countIDS++;
                            }
                            else { }
                            if (!CheckID(count).IsGood)
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckID(count).ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                            else { }
                            if (str[count] == '=')
                            {
                                enums = Enums.Id4;
                            }
                            else
                            {
                                if (str[count] == ' ')
                                {
                                    enums = Enums.Space9;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался знак \'=\' или пробел";
                                    return Error(ErrorMessage, countError);
                                }

                            }


                        }
                        break;

                    case Enums.Id4:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (Char.IsLetter(str[count]))
                            {
                                enums = Enums.Id5;
                                FirstSymbolId = count;
                            }
                            else
                            {
                                if (str[count] == '-')
                                {
                                    FirstSymbolId = count;
                                    enums = Enums.EnterMinus4;
                                }
                                else
                                {
                                    if (Char.IsNumber(str[count]))
                                    {
                                        enums = Enums.EnterInt4;
                                        FirstSymbolId = count;
                                    }
                                    else
                                    {
                                        enums = Enums.E;
                                        countError = count;
                                        ErrorMessage = "Ожидался идентификатор, начинающийся с буквы или же число";
                                        return Error(ErrorMessage, countError);
                                    }
                                }

                            }
                        }
                        break;

                    case Enums.Space9:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == '=')
                            {
                                enums = Enums.Id4;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался знак \'=\'";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Id5:
                        if (Char.IsLetter(str[count]) || Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            //Проверка идентификатора
                            LastSymbolId = count - 1;
                            bool tempCheck = false;
                            if (IDS.Contains(getString(FirstSymbolId, LastSymbolId))) tempCheck = true;

                            if (!tempCheck)
                            {
                                IDS.AddLast(getString(FirstSymbolId, LastSymbolId));
                                countIDS++;
                            }
                            else { }
                            if (!CheckID(count).IsGood)
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckID(count).ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                            else { }
                            if (str[count] == ' ')
                            {
                                enums = Enums.Space10;
                            }
                            else
                            {
                                if (str[count] == '+' || str[count] == '-' || str[count] == '*' || str[count] == '/')
                                {
                                    enums = Enums.Id4;
                                }
                                else
                                {
                                    if (str[count] == '\r')
                                    {
                                        //////////
                                        while (str[count] == '\r') count++;
                                        /////////
                                        //count++;
                                        if (str[count] == '\n')
                                        {
                                            while (str[count] == '\n') count++;
                                            count--;
                                            enums = Enums.Thrity;
                                        }
                                        else
                                        {
                                            enums = Enums.E;
                                            countError = count;
                                            ErrorMessage = "Ожидался пробел, переход на новую строку или математическая операция";
                                            return Error(ErrorMessage, countError);
                                        }

                                    }
                                    else
                                    {
                                        enums = Enums.E;
                                        countError = count;
                                        ErrorMessage = "Ожидался пробел, переход на новую строку или математическая операция";
                                        return Error(ErrorMessage, countError);
                                    }
                                }


                            }
                        }
                        break;


                    case Enums.Space10:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == '+' || str[count] == '-' || str[count] == '*' || str[count] == '/')
                            {
                                enums = Enums.Id4;
                            }
                            else
                            {
                                if (str[count] == '\r')
                                {
                                    //////////
                                    while (str[count] == '\r') count++;
                                    /////////
                                    //count++;
                                    if (str[count] == '\n')
                                    {
                                        while (str[count] == '\n') count++;
                                        count--;
                                        enums = Enums.Thrity;
                                    }
                                    else
                                    {
                                        enums = Enums.E;
                                        countError = count;
                                        ErrorMessage = "Ожидалась математическая операция или переход на новую строку";
                                        return Error(ErrorMessage, countError);
                                    }
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидалась математическая операция или переход на новую строку";
                                    return Error(ErrorMessage, countError);
                                }
                            }

                        }
                        break;

                    case Enums.EnterInt4:
                        if (Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            LastSymbolId = count - 1;
                            CONSTS.AddLast(getString(FirstSymbolId, LastSymbolId));
                            countCONSTS++;
                            if (CheckInt().IsGood)
                            {
                                if (str[count] == ' ')
                                {
                                    enums = Enums.Space10;
                                }
                                else
                                {
                                    if (str[count] == '+' || str[count] == '-' || str[count] == '*' || str[count] == '/')
                                    {
                                        enums = Enums.Id4;
                                    }
                                    else
                                    {
                                        if (str[count] == '\r')
                                        {
                                            //////////
                                            while (str[count] == '\r') count++;
                                            /////////
                                            //count++;
                                            if (str[count] == '\n')
                                            {
                                                while (str[count] == '\n') count++;
                                                count--;
                                                enums = Enums.Thrity;
                                            }
                                            else
                                            {
                                                enums = Enums.E;
                                                countError = count;
                                                ErrorMessage = "Ожидался пробел, математическая операция или переход на новую строку";
                                                return Error(ErrorMessage, countError);
                                            }
                                        }
                                        else
                                        {
                                            enums = Enums.E;
                                            countError = count;
                                            ErrorMessage = "Ожидался пробел, математическая операция или переход на новую строку";
                                            return Error(ErrorMessage, countError);
                                        }
                                    }


                                }
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckInt().ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.EnterMinus4:
                        if (Char.IsNumber(str[count]))
                        {
                            enums = Enums.EnterInt4;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался ввод числа";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Thrity:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == 'E')
                            {
                                enums = Enums.Exit1;
                            }
                            else
                            {
                                if (str[count] == 'N')
                                {
                                    enums = Enums.Next1;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался оператор \"EXIT FOR\" или \"NEXT\"";
                                    return Error(ErrorMessage, countError);
                                }
                            }
                        }
                        break;

                    case Enums.Next1:
                        if (str[count] == 'E')
                        {
                            enums = Enums.Next2;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"NEXT\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Next2:
                        if (str[count] == 'X')
                        {
                            enums = Enums.Next3;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"NEXT\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Next3:
                        if (str[count] == 'T')
                        {
                            enums = Enums.Next4;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"NEXT\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Next4:
                        if (str[count] == ' ')
                        {
                            enums = Enums.Space12;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался пробел";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Space12:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (Char.IsLetter(str[count]))
                            {
                                enums = Enums.Id6;
                                FirstSymbolId = count;
                                f2 = count;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался идентификатор, начинающийся с буквы";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Id6:
                        if (Char.IsLetter(str[count]) || Char.IsNumber(str[count]))
                        {
                            //count++;
                        }
                        else
                        {
                            //Проверка идентификатора
                            LastSymbolId = count - 1;
                            str = str.TrimEnd(' ');
                            //char[] strchars = str.ToCharArray();
                            //strchars[count] = '#';
                            //str = new string(strchars);
                            ErrorCountOnIdInNext = count;
                            bool tempCheck = false;


                            if (IDS.Contains(getString(FirstSymbolId, LastSymbolId))) tempCheck = true;

                            if (!tempCheck)
                            {
                                IDS.AddLast(getString(FirstSymbolId, LastSymbolId));
                                countIDS++;
                            }
                            else { }
                            l2 = LastSymbolId;
                            if (!CheckID(count).IsGood)
                            {
                                enums = Enums.E;
                                countError = count - 1;
                                ErrorMessage = CheckID(count).ErrorMessage;
                                return Error(ErrorMessage, countError);
                            }
                            else
                            {
                                enums = Enums.F;
                            }
                        }
                        break;

                    case Enums.Exit1:
                        if (str[count] == 'X')
                        {
                            enums = Enums.Exit2;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"EXIT FOR\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Exit2:
                        if (str[count] == 'I')
                        {
                            enums = Enums.Exit3;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"EXIT FOR\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Exit3:
                        if (str[count] == 'T')
                        {
                            enums = Enums.Space11;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"EXIT FOR\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Space11:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == 'F')
                            {
                                enums = Enums.Exit4;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался оператор \"EXIT FOR\"";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;

                    case Enums.Exit4:
                        if (str[count] == 'O')
                        {
                            enums = Enums.Exit5;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"EXIT FOR\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Exit5:
                        if (str[count] == 'R')
                        {
                            enums = Enums.Exit6;
                            checkExitFor = true;
                        }
                        else
                        {
                            enums = Enums.E;
                            countError = count;
                            ErrorMessage = "Ожидался оператор \"EXIT FOR\"";
                            return Error(ErrorMessage, countError);
                        }
                        break;

                    case Enums.Exit6:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == '\r')
                            {
                                //////////
                                while (str[count] == '\r') count++;
                                /////////
                                //count++;
                                if (str[count] == '\n')
                                {
                                    while (str[count] == '\n') count++;
                                    count--;
                                    enums = Enums.Exit7;
                                }
                                else
                                {
                                    enums = Enums.E;
                                    countError = count;
                                    ErrorMessage = "Ожидался переход на новую строку";
                                    return Error(ErrorMessage, countError);
                                }
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался переход на новую строку";
                                return Error(ErrorMessage, countError);
                            }

                        }
                        break;

                    case Enums.Exit7:
                        if (str[count] == ' ')
                        {
                            //count++;
                        }
                        else
                        {
                            if (str[count] == 'N')
                            {
                                enums = Enums.Next1;
                            }
                            else
                            {
                                enums = Enums.E;
                                countError = count;
                                ErrorMessage = "Ожидался оператор NEXT";
                                return Error(ErrorMessage, countError);
                            }
                        }
                        break;
                }
                count++;
            }
            count--;
            while (true)
            {
                if (enums == Enums.F)
                {
                    if (str[count] == ' ' || str[count] == '\n' || str[count] == '\r')
                    {
                        count++;
                    }
                    else
                    {
                        if(str[count] == '#')
                        {
                            int temp1 = getInt(firstIDf, firstIDl);
                            int temp2 = getInt(secondIDf, secondIDl);
                            int loop = 0;
                            if (checkStep)
                            {
                                int temp3 = getInt(stepIDf, stepIDl);
                                loop = loops(temp1, temp2, temp3);
                            }
                            else
                            {
                                loop = loops(temp1, temp2);
                            }
                            if (checkExitFor)
                            {
                                loop = 1;
                            }
                            else { }



                            if (IsEqualId(f1, l1, f2, l2))
                            {
                                return new Errors(true, IDS, CONSTS, loop);
                            }
                            else
                            {
                                ErrorMessage = "Имена идентификаторов после \"FOR\" и после \"NEXT\" не совпадают";
                                return Error(ErrorMessage, ErrorCountOnIdInNext);
                            }
                        }
                        else
                        {
                            return Error("Цепочка имеет недопустимое продолжение, которое необходимо удалить (после каретки)", count);
                        }
                        
                    }

                }
                else
                {
                    return new Errors(false, "Цепочка не принадлежит языку", count);
                }
            }
            
            

        }


        private static Errors CheckID(int count)
        {
            int length = LastSymbolId - FirstSymbolId + 1;
            if (length > 8) return new Errors(false, "Длина идентификатора должна быть не более 8 символов", count);
            string s = str.Substring(FirstSymbolId, length);
            if (String.Equals(s, "FOR") || String.Equals(s, "TO") || String.Equals(s, "STEP") || String.Equals(s, "EXIT") || String.Equals(s, "NEXT")) return new Errors(false, "Наименование идентификатора совпадает с зарезервированным словом");
            return new Errors(true);
        }
        private static Errors CheckInt()
        {
            int length = LastSymbolId - FirstSymbolId + 1;
            string s = str.Substring(FirstSymbolId, length);
            int a;
            try
            {
                a = Convert.ToInt32(s);
            }
            catch
            {
                return new Errors(false, "Ошибка ввода числа");
            }
            if (a >= -32768 && a <= 32767)
            {
                return new Errors(true);
            }
            else
            {
                return new Errors(false, "Число выходит за допустимые пределы");
            }
        }

        private static bool IsEqualId(int f1, int l1, int f2, int l2)
        {
            int length1 = l1 - f1 + 1;
            int length2 = l2 - f2 + 1;
            if (length1 != length2) return false;
            for (int i = f1, j = f2, k = 0; k < length1; i++, j++, k++)
            {
                if (str[i] != str[j]) return false;
            }
            return true;
        }


        private static int getInt(int f, int l)
        {
            int length = l - f + 1;
            string s = str.Substring(f, length);
            int a;
            a = Convert.ToInt32(s);
            return a;
        }
        private static string getString(int f, int l)
        {
            int length = l - f + 1;
            string s = str.Substring(f, length);
            return s;
        }
        private static int loops(int lower, int higher, int step)
        {
            int count = 0;
            if (step > 0)
            {
                if (lower <= higher)
                {
                    for (int i = lower; i <= higher; i += step) count++;
                    return count;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (lower >= higher)
                {
                    for (int i = lower; i >= higher; i += step) count++;
                    return count;
                }
                else
                {
                    return -1;
                }
            }
        }
        private static int loops(int lower, int higher)
        {
            int count = 0;
            if (lower <= higher)
            {
                for (int i = lower; i <= higher; i++) count++;
                return count;
            }
            else
            {
                return -1;
            }

        }
    }
    public class Errors
    {
        public bool IsGood;
        public string ErrorMessage;
        public int countError;
        public LinkedList<string> IDS;
        public LinkedList<string> CONSTS;
        public int loops;
        public Errors(bool b, string s, int c)
        {
            IsGood = b;
            ErrorMessage = s;
            countError = c;
        }
        public Errors(bool b, LinkedList<string> id, LinkedList<string> con, int a)
        {
            IsGood = b;
            IDS = id;
            CONSTS = con;
            loops = a;
        }
        public Errors(bool b, string s)
        {
            IsGood = b;
            ErrorMessage = s;
        }
        public Errors(bool b)
        {
            IsGood = b;
        }

    }
}
