using System;
using System.Diagnostics;
using TiVo;

const int size = 1_000_000;

Console.WriteLine("Start!");
var sw = Stopwatch.StartNew();

FileGenerate fg = new(size);
var nf = fg.SaveFile();
FileOrder fo = new(nf);
var orderedFile = fo.Order(size / 10);

sw.Stop();
Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms.");
Console.WriteLine($"Check={FileOrder.Check(orderedFile)}.");
