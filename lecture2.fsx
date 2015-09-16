(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 2: values, functions, tuples and lists

  Juhan Ernits

  Material based on chapter 3 of RWFP

*)

let number = 2015
printfn "%d" number
let message = "Answer: " + number.ToString()
printfn "%s" message

let number2 = 2015 in
(
  printfn "%d" number2
  let message = "Answer: " + number2.ToString() in printfn "%s" message
)

let multiply num1 num2 = num1 * num2

let multiply2 (num1:float) (num2:int) : float = num1 * float num2

let multiply3 (num1:float) (num2:int) : int = int num1 * num2

let tmpfn = multiply3 2.0

5 |> tmpfn
// is equivalent to
tmpfn 5

let printSquares message (num1:int) num2 =
    let printSquareUtility num =
      let square = num * num
      printfn "%s %d: %d" message num square
      //()
      //square
    printSquareUtility num1
    printSquareUtility num2

printSquares "Square of: " 24 42

let n1 = 24

//does not work
n1 <- 25

let mutable n2 = 24
//works now
n2 <- 25

n2

let tp = "Hello world", 55

let tp2 = 54, 55, 56, "Hello world"



let prague = "Prague", 1188126
let seattle = "Seattle", 594210

let printCity cityInfo = 
    printfn "Population of %s is %d." (fst cityInfo) (snd cityInfo)

printCity prague
printCity seattle

let withItem2 newItem2 tuple = 
    let originalItem1, originalItem2 = tuple
    originalItem1, newItem2

withItem2 1188127 prague

let withItem2 newItem2 tuple = 
    let originalItem1, _ = tuple
    originalItem1, newItem2

let withItem2 newItem2 (originalItem1, _) = originalItem1, newItem2

let withItem2 newItem2 tuple =
   match tuple with
   | originalItem1, _ -> originalItem1, newItem2

withItem2 1 prague 

let setPopulation tuple newPopulation =
   match tuple with
   | "New York", _ -> "New York", newPopulation + 100
   | cityName , _ -> cityName, newPopulation
   
let newyork = "New York", 718000

setPopulation prague 500000
setPopulation newyork 500000

let setPopulation newPopulation =
   function
   | "New York", _ -> "New York", newPopulation + 100
   | cityName , _ -> cityName, newPopulation

