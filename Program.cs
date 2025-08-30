using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using TiVo;

string fn, fno;

if (args.Length == 2)
{
    fn = args[0];
    fno = args[1];
}
else
{
    fn = FileGenerate.MakeFile(45_000_000);
    fno = "out.txt";
}

using var writer = new StreamWriter(fno);
using var reader = new StreamReader(fn);
var sw = Stopwatch.StartNew();
FileOrder.Order(500_000, writer, reader);
Console.WriteLine("Elapsed Minutes: {0}", sw.Elapsed.TotalMinutes);