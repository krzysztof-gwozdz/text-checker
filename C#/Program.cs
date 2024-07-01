using Spectre.Console;

const string dataDirectoryPath = @"..\\..\\..\\..\\test_data";

AnsiConsole.Write(new FigletText("C#").Color(Color.Green1));

var files =
    Directory.GetFiles(dataDirectoryPath, "*.md", SearchOption.TopDirectoryOnly)
        .Select(file => (
                Name: Path.GetFileName(file),
                Path: file
            )
        )
        .ToList();

AnsiConsole.MarkupLine("[bold]Files[/]");
files.ForEach(file => Console.WriteLine(file.Name));
PrintSeparator();

return;

void PrintSeparator() =>
    Console.WriteLine($"{Environment.NewLine}--- --- --- --- ---{Environment.NewLine}");