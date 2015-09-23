(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 3: lists, recursion, units of measure

  Juhan Ernits

*)

let rangeTest testValue mid size = 
    match testValue with
    | var1 when var1 >= mid - size/2 && var1 <=mid + size/2 -> printfn "The value is  in range"
    | _ -> printfn "The value is out of range"


let tallinn = ("Tallinn", 400000)
let tartu = ("Tartu", 200000)

let getName city = 
    match city with
    | ("Tallinn",_) -> "the capital"
    | (name,_) -> name


getName tallinn
getName tartu

let getName1 city = 
    let name, pop = city
    match name,pop with
    | "Tallinn",_ -> "the capital"
    | t -> "other town"

let getName2 (name, pop) = 
    match name with
    | "Tallinn" -> "the capital"
    | _ -> name


let rec factorial n =
  if n <= 1 then
    1
  else
    n * factorial (n-1)

factorial 6
factorial 3

let ls1 = []
let ls2 = 5 :: ls1
let ls3 = 3 :: 4 :: 5 :: []
let ls4 = [3;4;5;6;7]

// this is a no-no in homeworks!
ls4.[2]

let squareFirst list =
  match list with
  | head :: _ -> head * head
  | []        -> -1 // failwith "oops"

squareFirst ls4

squareFirst []

let squareFirstFloat list =
  match list with
  | head :: _ -> head * head
  | []        -> -1.0 // just changing this will change the type of the function.

let list = [1..5]

List.map (printf "%d ") [1..5000]

let rec sumList list =
  match list with
  | head :: tail -> head + sumList tail
  | []  -> 0

sumList [1..5]

let rec prodList list =
  match list with
  | head :: tail -> head * prodList tail
  | []  -> 1

prodList [1..5]

// fold
// aggregateList : list:'a list -> init:'b -> op:('a -> 'b -> 'b) -> 'b
let rec aggregateList (list : int list) (init : int) (op : int -> int -> int) : int =
  match list with
  | []           -> init
  | head :: tail -> op head (aggregateList tail init op) 

(+) 1 3

aggregateList [1..5] 0 (+)

aggregateList [1..5] 1 (*)

aggregateList [1..5] 0 

// map
let rec mymap op list =
  match list with
  | [] -> []
  | head :: tail -> op head :: mymap op tail

let scale s x = s * x

mymap (scale 1000) [1..5] 

scale 1000

[<Measure>] type km
[<Measure>] type h

let v = 10.0<km/h>

let genericSumUnits (x: float<'u>) (y: float<'u>) = x + y

[<Measure>] type degC
[<Measure>] type degF

let convertCtoF (temp: float<degC>) = 9.0<degF> / 5.0<degC> * temp + 32.0<degF>
let convertFtoC (temp: float<degF>) = 5.0<degC> / 9.0<degF> * (temp - 32.0<degF>)

let currentTemp = 15.0<degC>
convertCtoF currentTemp





















