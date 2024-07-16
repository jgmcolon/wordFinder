




using wordFinder;
using wordFinder.helpers;



Console.WriteLine(WriteConsole.centerText("Word Finder"));
Console.WriteLine(WriteConsole.centerText("Jose Mata"));

List<string> matrixData = [
    "a b c d c a",
    "i g w i o b",
    "c h i l l c",
    "p q n s d e",
    "u v d x y f",
    "l l i h c d",
];

List<string> wordsToFind= ["cold","wind","snow","chill"];

try
{
    //Print Matrix
    Console.WriteLine(WriteConsole.NewLine('-'));
    Console.WriteLine(WriteConsole.centerText("***MATRIX***"));
    Console.WriteLine(WriteConsole.NewLine('-'));
    foreach (string row in matrixData)
    {
        Console.WriteLine(WriteConsole.centerText(row));
    }

    //Intance WordFinder and check rules;
    WordFinder wf = new WordFinder(matrixData);

    IEnumerable<string> result = wf.Find(wordsToFind);

    Console.WriteLine(WriteConsole.NewLine('-'));
    Console.WriteLine(WriteConsole.centerText("***RESULT***"));
    Console.WriteLine(WriteConsole.NewLine('-'));
    foreach (string word in result)
    {
        Console.WriteLine($"*{word}");
    }


}
catch (Exception ex) {
    Console.WriteLine(WriteConsole.NewLine('*'));
    Console.WriteLine(ex.Message);
    Console.WriteLine(WriteConsole.NewLine('*'));
}

