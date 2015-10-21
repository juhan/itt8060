(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 7: Interaction with .Net libraries, Charting, behaviour centric programming
  
  Juhan Ernits

*)

List.map
Option.map

List.fold
Option.fold

List.filter
Option.filter

List.fold (+) 0 [1;2;3]

Option.fold (+) 1 (Some 1)
Option.fold (+) 1 None

(+)
List.fold (fun x y -> x+y) 0 [1;2;3]

Option.bind
List.collect

open System.IO

let directories = 
   [ "c:\Windows";
     "c:\Program files" ]

directories |> List.collect (fun d ->
     d |> Directory.GetFiles
       |> List.ofSeq
       |> List.map Path.GetFileName )

directories |> List.collect (fun d ->
     d |> Directory.GetFiles 
       |> List.ofSeq )

List.ofSeq [|1;2;3|]
List.ofArray [|1;2;3|]

#r @"..\packages\FSharp.Charting.0.90.12\lib\net40\FSharp.Charting.dll"

open FSharp.Charting
open FSharp.Charting.ChartTypes

Chart.Line([for x in 0 .. 10 -> x, x * x]).ShowChart()

//alternative way to load the library
#load @"..\packages\FSharp.Charting.0.90.12\FSharp.Charting.fsx"

let rnd = System.Random()

let rand() = rnd.NextDouble()

let randomPoints = [for i in 0 .. 1000 -> 10.0 * rand() , 10.0 * rand()]

Chart.Point(randomPoints)

let randomTrend1 = [for i in 0.0 .. 0.1 .. 10.0 -> i, sin i + rand()]
let randomTrend2 = [for i in 0.0 .. 0.1 .. 10.0 -> i, sin i + rand()]

Chart.Combine [Chart.Line (randomTrend1, Title = "Awesome chart") ; Chart.Point randomTrend2]


// Records and behaviour centric programming

type Rect = 
  {
     Left   : float32
     Top    : float32
     Width  : float32
     Height : float32
  }

let rc = {Left = 10.0f; Top = 10.0f ; Width = 100.0f ; Height = 200.0f }

rc.Left + rc.Width

let rc2awk = { Left = rc.Left + 100.0f; Top = rc.Top;
               Width = rc.Width; Height = rc.Height }

let rc2 = { rc with Left = rc.Left + 100.0f}

type Client = 
  { Name : string; Income : int ; YearsInJob : int
    UsesCreditCard : bool;  CriminalRecord : bool }

let john = {Name = "John Doe"; Income = 40000 ; YearsInJob = 1 ; 
            UsesCreditCard = true ; CriminalRecord = false }

let tests =
 [ (fun cl -> cl.CriminalRecord = true) ;
   (fun cl -> cl.Income < 30000) ;
   (fun cl -> cl.UsesCreditCard = false) ;
   (fun cl -> cl.YearsInJob < 2)
 ]

let testClient client = 
   let issues = tests |> List.filter (fun f -> f client)
   let suitable = issues.Length<=1
   printfn "Client: %s\n Offer a loan %s (issues= %d)" client.Name
       (if suitable then "Yes" else "No") issues.Length

testClient john





































