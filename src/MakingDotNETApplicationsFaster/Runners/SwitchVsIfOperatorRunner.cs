using System;
using BenchmarkDotNet.Attributes;

namespace MakingDotNETApplicationsFaster.Runners
{
    public class SwitchVsIfOperatorsRunner
    {
        [Params(1, 2, 3, 4, 5, 7, 10, 13, 15, 17, 20, 21)]
        public int Input;

        //Method is just for small delay during argument's comparison
        private int InputRecalculate(int input)
        {
            return (int)Math.Round(input/1.0);
        }

        [Benchmark]
        public int IfWithLongCycleOrOperator()
        {
            int result = 0;

            if (InputRecalculate(Input) == 1 |
                InputRecalculate(Input) == 2 |
                InputRecalculate(Input) == 3 |
                InputRecalculate(Input) == 4 |
                InputRecalculate(Input) == 5 |
                InputRecalculate(Input) == 6 |
                InputRecalculate(Input) == 7 |
                InputRecalculate(Input) == 8 |
                InputRecalculate(Input) == 9 |
                InputRecalculate(Input) == 10 |
                InputRecalculate(Input) == 11 |
                InputRecalculate(Input) == 12 |
                InputRecalculate(Input) == 13 |
                InputRecalculate(Input) == 14 |
                InputRecalculate(Input) == 15 |
                InputRecalculate(Input) == 16 |
                InputRecalculate(Input) == 17 |
                InputRecalculate(Input) == 18 |
                InputRecalculate(Input) == 19 |
                InputRecalculate(Input) == 20)
            {
                result = Input;
            }

            return result;
        }

        [Benchmark]
        public int IfWithShortCycleOrOperator()
        {
            int result = 0;

            if (InputRecalculate(Input) == 1 ||
                InputRecalculate(Input) == 2 ||
                InputRecalculate(Input) == 3 ||
                InputRecalculate(Input) == 4 ||
                InputRecalculate(Input) == 5 ||
                InputRecalculate(Input) == 6 ||
                InputRecalculate(Input) == 7 ||
                InputRecalculate(Input) == 8 ||
                InputRecalculate(Input) == 9 ||
                InputRecalculate(Input) == 10 ||
                InputRecalculate(Input) == 11 ||
                InputRecalculate(Input) == 12 ||
                InputRecalculate(Input) == 13 ||
                InputRecalculate(Input) == 14 ||
                InputRecalculate(Input) == 15 ||
                InputRecalculate(Input) == 16 ||
                InputRecalculate(Input) == 17 ||
                InputRecalculate(Input) == 18 ||
                InputRecalculate(Input) == 19 ||
                InputRecalculate(Input) == 20)
            {
                result = Input;
            }

            return result;
        }

        [Benchmark]
        public int IfElseOperator()
        {
            int result;

            if (InputRecalculate(Input) == 1)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 2)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 3)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 4)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 5)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 6)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 7)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 8)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 9)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 10)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 11)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 12)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 13)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 14)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 15)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 16)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 17)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 18)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 19)
            {
                result = Input;
            }
            else if (InputRecalculate(Input) == 20)
            {
                result = Input;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        [Benchmark]
        public int SwitchOperator()
        {
            int result;

            switch (InputRecalculate(Input))
            {
                case 1:
                    result = Input;
                    break;
                case 2:
                    result = Input;
                    break;
                case 3:
                    result = Input;
                    break;
                case 4:
                    result = Input;
                    break;
                case 5:
                    result = Input;
                    break;
                case 6:
                    result = Input;
                    break;
                case 7:
                    result = Input;
                    break;
                case 8:
                    result = Input;
                    break;
                case 9:
                    result = Input;
                    break;
                case 10:
                    result = Input;
                    break;
                case 11:
                    result = Input;
                    break;
                case 12:
                    result = Input;
                    break;
                case 13:
                    result = Input;
                    break;
                case 14:
                    result = Input;
                    break;
                case 15:
                    result = Input;
                    break;
                case 16:
                    result = Input;
                    break;
                case 17:
                    result = Input;
                    break;
                case 18:
                    result = Input;
                    break;
                case 19:
                    result = Input;
                    break;
                case 20:
                    result = Input;
                    break;
                default:
                    result = 0;
                    break;
            }

            return result;
        }
    }
}
