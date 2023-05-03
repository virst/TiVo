using System;
using System.Diagnostics;
using TiVo;

int size = 1_000_000;
int split = 10;
if (args.Length > 0)
    size = Convert.ToInt32(args[0]);
if (args.Length > 1)
    split = Convert.ToInt32(args[1]);

Console.WriteLine("Start!");
var sw = Stopwatch.StartNew();

FileGenerate fg = new(size);
var nf = fg.SaveFile();
Console.WriteLine($"FileGenerate Elapsed: {sw.ElapsedMilliseconds} ms.");
FileOrder fo = new(nf);
var orderedFile = fo.Order(size / split);

sw.Stop();
Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms.");
Console.WriteLine($"Check={FileOrder.Check(orderedFile)}.");
