(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Bonus Coursework A: Simpification of symbolic terms

  ------------------------------------
  Name:
  TUT Student ID:
  ------------------------------------


  Answer the questions below.  You answers to questions 1--7 should be
  correct F# code written after the question. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere.

  This coursework will not be graded, but providing a solution to the problem may result
  in some additional points. It has to be submitted to the TUT
  git system using the instructions on the course web page by the end of October 2015.
*)



// 1. Make a function simplify: Fexpr -> Fexpr that will simplify a finite expression tree to a
//    simplest possible form
//
// For example:
// Add (Const 0.0, X) -> X
// Mul (Const 1.0, X) -> X
//Add
//    (Mul (Const 0.0,Mul (X,Mul (X,X))),
//     Mul
//       (Const 2.0,
//        Add
//          (Mul (Const 1.0,Mul (X,X)),
//           Mul (X,Add (Mul (Const 1.0,X),Mul (Const 1.0,X))))))
// ->
//     Mul
//       (Const 6.0,Mul (X,X))

// 2. Add support to represent fraction constants to Fexpr and modify the definition of D, and simplify accordingly.