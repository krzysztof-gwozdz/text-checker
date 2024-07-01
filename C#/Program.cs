using Markdig.Syntax;
using Spectre.Console;

const string dataDirectoryPath = @"..\\..\\..\\..\\test_data";
const string learningTag = "#ripit";

AnsiConsole.Write(new FigletText("C#").Color(Color.Green1));

var files =
    Directory.GetFiles(dataDirectoryPath, "*.md", SearchOption.TopDirectoryOnly)
        .Select(file => (
                Name: Path.GetFileName(file),
                Path: file,
                Content: Markdig.Markdown.Parse(File.ReadAllText(file))
            )
        )
        .Where(file => file.Content
            .Any(block => block is HeadingBlock headingBlock && (headingBlock.Inline?.FirstChild.ToString().Contains(learningTag) ?? false)))
        .ToList();

AnsiConsole.MarkupLine("[bold]Files[/]");
files.ForEach(file => Console.WriteLine(file.Name));
PrintSeparator();

AnsiConsole.MarkupLine("[bold]Headlines[/]");
files.ForEach(file =>
{
    Console.WriteLine(file.Name);
    file.Content.Where(block => block is HeadingBlock).Cast<HeadingBlock>().ToList().ForEach(headingBlock => Console.WriteLine($"  {headingBlock.Inline?.FirstChild.ToString()?.Replace($" {learningTag}", string.Empty)}"));
});

return;

void PrintSeparator() =>
    Console.WriteLine($"{Environment.NewLine}--- --- --- --- ---{Environment.NewLine}");