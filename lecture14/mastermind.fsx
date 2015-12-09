type Color = Red | Blue | Green | Purple

type Code = list<Color>
type Hint = RightPlace | WrongPlace | Missing
type Hints = list<Hint>
type Checker = Code -> Hints

let check (code : Code)(guess : Code) : Hints =
    let rightPlaces = List.map2 (fun x y -> x = y) code guess
    let wrongPlace g = List.exists (fun c -> g = c) code
    let wrongPlaces = List.map wrongPlace guess
    let test b1 b2 = match (b1,b2) with
        | (true,_) -> RightPlace
        | (false,true) -> WrongPlace
        | _            -> Missing
    List.map2 test rightPlaces wrongPlaces

let test1 = check [Blue;Blue;Green] [Blue;Green;Blue]

let printHints (xs : Hints) : string =
    let help (x : Hint) : string =
        match x with
            | RightPlace -> "*"
            | WrongPlace -> "-"
            | Missing    -> "."
    String.concat "" (List.map help xs)

let test2 = printHints test1            

let tryParseColor (c : char) : option<Color> =
    match c with
        | 'R' | 'r' -> Some Red
        | 'G' | 'g' -> Some Green
        | 'B' | 'b' -> Some Blue
        | 'P' | 'p' -> Some Purple
        | _         -> None

let test3 = Seq.map tryParseColor "RGBB"|> Seq.toList

let rec sequence (xs : list<option<'a>>) : option<list<'a>> =
    match xs with
    | [] -> Some []
    | None::tl -> None
    | Some x::tl ->
        match sequence tl with
        | Some xs -> Some (x::xs)
        | None    -> None
        
let test4 = sequence test3

let tryParse (s : string) : option<Code> =
    let xs = s |> Seq.map tryParseColor |> Seq.toList
    if xs.Length = 4 then sequence xs else None

let win (hs : Hints) : bool = List.forall (fun x -> x = RightPlace) hs

// model the game

type State = Start | Playing of int * Checker | Win | Lose

type Command = Reset | Submit of Code

let next (s : State)(c : Command) : State =
    match c with
        | Reset -> Start
        | Submit code ->
            match s with
                | Start -> Playing (8, check code)
                | Playing (i , f) ->
                    if win (f code) then
                        Win
                    elif i <= 1 then
                        Lose
                    else
                        Playing (i - 1, f)
                | Win -> Win
                | Lose -> Lose        
                        
                    
        
// Game loop
open System

let rec run (s : State) : unit =
    let getInput (cont : Code -> unit) : unit =
        let input = Console.ReadLine()
        if input = "reset" then
            run Start
        elif input = "quit" then
            ()
        else
            let code = tryParse input
            match code with
                | Some xs -> cont xs
                | None ->
                    Console.WriteLine("Invalid code")
                    run s
    match s with
            | Start ->
                Console.Write("Enter a secret code: ")
                getInput (fun xs ->
                          for i in 0..24 do
                            Console.WriteLine("")
                          run (next s (Submit xs)))
            | Playing (i , f) ->
                Console.Write("Enter a guess: ")
                getInput (fun xs ->
                          Console.WriteLine (printHints (f xs))
                          run (next s (Submit xs)))
            | Win ->
                Console.WriteLine("You win!")
                run (next Start Reset)
            | Lose ->
                Console.WriteLine("You lose!")
                run (next Start Reset)
                          
                          
                          
