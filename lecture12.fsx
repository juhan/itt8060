(*

   Lecture Nov 25, 2015: Sequences and computation expressions

   Based on Chapter 12 of RWFP (available for free online:
   https://manning-content.s3.amazonaws.com/download/0/9e61459-5d6a-43a3-a407-d73a9112ba77/SampleChapter12.pdf)

   Additional reading: Chapter 11 and 12 from Functional programming using F#

*)

let x = seq [1..10]
let y = seq [|1..10|]

Seq.find(fun x' -> x' = 9) y
Seq.toList y

let num = seq {
   let n = 10
   yield n + 1
   printfn "second .."
   yield n + 2
}

Seq.take 1 num
Seq.take 2 num
Seq.toList num
num

let capitals = ["Tallinn" ; "Prague"]

let withNew x =
   seq { yield x
         yield "New " + x
       }

let allCities =
   seq {
       yield "Seattle"
       yield! capitals
       yield! withNew "York"
   }

Seq.take 2 allCities
List.ofSeq allCities 
allCities |> List.ofSeq

open System

let rec factorialsUtil num factorial = seq {
   if (factorial < 10000000) then
       yield String.Format ("{0}! = {1}", num, factorial)
       yield sprintf "%d! = %d" num factorial
   //if (factorial < 100) then 
       let num = num + 1
       yield! factorialsUtil num (factorial * num)

}

let factorials =
    factorialsUtil 0 1

factorials |> List.ofSeq

Seq.take 2 factorials

open System
open System.Drawing

let rnd = new Random()
#nowarn "40"
let rec colorsRnd =
    seq { let c() = rnd.Next(256)
          yield Color.FromArgb (c(), c(), c())
          yield! colorsRnd
         }

Seq.take 3 colorsRnd

let cities = [("New York", "USA"); ("London", "UK")
              ("Cambridge", "UK"); ("Cambridge", "USA")]

let entered = ["London"; "Cambridge"]

seq {
   for name in entered do
      for (n,c) in cities do
         if (n = name) then
             yield sprintf "%s from %s" n c
}

entered |> Seq.collect (fun name -> 
      seq {
          for (n,c) in cities do
              if (n = name) then
                 yield sprintf "%s from %s" n c
      }
)

entered |> Seq.collect (fun name -> 
    cities |> Seq.collect (fun (n,c) ->
        if (n <> name) then []
        else [sprintf "%s from %s" n c]))


open System

type ValueWrapper<'a> =
    | Value of 'a

type ValueWrapperBuilder() =
    member x.Bind (Value (v), f)  = f v
    member x.Return(v) = Value v

let value = new ValueWrapperBuilder()

let readInt() = value {
   let n = Int32.Parse(Console.ReadLine())
   return n
}

readInt()

let v = 
   value {
      let! n = readInt()
      let! m = readInt()
      let add = n + m
      let sub = n - m
      return add * sub
   }

module Logging =
    open System

    type LoggingValue<'a> =
          | Log of 'a * list<string>

    type LoggingBuilder() =
      member x.Bind(Log(v,logs1), f) =
           let (Log(nv,logs2)) = f (v)
           Log (nv, logs1 @ logs2)

      member x.Return(v) = Log(v, [])
      member x.Zero() = Log((),[])

    let log = new LoggingBuilder()
    let logMessage(s) = Log((), [s])
    let write(s) = log {
        do! logMessage("Writing: " + s)
        Console.Write(s) }

    let read() = log {
        do! logMessage("Reading ...")
        return Console.ReadLine() }

    let testIt  ()= log {
       do! logMessage("Starting ...")
       do! write("Enter your name:")
       let! name = read()
       return "Hello "+ name + "!"
    }



Logging.testIt ()

// The compiler translates the computation expression syntax to the following:
module Translations =
    
    let write(s) =
      log.Bind(logMessage("writing: " + s), fun () ->
         Console.Write(s)
         log.Zero())

    let read() = 
      log.Bind(logMessage("reading"), fun () ->
        log.Return(Console.ReadLine()))

    let testIt() = 
      log.Bind(logMessage("starting"), fun () ->
        log.Bind(write("Enter name: "), fun () ->
          log.Bind(read(), fun name ->
            log.Return("Hello " + name + "!"))))







