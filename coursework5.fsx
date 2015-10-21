(*

  ITT8060 -- Advanced Programming 2015
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 5: Records, List.collect and Charting

  ------------------------------------
  Name:
  Student ID:
  ------------------------------------


  Answer the questions below. You answers to the questions should be
  correct F# code written after the question. This file is an F# script
  file; it should be possible to load the whole file at once. If you
  can't, then you have introduced a syntax error somewhere.

  This coursework will be graded.

  Commit and push your solution to the repository as file
  coursework5.fsx in directory coursework5. The downloaded data should go in
  atoms.xml in the coursework5 directory.

  The deadline for completing the above procedure is Friday,
  October 30, 2014.

  We will consider the submission to be the latest version of the
  appropriate files in the appropriate directory before the deadline
  of a particular coursework.


*)

// 1) Define a record type with the name FileMetaData representing the name of the file and the size of it. 

// 2) Make a function that takes an array returned by Directory.GetFiles 
//    and produces a list of instances of the record type defined in Q 1.

// 3) Make a function called getFileMetadata of type string list -> FileMetaDataList
//    that takes a list of directories as strings and returns a list of FileMetaData records of
//    all files contained in the directories.

// 4) Make a function that displays a histogram chart showing the distribution of
//    the file sizes given a list of FileMetaData records.