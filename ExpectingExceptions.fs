module ExpectingExceptions


module exceptionThrowingCode = 
   // A function that throws an exception
   let f1 () = failwith "Oops!"

   // A function that does not throw an exception
   let f2 () = "OK"

open NUnit.Framework

// This example uses just NUnit framework for checking if exception gets thrown.
// It is equivalent to either have 2 attributes of a single with a list of them.
// Note that it is important to ignore any output of the functions if output
// is produced.
// For this, only NUnit.Framework is required.
[<TestFixture>]
type NUnitExample () =

    [<Test; ExpectedException(typeof<System.Exception>)>]
    member x.``test if exception is thrown when it actually is``() =
       exceptionThrowingCode.f1 () |> ignore

    [<Test>]
    [<ExpectedException(typeof<System.Exception>)>]
    member x.``test if exception is thrown when it actually is not``() =
        exceptionThrowingCode.f2 () |> ignore

    [<Test>]
    member x.``test that f2 outputs "OK"``() =
        Assert.AreEqual(exceptionThrowingCode.f2 (), "OK")
        

open FsUnit

// This example uses FsUnit apporach to detecting exceptions. Note that the code throwing exception
// is included in the body of a lambda function.
// Requires both NUnit.Framework and FsUnit
[<TestFixture>]
type FsUnitExample () =

    [<Test>]
    member x.``test if exception is thrown when it actually is``() =
        (fun () -> exceptionThrowingCode.f1 () |> ignore) |> should throw typeof<System.Exception> 

    [<Test>]
    member x.``test if exception is thrown when it actually is not``() =
        (fun () -> exceptionThrowingCode.f2 () |> ignore) |> should throw typeof<System.Exception>

    [<Test>]
    member x.``test that f2 outputs "OK"``() =
        exceptionThrowingCode.f2 () |> should equal "OK"

open FsCheck
open FsCheck.NUnit

// In FsCheck it is also possible to check for exceptions. There is a property building combinator
// for that in the Prop module.
[<TestFixture>]
type FsCheckExample () =

    [<Property>]
    member x.``test if exception is thrown when it actually is``() =
        Prop.throws(lazy (exceptionThrowingCode.f1()|> ignore))

    [<Property>]
    member x.``test if exception is thrown when it actually is not``() =
        Prop.throws(lazy (exceptionThrowingCode.f2()|> ignore))

    // A simple property that checks output in the case of particular input (unit in this case)
    [<Property>]
    member x.``test that f2 outputs "OK"``() =
        exceptionThrowingCode.f2 () = "OK"



