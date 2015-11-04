(*

   Lecture Nov 4, 2015: Combining behaviour with data.
   Unit testing with FsUnit and NUnit.

   Based on chapter 9 of RWFP.
   The unit testing part is in chapter 11, but the code 
   here has been updated to support FsUnit and NUnit.

If tests fail to run in Visual studio/Windows (FsUnit dll related error messages) then check that
your Help -> About Visual Studio shows that you have installed update 4 to VS2013 and appropriate 
.Net framework updates. It is encouraged to test it in VS2015.

In MonoDevelop it is possible to get FsUnit 1.4.0.0 with NUnit to work
by changing the target .Net framework to version 4.5, otherwise installing FsUnit from NuGet will
fail with incompatible framework error.

*)
module lecture9

//namespace Lecture9

    type Rect = 
         { Left    : float32
           Top     : float32
           Width   : float32
           Height  : float32
         }

         member this.Deflate (vspace, hspace) = 
             {Left   = this.Left + vspace
              Top    = this.Top + hspace
              Width  = this.Width - (2.0f * vspace)
              Height = this.Height - (2.0f * hspace) }

         member this.Area() =
              this.Width * this.Height

    let r = {Left=0.0f; Top=0.0f; Width=100.0f; Height=200.0f}
    r.Area ()

    // from lecture 8
    type Client = 
      { Name : string; Income : int ; YearsInJob : int
        UsesCreditCard : bool;  CriminalRecord : bool }

    type QueryInfo =
      { Title     : string
        Check     : Client -> bool
        Positive  : Decision
        Negative  : Decision }

    and Decision = 
       | Result of string
       | Query  of QueryInfo

    let rec tree =
       Query  {Title = "More than €40k"
               Check = (fun cl -> cl.Income > 40000)
               Positive = moreThan40
               Negative = lessThan40}
    and moreThan40 =
       Query  {Title = "Has criminal record"
               Check = (fun cl -> cl.CriminalRecord)
               Positive = Result "NO"
               Negative = Result "YES"}
    and lessThan40 =
       Query  {Title = "Years in job"
               Check = (fun cl -> cl.YearsInJob > 1)
               Positive = Result "YES"
               Negative = usesCreditCard}
    and usesCreditCard =
       Query  {Title = "Uses credit card"
               Check = (fun cl -> cl.UsesCreditCard)
               Positive = Result "YES"
               Negative = Result "NO"}

    let rec testClientTree client tree =
        match tree with
        | Result msg  -> printfn " OFFER A LOAN: %s" msg
        | Query qinfo -> let result, case = 
                             if qinfo.Check(client) then
                                 "yes", qinfo.Positive
                             else
                                 "no", qinfo.Negative
                         printfn " - %s ? %s" qinfo.Title result
                         testClientTree client case

    let john = {Name = "John Doe"; Income = 40000 ; YearsInJob = 1 ; 
                UsesCreditCard = true ; CriminalRecord = false }

    testClientTree john tree

    type Decision with
        member x.decide(client) = testClientTree client x
        static member foo () = "static member"

    tree.decide(john)

    type IClientCredit =
         abstract Check  : Client -> bool
         abstract Report : Client -> unit

    let testCriminal = 
        {new IClientCredit with
         member this.Check(cl)  = cl.CriminalRecord
         member this.Report(cl) = printfn "%s' has a criminal record" cl.Name
         }

    type Client2 = 
      { Name : string; Income : int ; YearsInJob : int
        UsesCreditCard : bool;  CriminalRecord : bool }
       member x.Foo () = 1
       member x.Bar () = "bar"

       interface IClientCredit with
           member this.Check(cl)  = cl.CriminalRecord
           member this.Report(cl) = printfn "%s' has a criminal record" cl.Name

    type Client with
        member x.Gender = "M"

    type ClientInfo(name, income, years) = 
         let loanCoefficient = income / 500 * years
         do printfn "Creating client '%s'" name

         member x.Name = name
         member x.Income = income
         member x.Years = years

         member x.Report () = 
             printfn "Client: %s, loan coefficient: %d" name loanCoefficient

    type Triangle (a:float, b:float, c:float) =
        member this.A = a
        member this.B = b
        member this.C = c

        member this.IsRightAngled =
           a*a + b*b = c*c

        member this.IsTriangle : bool =
           a+b > c

#if INTERACTIVE
#r @"..\packages\NUnit.2.6.4\lib\nunit.framework.dll"
#r @"..\packages\FsUnit.1.4.0.0\lib\net45\FsUnit.NUnit.dll"
#endif

    open NUnit.Framework
    open FsUnit

    [<TestFixture>]
    type ``Given a rightangled triangle`` () =
       let triangle = new Triangle (3.0, 4.0, 5.0)

       [<Test>]
       member this.
         ``Whether the right angled triangle is a triangle`` () =
         triangle.IsTriangle |> should equal true

       [<Test>]
       member this.
          ``Whether the right angle check works`` () =
          triangle.IsRightAngled |> should equal true


    [<TestFixture>]
    type ``Given some negative values`` () =
       let triangle = new Triangle (3.0, 4.0, -5.0)

       [<Test>]
       member this.
          ``The triangle test should fail`` () =
          triangle.IsTriangle |> should equal false
    