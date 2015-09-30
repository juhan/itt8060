(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 3: User defined types

  ------------------------------------
  Name:
  TUT Student ID:
  ------------------------------------


  Answer the questions below.  You answers to questions 1--5 should be
  correct F# code written after the question. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere.

  This coursework will be graded. It has to be submitted to the TUT
  git system using the instructions on the course web page by October 9, 2015.
*)

// 1. Make a function rearrange: Fexpr -> Fexpr that will rearrange a finite expression tree (as defined in
// the lecture in the differentiation example) in such a way that constants always are to the left of variables
// in addition and multiplication and the variables with higher power in multiplication are pushed to the right.

// 2. Make a function simplify: Fexpr -> Fexpr that will simplify a finite expression tree by removing
// terms that evaluate to zero.

// Given the type definition:
// type BList =
//  | BEmpty
//  | Snoc of BList * int
// 
// 3. Make the function filterB: (prop: int -> bool) BList -> BList that will return a list for the elements of which
// the function prop returns true.

// 4. Make the function mapB: (trans: int -> int) BList -> BList that will return a list where the function trans has
// been applied to each element.

// Given the type definition
// type Tree =
//  | Nil
//  | Branch2 of Tree * int * Tree
//  | Branch3 of Tree * int * Tree * int * Tree
// 
// 5. Define the value exampleTree : Tree that represents the following
//    tree:
//
//        2
//       / \
//      *  3 5
//        / | \
//       *  *  *

// 6. Define a function sumTree : Tree -> int that computes the sum of
//    all labels in the given tree.

// 7. Define a function productTree : Tree -> int that computes the
//    product of all labels in the given tree. If this function
//    encounters a label 0, it shall not look at any further labels, but
//    return 0 right away.

// ** Bonus questions **

// 8. Define a function mapTree : (int -> int) -> Tree -> Tree that
//    applies the given function to every label of the given tree.

// 9. Use mapTree to implement a function negateAll : Tree -> Tree that
//    negates all labels of a given tree.
