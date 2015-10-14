(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 6 Higher order functions

  Reading: Chapter 6 in RWFP
           Additional reading: http://fsharpforfunandprofit.com/posts/elevated-world-2/
  
  Juhan Ernits

*)

open System

type Schedule =
   | Never
   | Once of DateTime
   | Repeatedly of DateTime * TimeSpan

// this is broken, because the type of d is not known yet
(fun d -> d.AddDays(7.0)) (DateTime(2015,10,14))

// fix
(DateTime(2015,10,14)) |> (fun d -> d.AddDays(7.0))
// fix 2
(fun (d:DateTime) -> d.AddDays(7.0)) (DateTime(2015,10,14))

((fun (d:DateTime) -> d.AddDays(7.0)) : DateTime -> DateTime) (DateTime(2015,10,14))

((fun d -> d.AddDays(7.0)) : DateTime -> DateTime) (DateTime(2015,10,14))

(DateTime(2015,10,14)).AddDays(7.0)

let mapSched (f: DateTime -> DateTime) (sched: Schedule) : Schedule =
  match sched with
  | Never -> Never
  | Once dt -> Once (f dt)
  | Repeatedly (dt, ts) -> Repeatedly (f dt, ts)

mapSched (fun d -> d.AddDays(7.0)) (Once (DateTime(2015,10,15)))

let scheds = [Never; Once (DateTime(2015,10,15))]

List.map (mapSched (fun d -> d.AddDays(7.0))) scheds

let readInput() = 
   let input = Console.ReadLine()
   match Int32.TryParse input with
   | true, i -> Some i
   | false, _-> None
   
let readAndAdd1() =
  match readInput() with
  | None -> None
  | Some n ->
     readInput() |> Option.map ((+) n) 

let readAndAdd2() =
  match readInput() with
  | None -> None
  | Some n ->
     match readInput() with
     | None -> None
     | Some m -> Some (n + m)


readAndAdd1()

let map f opt = 
  match opt with
  | None   -> None
  | Some x -> Some (f x) 


let bind f opt = 
  match opt with
  | None    -> None
  | Some x  -> f x


let readAndAdd3() = readInput() |> bind (fun n -> readInput() |> Option.map ((+) n))

readAndAdd3()

//1 / 0

let parseInt str = 
  match str with
  | "-1" -> Some -1
  | "0"  -> Some  0
  | "1"  -> Some  1
  | "2"  -> Some  2
  | _    -> None

type OrderQty = OrderQty of int

let toOrderQty qty = 
  if qty >= 1 then
      Some (OrderQty qty)
  else
      None

//Option.bind is a built in implementation of the above bind
let parseOrderQty str =
  parseInt str |> Option.bind toOrderQty

// is equivalent to
let parseOrderQty2 str =
  Option.bind toOrderQty (parseInt str) 

parseOrderQty "2"
parseOrderQty "-1"

//let parseOrderQty3 str = 
//   (str |> parseInt) >>= toOrderQty

//Type inference example done on the board. As in section 6.2.2 in RWFP.

