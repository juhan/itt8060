(*

   Lecture Nov 11, 2015: Generating data for testing: FsCheck

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

// Basic checking

let revRevIsOrigInt (xs : list<int>) = List.rev (List.rev xs ) = xs

revRevIsOrigInt [1..5]

Check.Quick revRevIsOrigInt

// More details
Check.Verbose revRevIsOrigInt

// A test that is bound to fail
let revIsOrigInt (xs : list<int>) = List.rev xs = xs

Check.Quick revIsOrigInt

// Some surprises with floats
let revRevIsOrigFloat (xs : list<float>) = List.rev (List.rev xs ) = xs

Check.Quick revRevIsOrigFloat
Check.Verbose revRevIsOrigFloat

// Testing generic functions -> the generated argument is of the most general
// type satisfying the type constraints, in this case of Object type.
let revRevIsOrig xs  = List.rev (List.rev xs) = xs

Check.Quick revRevIsOrig
Check.Verbose revRevIsOrig

// A check that checks if a list is ordered
let rec ordered xs = 
    match xs with
      | [] | [_]        -> true
      | x1 :: x2 :: xs'  -> x1 <= x2 && ordered (x2 :: xs')

ordered [1;2;3;4]

ordered [2;2;2;2;2]

// A function that inserts into a list and preserves order
let rec insert x xs = 
    match xs with
    | []      -> [x]
    | y :: ys -> if x <= y then x :: y :: ys 
                 else           y :: insert x ys

// Not the most clear way of writing such a property
let insertKeepsOrderBad (x : int) xs =
    if ordered xs then ordered (insert x xs) else false

// A clearer way of writing a property that checks if insert keeps order
let insertKeepsOrder (x : int) xs = ordered xs ==> ordered (insert x xs)

// note that many cases fail to match the ordered list criterion that is a
// prerequisite for the property.
Check.Quick insertKeepsOrder

// Thus we create a generator that generates ordered lists by
// creating an arbitrary (random) list of integers that is then
// sorted by List.sort function and later filtered by the ordered function.
// The filtering guarantees that all data fits our prerequisites
let orderedList = Arb.from<list<int>> |> Arb.mapFilter List.sort ordered

// Now we check if for all ordered lists that our random list generator generates
// the property of preserving orderedness under insert preserves 
let insertKeepsOrder' x = 
    Prop.forAll orderedList (fun xs -> ordered (insert x xs))

// Now we get OK and see that all tests actually did something useful
Check.Quick insertKeepsOrder'
Check.Verbose insertKeepsOrder'

// Now lets try to make some sense out of the data distribution
// of the generated data.
// First, let us use a built in classifier that classifies
// data instances to be trivial (e.g. when the list is empty)
let insertKeepsOrderWithTrivial x xs = 
    insertKeepsOrder x xs |> Prop.trivial (List.length xs = 0)

Check.Quick insertKeepsOrderWithTrivial

// We can add classifiers to see how many of such instances 
// occurred in the generated data.
let insertKeepsOrderWithClassify x xs = 
    insertKeepsOrder x xs
    |> Prop.classify (ordered (x :: xs))   "at the beginning"
    |> Prop.classify (ordered (xs @ [x]))  "at the end"

Check.Quick insertKeepsOrderWithClassify

// We can combine these with collect that counts test cases based
// on some criterion passed as an argument, in this case the length of
// the list.
let insertKeepsOrderWithCollect x xs = 
    insertKeepsOrder x xs
    |> Prop.classify (ordered (x :: xs))   "at the beginning"
    |> Prop.classify (ordered (xs @ [x]))  "at the end"
    |> Prop.collect (List.length xs)

Check.Quick insertKeepsOrderWithCollect

// We can combine several properties into a more complex 
// property. In this case you could also use conjunction "&&"
// but in the next case not any more.
let complex (n: int) (m: int) =
    let res = n + m
    (res >= n) .&.
    (res >= m) 

Check.Quick complex

// Labelling the subproperties will give feedback at failure
// which subproperty actually got violated.
let complexWithLabels (n: int) (m: int) =
    let res = n + m
    (res >= n) |@ "result >= #1"    .&.
    (res >= m) |@ "result >= #2"   

Check.Quick complexWithLabels

Check.Quick complexWithLabels

// The operator that combines a property with a label
(|@)

// The operator that combines a label with a property
(@|)











   




























