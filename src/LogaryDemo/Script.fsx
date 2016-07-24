#I @"../../packages/"
#r "Logary/lib/net40/Logary.dll"
#r "Logary.Adapters.Suave/lib/net40/Logary.Adapters.Suave"
#r "Logary.WinPerfCounters/lib/net40/Logary.WinPerfCounters.dll"
#r "Hopac/lib/net45/Hopac.dll"
#r "Hopac/lib/net45/Hopac.Core.dll"
#r "Hopac/lib/net45/Hopac.Platform.dll"
#r "NodaTime/lib/net35-client/NodaTime.dll"
#r "Suave/lib/net40/Suave.dll"

open System
open System.IO
open NodaTime
open Hopac.Core
open Logary
open Logary.Message
open Logary.Logging
open Logary.Configuration
open Logary.Targets
open Logary.Metric
open Logary.Metrics
open Logary.Metrics.WinPerfCounter

#if INTERACTIVE
let path = __SOURCE_DIRECTORY__
#else
let path = System.Reflection.Assembly.GetExecutingAssembly().Location |> Path.GetDirectoryName
[<EntryPoint>]
#endif
let main argv =
  use logary =
    withLogaryManager "TextWriter.Example" (
      withTargets [
        Logary.Targets.TextWriter.create(
            let textConf = 
                TextWriter.TextWriterConf.create(
                    Path.Combine(path, DateTime.UtcNow.ToString("yyyy-MM") + "-happy.log") |> File.AppendText, 
                    Path.Combine(path, DateTime.UtcNow.ToString("yyyy-MM") + "-sad.log") |> File.AppendText)
            let newConf = { textConf with flush = true }
            newConf
        ) (PointName.ofSingle "filelogger")
      ] >>
      withRules [
        Rule.createForTarget (PointName.ofSingle "filelogger")
      ]
    ) |> Hopac.TopLevel.run

  Console.ReadKey true |> ignore
  0