(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 4: Multiple arguments and multiple results,
             user defined data types,
             recursive data types,
             option

  Juhan Ernits

*)


let pair =  11, "Hello!"

let divRem n m = n / m, n % m

let divRem' (n,  m) = n / m, n % m

divRem' (7, 2)

open System

Int32.TryParse "11"

Int32.TryParse "F#"

let triple = 11, "Hello!", 'A'
let leftLeaning = (11, "Hello!"), 'A'
let rightLeaning = 11, ("Hello!", 'A')

let printPlaceInfo name (latitude, longitude) = 
    printfn "%s @ (%f deg, %f deg)" name latitude longitude
    
let tut = "Tallinn University of Technology", (59.3950, 24.6719)

let tutName, tutPos = tut
printPlaceInfo tutName tutPos

// user defined types

type Lat = float
type Lon = float

type Fexpr = | Const of float
             | X
             | Add of Fexpr * Fexpr
             | Sub of Fexpr * Fexpr
             | Mul of Fexpr * Fexpr
             | Div of Fexpr * Fexpr
             | Sin of Fexpr
             | Cos of Fexpr
             | Log of Fexpr
             | Exp of Fexpr

let rec D fe =
   match fe with
   | Const _       -> Const 0.0
   | X             -> Const 1.0
   | Add(fe1,fe2)  -> Add(D fe1, D fe2)
   | Sub(fe1,fe2)  -> Sub(D fe1, D fe2)
   | Mul(fe1,fe2)  -> Add(Mul(D fe1, fe2), Mul(fe1,D fe2))
   | Div(fe1,fe2)  -> Div(Sub(Mul(D fe1, fe2), Mul(fe1,D fe2)), Mul(fe2,fe2))
   | Sin fe1       -> Mul(Cos fe1, D fe1)
   | Cos fe1       -> Mul(Const -1.0, Mul(Sin fe1, D fe1))
   | Log fe1       -> Div(D fe1, fe1)
   | Exp fe1       -> Mul(Exp fe1, D fe1)

let example1 = Mul(Const 2.0, Mul(X,Mul(X,X)))
let example2 = Exp(X)

D example1
D example2


type IntList =
  | Empty
  | NonEmpty of int * IntList

let rec sumIntList list = 
  match list with
  | Empty   -> 0 
  | NonEmpty (head, tail) -> head + sumIntList tail

let list1 = NonEmpty(1, NonEmpty(2, NonEmpty(3,Empty)))

sumIntList list1

let rec mapIntList (trans : int -> int) list =
  match list with
  | Empty                 -> Empty
  | NonEmpty (head, tail) -> NonEmpty (trans head, mapIntList trans tail)

let addOne = (+) 1

mapIntList addOne list1

mapIntList ((+) 2) list1

let rec filterIntList (prop: int -> bool) list = 
  match list with
   | Empty                 -> Empty
   | NonEmpty (head, tail) -> let filteredTail = filterIntList prop tail
                              if prop head
                              then NonEmpty (head, filteredTail)
                              else filteredTail

let equalsToTwo = (=) 2

filterIntList equalsToTwo list1
filterIntList ((<) 1) list1


type Tree =
  | Leaf of int
  | Node of Tree * Tree

let tree1 = Leaf 2
let tree2 = Node (Leaf 3, Leaf 5)
let tree3 = Node (tree1, tree2)

let rec sumTree tree = 
  match tree with
  | Leaf label          -> label
  | Node (left, right)  -> sumTree left + sumTree right


sumTree tree1
sumTree tree2
sumTree tree3































 