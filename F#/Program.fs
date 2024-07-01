open System
open System.IO
open Spectre.Console

let dataDirectoryPath = @"..\\..\\..\\..\\test_data"

let printSeparator () =
    printfn $"{Environment.NewLine}--- --- --- --- ---{Environment.NewLine}"

AnsiConsole.Write(FigletText("F#"))

let files =
    Directory.GetFiles(dataDirectoryPath, "*.md", SearchOption.TopDirectoryOnly)
    |> Array.map (fun file -> (Path.GetFileName file, file))

AnsiConsole.MarkupLine("[bold]Files[/]")
files |> Array.iter (fun (name, _) -> printfn $"%s{name}")
printSeparator ()

