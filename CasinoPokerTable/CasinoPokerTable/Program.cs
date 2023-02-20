// See https://aka.ms/new-console-template for more information

using System.Data;
using CasinoPokerTable;

int[] arr = { 6, 2, 4, 10, 3 };
var table = new TableEquilibrium(arr);

if (!table.IsEquilibriumAble)
{
    Console.WriteLine("Wrong number of chips, can't equilibrium the table");
}
else
{
    table.MinEquilibriumMovesCalc();
    Console.WriteLine($"Min flip {table.MinEquilibriumMoves}");

}




