using System.Diagnostics;
using TiVo;

Console.WriteLine("Start!");
var sw = Stopwatch.StartNew();

FileGenerate fg = new FileGenerate(100_000);
var nf = fg.SaveFile();
FileOrder fo = new FileOrder(nf);
fo.Order(10_000);

sw.Stop();
Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms.");
