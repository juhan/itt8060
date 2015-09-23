(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 2: Operations on lists, unit of measure

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
  git system using the instructions on the course web page by October 2, 2015.
*)

// 1. Make a value sl containing empty list of type string list.

// 2. Make a function shuffle: int list -> int list that rearranges the elements of the argument list
// in such a way that the first value goes to first, last to second,
// second to third, last but one to fourth etc.
// E.g.
// shuffle [] -> []
// shuffle [1;2] -> [1;2]
// shuffle [1..4] -> [1;4;2;3]

// 3. Make a function segments: int list -> int list list that splits the list passed
// as an argument into list of lists of nondecreasing segments.
// The segments need to be of maximal possible length (the number of segments
// needs to be minimal)
// E.g.
// segments [] ->  []
// segments [1] -> [[1]]
// segments [3;4;5;5;1;2;3] -> [[3;4;5;5];[1;2;3]]


// 4. Write a function convertSpeeds: float<miles/hour> list -> float<km/hour> list that
// converts all values in mph in the initial list to km/h in the returned list.

// 5. Write a function that from a list of values in degrees Celsius filters
// out values that exceed a predefined limit.
