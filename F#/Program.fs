open System
open System.IO
open Markdig
open Spectre.Console

let dataDirectoryPath = @"..\\..\\..\\..\\test_data"
let learningTag = "#ripit";

let printSeparator () =
    printfn $"{Environment.NewLine}--- --- --- --- ---{Environment.NewLine}"

AnsiConsole.Write(FigletText("F#"))

let files =
    Directory.GetFiles(dataDirectoryPath, "*.md", SearchOption.TopDirectoryOnly)
    |> Array.map (fun file -> 
        let fileName = Path.GetFileName(file)
        let filePath = file
        let fileContent = File.ReadAllText(file)
        let parsedMarkdown = Markdown.Parse(fileContent)
        
        let headlines = 
            parsedMarkdown
            |> Seq.filter (fun block -> block.GetType() = typeof<Markdig.Syntax.HeadingBlock>)
            |> Seq.cast<Markdig.Syntax.HeadingBlock>
            |> Seq.map (fun headingBlock -> headingBlock.Inline.FirstChild.ToString())
            |> Seq.filter (fun heading -> heading.Contains(learningTag))
            |> Seq.map (fun heading -> heading.Replace($" {learningTag}", String.Empty))
            |> Seq.toList
        
        fileName, filePath, headlines
    )
    |> Array.filter (fun (_, _, headlines) -> headlines |> List.isEmpty |> not)

AnsiConsole.MarkupLine("[bold]Files[/]")
files |> Array.iter (fun (name, _, _) -> printfn $"{name}")
printSeparator ()

AnsiConsole.MarkupLine("[bold]Headlines[/]")

files |> Array.iter (fun (name, _, headlines) ->
    printfn $"{name}"
)