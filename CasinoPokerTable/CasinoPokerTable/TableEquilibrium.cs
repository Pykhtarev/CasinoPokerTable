using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void MinEquilibriumMovesCalc()
        {
            if (IsEquilibriumAble)
            {
                int current = 0;
                int shift = 1;
                int rightIter = current + shift;
                bool[] isEquilibrium = new bool[_chipsSet.Length];
                while (isEquilibrium.Contains(false))
                {

                    if ((_chipsSet[current] < referChip && _chipsSet[rightIter] > referChip) ||
                        (_chipsSet[current] > referChip && _chipsSet[rightIter] < referChip))
                    {
                        var indexOfMin = _chipsSet[current] < _chipsSet[rightIter] ? current : rightIter;
                        var indexOfMax = _chipsSet[current] > _chipsSet[rightIter] ? current : rightIter;
                        var needToMove = 0;
                        if (referChip - _chipsSet[indexOfMin] >= _chipsSet[indexOfMax] - referChip)
                        {
                            needToMove = _chipsSet[indexOfMax] - referChip;
                        }
                        else
                        {
                            needToMove = referChip - _chipsSet[indexOfMin];
                        }

                        _chipsSet[indexOfMax] -= needToMove;
                        _chipsSet[indexOfMin] += needToMove;

                        var distLeft = Math.Max(rightIter, current) - Math.Min(current, rightIter);
                        var distRight = _chipsSet.Length - Math.Max(rightIter, current) + Math.Min(current, rightIter);
                        MinEquilibriumMoves += Math.Min(distLeft, distRight) * needToMove;
                    }

                    if (_chipsSet[current]==referChip)
                    {
                        isEquilibrium[current] = true;
                    }

                    if (current + shift > _chipsSet.Length-1)
                    {
                        current = 0;
                        if (shift>_chipsSet.Length)
                        {
                            shift = 0;
                        }
                        ++shift;
                    }
                    else
                    {
                        ++current;
                    }

                    rightIter = (current + shift) % _chipsSet.Length;

                }

               
            }
        }
        [Obsolete("This method is obsolete I left it just in case. Call MinEquilibriumMovesCalc instead.", false)]
        public  void ObsoleteMinEquilibriumMovesCalc()
      {
          if (IsEquilibriumAble)
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

                if (chipsLeft != 0 && leftIter < current)
                {
                    _chipsSet[current] -= chipsLeft;
                    _chipsSet[leftIter] += chipsLeft;
                    MinEquilibriumMoves += chipsLeft * distance;
                    continue;
                }

                if (chipsRight != 0)
                {
                    _chipsSet[current] -= chipsRight;
                    _chipsSet[rightIter] += chipsRight;
                    MinEquilibriumMoves += chipsRight * distance;
                    continue;
                }
                distance++;
                rightIter++;
                leftIter--;
            }
          }

        }


    }
}
