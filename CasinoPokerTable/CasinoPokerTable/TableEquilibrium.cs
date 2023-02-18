using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoPokerTable
{
    internal class TableEquilibrium
    {
        private int[] _chipsSet;
        internal int MinEquilibriumMoves { get; private set; }

        internal bool IsEquilibriumAble { get; private set; }

        private readonly int referChip;




        internal TableEquilibrium(int[] chipsSet)
        {
            _chipsSet = chipsSet;

            CheckEquilibriumAble();

            if (IsEquilibriumAble)
            {
                referChip = (int)_chipsSet.Average();
            }
        }

        void CheckEquilibriumAble()
        {
            IsEquilibriumAble = _chipsSet.Average() % 1 == 0&& _chipsSet.Min()>=0;
            
        }

      public  void MinEquilibriumMovesCalc()
      {
            int current = 0;
            int leftIter = current - 1;
            int rightIter = current + 1;
            int distance = 1;
            int chipsRight;
            int chipsLeft;
            while (current < _chipsSet.Length)
            {
                chipsRight = 0;
                chipsLeft = 0;
                if (_chipsSet[current] <= referChip)
                {
                    current++;
                    leftIter = current - 1;
                    rightIter =current + 1;
                    distance = 1;
                    continue;
                }
                
                if (rightIter == _chipsSet.Length)
                {
                    rightIter = 0;
                }

                if (_chipsSet[rightIter] < referChip)
                {
                    
                    if (_chipsSet[current] - referChip < referChip - _chipsSet[rightIter])
                    {
                         chipsRight = _chipsSet[current] - referChip;
                    }
                    else
                    {
                        chipsRight = referChip - _chipsSet[rightIter];
                    }
                   
                }
                if (leftIter < 0)
                {
                    leftIter = _chipsSet.Length-1;
                }
                if (_chipsSet[leftIter] < referChip)
                {
                    if (_chipsSet[current] - referChip < referChip - _chipsSet[leftIter])
                    {
                        chipsLeft = _chipsSet[current] - referChip;
                    }
                    else
                    {
                        chipsLeft = referChip - _chipsSet[leftIter];
                    }
                   
                }

                if (chipsRight != 0 && chipsLeft==0)
                {
                    _chipsSet[current] -= chipsRight;
                    _chipsSet[rightIter] += chipsRight;
                    MinEquilibriumMoves += chipsRight * distance;
                    continue;
                }

                if (chipsLeft!= 0)
                {
                    _chipsSet[current] -= chipsLeft;
                    _chipsSet[leftIter] += chipsLeft;

                    MinEquilibriumMoves += chipsLeft * distance;
                    continue;
                }
                distance++;
                rightIter++;
                leftIter--;

            }

        }


    }
}
