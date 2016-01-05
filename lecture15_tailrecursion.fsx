(*

   Lecture Dec 16, 2015: Tail recursion

   Additional reading: Chapter 10 in RWFP
                       Chapter 9 in Functional Programming Using F#


   The example is based on chapter 9 from Expert F# 3.0.

*)

#time

let rec sum l =
   match l with
   | [] -> 0
   | h :: t -> h + sum t

sum [1..100000] // works
//sum [1..1000000] // crashes

let sumA l = 
    let rec sumAaux a l =
        match l with
        | [] -> a
        | h :: t -> sumAaux (a+h) t
    sumAaux 0L l

sumA [1L..1000000L]

let rec fact x =
   match x with
   | 1 -> 1
   | x -> x * fact (x-1)

fact 16

let factA x =
    let rec aux (p,x) =  
       match x with
       | 1 -> p
       | x -> aux (p*x, x-1)
    aux (x,x)

factA 16

let numbers = List.init 10000000 (fun i -> 16) 

#time

List.map fact numbers

List.map factA numbers

type Tree =
   | Node of string * Tree * Tree
   | Tip of string

let rec sizeNotTailRec tree = 
   match tree with
   | Tip _ -> 1
   | Node (_, treeLeft, treeRight) -> 
        sizeNotTailRec treeLeft + sizeNotTailRec treeRight


let rec sizeAcc acc tree =
   match tree with
   | Tip _ -> 1 + acc
   | Node (_,treeLeft,treeRight) -> 
       let acc = sizeAcc acc treeLeft
       sizeAcc acc treeRight

let rec mkBigUBTree n tree =
   if n = 0 then tree
   else Node("n", Tip("tip"), mkBigUBTree (n-1) tree)
   
   
let tree1 = Tip("tip")
let tree2 = mkBigUBTree 15000 tree1
let tree3 = mkBigUBTree 15000 tree2
let tree4 = mkBigUBTree 15000 tree3
let tree5 = mkBigUBTree 15000 tree4
let tree6 = mkBigUBTree 15000 tree5


let rec sizeCont tree cont =
   match tree with
   | Tip _ -> cont 1
   | Node (_,leftTree,rightTree) -> 
        sizeCont leftTree (fun leftSize ->
              sizeCont rightTree (fun rightSize ->
                   cont (leftSize + rightSize)))


let rec sizeContAcc acc tree cont =
   match tree with
   | Tip _ -> cont (1 + acc)
   | Node (_,leftTree, rightTree) ->
         sizeContAcc acc leftTree (fun accLeftSize -> 
             sizeContAcc accLeftSize rightTree cont)

sizeContAcc 0 tree6 (fun x -> x)

let rec sumCont l cont =
   match l with
   | [] -> cont 0
   | h :: t -> sumCont t (fun x -> cont (x + h))

sumCont [1..1000000] id