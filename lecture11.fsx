(*

   Lecture Nov 18, 2015: Generating data for testing: FsCheck continued

   Based on https://fscheck.github.io/FsCheck/

If tests fail to run in Visual studio/Windows (FsUnit dll related error messages) then check that
your Help -> About Visual Studio shows that you have installed update 4 to VS2013 and appropriate 
.Net framework updates. It is encouraged to test it in VS2015.

In MonoDevelop it is possible to get FsCheck with NUnit to work
by changing the target .Net framework to version 4.5, otherwise installing FsCheck and FsCheck.NUnit from NuGet will
fail with incompatible framework error.

*)

#r @"..\packages\FsCheck.2.2.1\lib\net45\FsCheck.dll"

open FsCheck

// Generators

let chooseFromList xs =
   gen {
       let! idx = Gen.choose(0, List.length xs - 1)
       return (List.nth xs idx)
   }


List.nth [1..5] 4
[1..5].[4]

let primeIsOdd =
    Prop.forAll (Arb.fromGen (chooseFromList [2; 3; 5; 7; 11; 13] ))
                 (fun p -> p % 2 = 1)

Check.Quick(primeIsOdd)
   
let booleanGen =
   Gen.oneof [
       gen {return true}
       gen {return false}
   ]
   
let optimist =
   Gen.frequency [
      (2, gen {return true})
      (1, gen {return false})
   ]

let f =
   Prop.forAll(Arb.fromGen (optimist))
          (fun x -> true) 
   
Check.Quick(f)

type Tree =
  | Leaf of int
  | Branch of Tree * Tree


let rec unsafeTreeGen () =
   Gen.oneof [
      Gen.map Leaf Arb.generate<int>
      Gen.map2 (fun left right -> Branch (left, right))
               (unsafeTreeGen ())
               (unsafeTreeGen ())
   ]

let treeGen =
  let rec sizedTreeGen size =
    match size with
    | 0 -> Gen.map Leaf Arb.generate<int>
    | _ -> let subtreeGen = sizedTreeGen (size / 2)
           Gen.oneof [
                Gen.map Leaf Arb.generate<int>
                Gen.map2 (fun left right -> Branch (left, right))
                         subtreeGen
                         subtreeGen
           ]
  Gen.sized sizedTreeGen

let rec leaves tree =
   match tree with
   | Leaf lab -> [lab]
   | Branch (left, right) -> leaves left @ leaves right

let leavesNotEmpty = 
   Prop.forAll (Arb.fromGen treeGen)
               (fun tree -> List.length (leaves tree) > 0)


Check.Verbose(leavesNotEmpty)

type Generators =
   static member Tree () = {
     new Arbitrary<Tree> () with
        override arbitrary.Generator = treeGen
        override arbitrary.Shrinker tree = Seq.empty
   }

Arb.register<Generators>

let leavesNotEmpty' tree = List.length (leaves tree) > 0

Check.Verbose leavesNotEmpty'



// Lazy


let x = printf "called me!" ; 1 + 2
x

let x' = lazy (printf "called me!" ; 1 + 2)

x'.Force() 
x'.Force()

// Memoisation of functions

let addSimple (a , b) =
   printfn "adding %d + %d" a b
   a + b

addSimple (2, 3)

open System.Collections.Generic

let add =
  let cache = new Dictionary<_,_>()
  (fun x ->
     match cache.TryGetValue x with
     | true, v -> v
     | _       -> let v = addSimple x
                  cache.Add(x,v)
                  v)


add(2,3)
add(2,3)
add(2,3)
add(2,4)





