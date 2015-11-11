(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------------------

  Coursework 7: Generating data for testing

  ------------------------------------------------
  Name:
  Student ID:
  ------------------------------------------------


  Answer the questions below. You answers to the questions should be correct F#
  code written after the question. This file is an F# script file; it should be
  possible to load the whole file at once. If you can't, then you have
  introduced a syntax error somewhere.

  This coursework will be graded.

  Commit and push your solution to the repository as
  file coursework7.fsx in directory coursework7.

  Please do not upload any DLL-s. Just include a readme.md file containing the 
  dependencies required (additional DLLs)

  The deadline for completing the above procedure is Friday, November 20, 2015.

  We will consider the submission to be the latest version of the appropriate
  files in the appropriate directory before the deadline of a particular
  coursework.

*)

(*
You should now have a BrokenRomanNumbers implementation and your own CorrectRomanNumbers
implementation from coursework 6.
*)

(*   1) Write at least 3 FsCheck properties that you can use to check both
     implementations of Roman numeral conversions. The properties should return
     "OK" in the case of correct implementation, and "Falsifyable" in the
     broken implementation.
*)

(*   2) Write at least 1 data generator and at least 1 FsCheck property for the following code:

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
    | Result msg  -> printfn " OFFER A LOAN: %s" msg ; msg // Added that msg gets returned, otherwise side effects are hard to test.
    | Query qinfo -> let result, case = 
                         if qinfo.Check(client) then
                             "yes", qinfo.Positive
                         else
                             "no", qinfo.Negative
                     printfn " - %s ? %s" qinfo.Title result
                     testClientTree client case
*)