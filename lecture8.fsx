(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 8: behaviour centric programming: decision trees
             recursive definition of types and values
  
  Juhan Ernits

*)
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

