using Markdig.Syntax;
using Spectre.Console;

const string dataDirectoryPath = @"..\\..\\..\\..\\test_data";
const string learningTag = "#ripit";

AnsiConsole.Write(new FigletText("C#").Color(Color.Green1));

var files =
    Directory.GetFiles(dataDirectoryPath, "*.md", SearchOption.TopDirectoryOnly)
        .Select(file =>
            {
                var content = File.ReadAllText(file);
                var headings = Markdig.Markdown.Parse(content)
                    .OfType<HeadingBlock>()
                    .Select(headingBlock => headingBlock.Inline?.FirstChild.ToString());
                return (
                    Name: Path.GetFileName(file),
                    Path: file,
                    Headlines: headings
                        .Where(heading => heading.Contains(learningTag))
                        .Select(heading => heading.Replace($" {learningTag}", string.Empty))
                        .ToList()
                );
            }
        )
        .Where(file => file.Headlines.Any())
        .ToList();

AnsiConsole.MarkupLine("[bold]Files[/]");
files.ForEach(file => Console.WriteLine(file.Name));
PrintSeparator();

AnsiConsole.MarkupLine("[bold]Headlines[/]");
files.ForEach(file =>
{
    Console.WriteLine(file.Name);
    file.Headlines.ForEach(headingBlock => Console.WriteLine($"  {headingBlock}"));
});

return;

void PrintSeparator() =>
    Console.WriteLine($"{Environment.NewLine}--- --- --- --- ---{Environment.NewLine}");