//
// Examples from the Q&A session in Tartu on Friday, November 20th
//
//


#r @"..\packages\FsCheck.2.2.1\lib\net45\FsCheck.dll"


open FsCheck

// What about the property of a sum of elements of a list?

let rec sum xs = 
  match xs with
  | []     -> 0
  | x::xs' -> x + sum xs'

let listSumDependsOnAddedElement xs x =
  ((x >= 0) ==>  (sum xs <= sum (x::xs))) .|.
  ((x < 0)  ==>  (sum xs > sum (x::xs)))

Check.Quick listSumDependsOnAddedElement


let checkSomeRomanNumberProperty s =
    ((s = "XIX") ==> true )


Check.Quick checkSomeRomanNumberProperty


Arb.generate<int>


let createStringFromChars (cs : char list) = 
    gen {
       let! chars = Gen.arrayOf (Gen.elements cs)
       return System.String.Concat(chars)
    }


let testStringGen =
    Prop.forAll (Arb.fromGen (createStringFromChars ['I';'X']))
                 (fun r -> checkSomeRomanNumberProperty r)
//let customConfig = { Config.Quick with MaxTest=100 }
//Check.One(customConfig, testStringGen)
Check.Quick testStringGen


let x = 4 + 5

let lx = lazy (4 + 5)

let y = lazy (printf "hi"; 5 + 6)

y.Value + 1




