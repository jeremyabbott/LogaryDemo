namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("LogaryDemo")>]
[<assembly: AssemblyProductAttribute("LogaryDemo")>]
[<assembly: AssemblyDescriptionAttribute("Demo of Logary")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
    let [<Literal>] InformationalVersion = "1.0"
