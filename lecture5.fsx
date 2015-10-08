(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 5: Option, functional values, generics

  Reading: Chapter 5 in RWFP

           * Section 3.11 introduces option
           * The other concepts are thoroughly explained in Chapters 4 and 5.

  Juhan Ernits

*)

open System

Int32.TryParse "1"
Int32.TryParse "noise"

type IntOption = | IntSome of int
                 | IntNone

let readInput1() =
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | true, i -> IntSome i
    | _       -> IntNone

readInput1()

// Built-in type
//type Option<'a> = | Some of 'a 
//                  | None
// OCaml syntax for the built-in option.
//type 'a option = | Some of 'a 
//                 | None

let readInput2() =
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | true, i -> Some i
    | _       -> None


let multiplyWith (scalar: int) (parsedData : int option) =
    match parsedData with
    | Some x  ->  scalar * x
    | None    ->  0

multiplyWith 2 (Some 5)

multiplyWith 3 None

multiplyWith 3 (readInput2())

let readValue (opt : 'a option) : 'a =
   match opt with
   | Some n -> n
   | None   -> failwith "No value"

let readValue2 opt =
   match opt with
   | Some n -> n
   | None   -> failwith "No value"

readValue (Some 2)

// lecture5.fsx(69,1): error FS0030: Value restriction. The value 'it' has been inferred to have generic type
//    val it : '_a    
// Either define 'it' as a simple data term, make it a function with explicit arguments or, if you do not intend for it to be generic, add a type annotation.
// need to fix it by a concrete type parameter
readValue (None : 'a option)
// e.g.
readValue (None : int option)


let nums = [4 ; 9; 1; 8; 6]

let evenTest n = (n % 2 = 0)

let evens = List.filter evenTest nums

let square1 (a : int) : int = a * a

let square2 : int -> int = fun a -> a * a

let square2_2 = fun a -> a * a

let add : int -> int -> int = fun a b -> a + b

let twice input f = f (f input)

twice 2 square1

// not functional style
let add2  = fun (a, b) -> a + b

// cannot do that add2 (2,_)

let add3 = fun a b -> a + b

let add4 = fun a -> fun b -> a + b

let addTen = add 10

List.map addTen [ 1.. 10 ]

List.map (fun a -> a 1) (List.map add [1 .. 10])

let (|>) x f  = f x

List.map add [1 .. 10] |> List.map (fun a -> a 1)

List.head( List.rev [1 .. 5])

[1;2;3] @ [4;5]

let oldPrague = "Prague" , 123

// first attempt
let name, pop = oldPrague
let newPrague = name, pop + 1 

//second attempt
let newPrague2 = fst oldPrague, snd oldPrague + 1

//third attempt
let mapSecond f (a, b) = a, f b
let newPrague3 = oldPrague |> mapSecond ((+) 1)

let bimap f g (a, b) = f a, g b
let mapFirst f p = bimap f id p

let id a = a



