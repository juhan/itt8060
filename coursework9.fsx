(*
Tic Tac Toe Assignment  02-12-2015
Skeleton Program 

Upload the solution to your GIT repository into directory called coursework9
Note that you will need to implement several functions and design the state machine.
 
Task 1

Study the program skeleton and derive the automaton specifying the desirable interactions
involving NewGame and UserMove on the basis of the three functions init, myTurn, and
userTurn. (Notice that the automaton may have epsilon transitions.)

Extend the program skeleton with implementations of the following functions:
 fullRow: Game -> bool. This function is used to test whether a player has
won, that is, there is a full row
 myMove: Game -> Move. This function returns the computer's move for a given state
of the game, by use of the strategy described above.
 performMove: Game -> Move -> Game. This function performs a move (i; n) in a
game state g.
You can now play Tic Tac Toe: enter a game in a text box with predefined board
and engage in alternating move events with the computer until a winner is found.


Task 2

So far it is just two buttons: NewGame and Move, that have an effect.
Extend your automaton and program so that the user gets an option to quit an unfinished
game by pressing the Quit button.

When the user enters an ill-formed move, the program writes "illegal input" in the text box
statusBox. This text box is not reset properly in the program and the text will remain
even after the user have entered a well-formed move. Solve this problem.
*)



// Prelude
open System 
open System.Net 
open System.Threading 
open System.Windows.Forms 
open System.Drawing 

// Async event queue
type AsyncEventQueue<'T>() = 
    let mutable cont = None 
    let queue = System.Collections.Generic.Queue<'T>()
    let tryTrigger() = 
        match queue.Count, cont with 
        | _, None -> ()
        | 0, _ -> ()
        | _, Some d -> 
            cont <- None
            d (queue.Dequeue())

    let tryListen(d) = 
        if cont.IsSome then invalidOp "multicast not allowed"
        cont <- Some d
        tryTrigger()

    member x.Post msg = queue.Enqueue msg; tryTrigger()
    member x.Receive() = 
        Async.FromContinuations (fun (cont,econt,ccont) -> 
            tryListen cont)



// The window part
let window =
  new Form(Text="Play TicTacToe", Size=Size(800,650))

let infoBox =
  new TextBox(Location=Point(50,25),Size=Size(400,25))

let inputBox =
  new TextBox(Location=Point(50,75),Size=Size(400,25))

let statusBox = 
   new TextBox(Location=Point(50,200),Size=Size(400,25))

let gameBox = 
  new TextBox(Location=Point(50,275),Size=Size(400,370))

gameBox.Multiline <- true
gameBox.AcceptsReturn <- true


let newGameButton =
  new Button(Location=Point(50,115),MinimumSize=Size(100,50),
              MaximumSize=Size(100,50),Text="NEW GAME")

let moveButton =
  new Button(Location=Point(200,115),MinimumSize=Size(100,50),
              MaximumSize=Size(100,50),Text="MOVE")

let quitButton =
  new Button(Location=Point(350,115),MinimumSize=Size(100,50),
              MaximumSize=Size(100,50),Text="QUIT")

//let fetchButton =
//  new Button(Location=Point(500,115),MinimumSize=Size(100,50),
//              MaximumSize=Size(100,50),Text="FETCH")

let cancelButton = 
  new Button(Location=Point(650,115),MinimumSize=Size(100,50),
              MaximumSize=Size(100,50),Text="CANCEL")

let disable bs = 
    for b in [newGameButton;moveButton;quitButton;cancelButton] do 
        b.Enabled  <- true
    for (b:Button) in bs do 
        b.Enabled  <- false

// No changes is needed above this comment

type XO = X | O

type Game = XO option []     // We assume that the rows of tic tac toe are represented in sequence in the array. The length is 9.
                             // No tokens on the board is represented by an array containing None values.

type Move = XO * int  // Places X or O in the appropriate position

type Player = | You   // the user
              | Me    // the computer


type Message = | NewGame of string     // a string of n>0 integers "  I1    I2     ...  In  
               | Quit                  // Give up current game
               | UserMove of string    // String with two integers for a move indicating the coordinate of the 3x3 cell
               | Fetch                 // Fetch a game
               | Cancel                // to cancel a download
               ;;  



// fullRow: Game -> bool
// checks if there is a row containing all same tokens                         
// Should be implemented


// myMove: Game -> Move
// Should be implemented

// performMove: Game -> Move -> Game
// Should be implemented


// gameToString: Game -> string                           
let gameToString (g: Game) = 
   let cellToStr (s,i) c = 
      let i' = (i + 1) % 3
      let nl = if i'<i then Environment.NewLine else "" 
      match c with
      | None   -> (s + " "+nl,i')
      | Some m -> (s + string m + nl,i')
   fst (Array.fold cellToStr ("",0) g)



//let stra = Array.mapi (fun i n -> match string i + ": " + string n) g
//                             String.concat "  ;  " stra;; 
                              
// moveToString: Move -> string
let moveToString (h,n) = let str = string h + "  " + string n
                         function 
                         | You -> "Your move: " + str
                         | Me  -> "My move  : " + str  

// isIntegerString: string -> bool
let isIntegerString str = String.forall Char.IsDigit str

let xOrO (g: Game) = let i = Array.fold (fun i c -> 
                       match c with | Some X -> i + 1 | Some O -> i - 1 | _ -> i ) 0 g
                     if i <= 0 then X else O

// moveOfString: Game -> string -> Move option
let moveOfString (g: Game) (str:string) =               
               let stra = str.Split([|' '|],System.StringSplitOptions.RemoveEmptyEntries)
               if stra.Length = 2 && isIntegerString(stra.[0]) && isIntegerString(stra.[1])
               then let (h,n) = (xOrO g,(int stra.[0]) * (int stra.[1])-1)
                    if n >= 0 || n<g.Length then
                        match g.[n] with
                        | None -> Some (h,n)
                        | _ -> None 
                    else None
               else None;;

// gameOfString: string -> Game option
let gameOfString (str:string) = Some [|None;None;None;None;None;None;None;None;None|]

// printMove: Game -> Move -> Player -> unit
let printMove (g: Game) mv player = gameBox.AppendText ("\r\n" + gameToString g + "    " + moveToString mv player) 


let ev = AsyncEventQueue()                                                         

// init: unit -> Async(unit) 
// myTurn: Game -> Async(unit)
// yourTurn: Game -> Async(unit)
let rec init() = 
   async{infoBox.Text   <- "New game: state size of the board"
         inputBox.Text  <- ""
         gameBox.Text   <- ""
         disable [moveButton; quitButton; cancelButton]

         let! msg = ev.Receive()
         match msg with           
         | NewGame str -> match gameOfString str with
                          | Some g -> statusBox.Text <- ""
                                      gameBox.Text <- gameToString g
                                      return! userTurn g
                          | None   -> return! init() 

         | _           -> failwith "unexpected message"}
                                                        
and myTurn g = 
   async{let mv = myMove g
         let g' = performMove g mv
         printMove g' mv Me 
         if fullRow g' then 
             statusBox.Text <- "I won"
             return! init()
         else
             return! userTurn g'}
                        
and userTurn g = 
   async{infoBox.Text <- "state where you would like to place your token"
         inputBox.Text <- ""
         disable [newGameButton; cancelButton]

         let! msg = ev.Receive()
         match msg with
         | UserMove str -> match moveOfString g str with 
                           | Some mv -> let g' = performMove g mv 
                                        printMove g' mv You
                                        if fullRow g' then
                                            statusBox.Text <- "You won"
                                            return! init()
                                        else return! myTurn g'
                           | None    -> statusBox.Text <- "illegal input"
                                        return! userTurn g                                                        
         | _           -> failwith "unexpected message"};;


// No changes will be needed below this comment                                                                 
// Initialization
window.Controls.Add infoBox
window.Controls.Add inputBox
window.Controls.Add statusBox
window.Controls.Add gameBox
window.Controls.Add newGameButton
window.Controls.Add moveButton
//window.Controls.Add fetchButton
window.Controls.Add quitButton
window.Controls.Add cancelButton 
cancelButton.Click.Add (fun _ -> ev.Post Cancel)
newGameButton.Click.Add (fun _ -> ev.Post (NewGame inputBox.Text))
//fetchButton.Click.Add (fun _ -> ev.Post Fetch)
moveButton.Click.Add (fun _ -> ev.Post (UserMove inputBox.Text))
quitButton.Click.Add (fun _ -> ev.Post Quit)

// Start
Async.StartImmediate (init())
window.Show()    
