// See https://aka.ms/new-console-template for more information

using System.Data;
using CasinoPokerTable;

int[] arr = { 1, 5, 9, 10, 5 };
var table = new TableEquilibrium(arr);

if (!table.IsEquilibriumAble)
{
    Console.WriteLine("Wrong number of chips, can't equilibrium the table");
}

table.MinEquilibriumMovesCalc();
Console.WriteLine($"Min flip {table.MinEquilibriumMoves}");




