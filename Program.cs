using System;
using System.Diagnostics;
using TiVo;

const int size = 1_000_000;

Console.WriteLine("Start!");
var sw = Stopwatch.StartNew();

FileGenerate fg = new FileGenerate(size);
var nf = fg.SaveFile();
FileOrder fo = new FileOrder(nf);
fo.Order(size / 10);

sw.Stop();
Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms.");
