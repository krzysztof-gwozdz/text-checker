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
    |> Array.map (fun file -> (Path.GetFileName file, file, Markdown.Parse(File.ReadAllText(file))))
    |> Array.filter (fun (_, _, content) -> content |> Seq.exists (fun block ->
        match block with
        | :? Markdig.Syntax.HeadingBlock as headingBlock -> 
            headingBlock.Inline.FirstChild.ToString().Contains(learningTag)
        | _ -> false
    ))

AnsiConsole.MarkupLine("[bold]Files[/]")
files |> Array.iter (fun (name, _, _) -> printfn $"{name}")
printSeparator ()

AnsiConsole.MarkupLine("[bold]Headlines[/]")

files
|> Array.iter (fun (name, _, content) ->
    printfn $"{name}"
    content |> Seq.iter (fun block ->
        match block with
        | :? Markdig.Syntax.HeadingBlock as headingBlock -> 
            AnsiConsole.MarkupLine($"  {headingBlock.Inline.FirstChild}")
        | _ -> ()
    )
)