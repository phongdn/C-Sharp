using System;
using System.Collections.Generic;
using System.Text;

namespace MulitList_Starter
{
    //Line 73:                AllTests tester = new AllTests();
    //This line prevented the program from running. 

    class Program
    {
        static void Main(string[] args)
        {
            (new UserInterface()).RunProgram();

            // Or, you could go with the more traditional:
            // UserInterface ui = new UserInterface();
            // ui.RunProgram();
        }
    }

    // Bit of a hack, but still an interesting idea....
    enum MenuOptions
    {
        // DO NOT USE ZERO!
        // (TryParse will set choice to zero if a non-number string is typed,
        // and we don't want to accidentally set nChoice to be a member of this enum!)
        QUIT = 1,
        ADD_BOOK,
        PRINT_BY_AUTHOR,
        PRINT_BY_TITLE,
        REMOVE_BOOK,
        RUN_TESTS
    }
    class AllTests
    {
        public void RunTests()
        {
            MultiLinkedListOfBooks mlb;
            
        }
    }
    class UserInterface
    {
        MultiLinkedListOfBooks theList;
        public void RunProgram()
        {
            int nChoice;
            theList = new MultiLinkedListOfBooks();

            do // main loop
            {
                Console.WriteLine("Your options:");
                Console.WriteLine("{0} : End the program", (int)MenuOptions.QUIT);
                Console.WriteLine("{0} : Add a book", (int)MenuOptions.ADD_BOOK);
                Console.WriteLine("{0} : Print all books (by author)", (int)MenuOptions.PRINT_BY_AUTHOR);
                Console.WriteLine("{0} : Print all books (by title)", (int)MenuOptions.PRINT_BY_TITLE);
                Console.WriteLine("{0} : Remove a Book", (int)MenuOptions.REMOVE_BOOK);
                Console.WriteLine("{0} : RUN TESTS", (int)MenuOptions.RUN_TESTS);
                if (!Int32.TryParse(Console.ReadLine(), out nChoice))
                {
                    Console.WriteLine("You need to type in a valid, whole number!");
                    continue;
                }
                switch ((MenuOptions)nChoice)
                {
                    case MenuOptions.QUIT:
                        Console.WriteLine("Thank you for using the multi-list program!");
                        break;
                    case MenuOptions.ADD_BOOK:
                        this.AddBook();
                        break;
                    case MenuOptions.PRINT_BY_AUTHOR:
                        theList.PrintByAuthor();
                        break;
                    case MenuOptions.PRINT_BY_TITLE:
                        theList.PrintByTitle();
                        break;
                    case MenuOptions.REMOVE_BOOK:
                        this.RemoveBook();
                        break;
                    case MenuOptions.RUN_TESTS:
                        AllTests tester = new AllTests();
                        tester.RunTests();
                        break;
                    default:
                        Console.WriteLine("I'm sorry, but that wasn't a valid menu option");
                        break;

                }
            } while (nChoice != (int)MenuOptions.QUIT);
        }

        public void AddBook()
        {
            Console.WriteLine("ADD A BOOK!");

            Console.WriteLine("Author name?");
            string author = Console.ReadLine();

            Console.WriteLine("Title?");
            string title = Console.ReadLine();

            double price = -1;
            while (price < 0)
            {
                Console.WriteLine("Price?");
                if (!Double.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("I'm sorry, but that's not a number!");
                    price = -1;
                }
                else if (price < 0)
                {
                    Console.WriteLine("I'm sorry, but the number must be zero, or greater!!");
                }
            }

            ErrorCode ec = theList.Add(author, title, price);
            if(ec == ErrorCode.DuplicateBook)
                Console.WriteLine("Book is already in the list! Please add a different book");
        }

        public void RemoveBook()
        {
            Console.WriteLine("REMOVE A BOOK!");

            Console.WriteLine("Author name?");
            string author = Console.ReadLine();

            Console.WriteLine("Title?");
            string title = Console.ReadLine();

            ErrorCode ec = theList.Remove(author, title);
            if (ec == ErrorCode.BookNotFound)
                Console.WriteLine("Book not in List!");
        }
    }

    enum ErrorCode
    {
        OK,
        DuplicateBook,
        BookNotFound
    }

    class MultiLinkedListOfBooks
    {
        Book first;
        private class Book
        {
            public Book NextAuthor;
            public Book NextTitle;
            public string author;
            public string title;
            double price;

            public Book(string Newauthor, string Newtitle, double Newprice)
            {
                author = Newauthor;
                title = Newtitle;
                price = Newprice;
            }
            public void Print()
            {
                Console.WriteLine("Book Title: {0}", title);
                Console.WriteLine("Book Author: {0}", author);
                Console.WriteLine("Price: ${0}", price);
            }

            /// <summary>
            /// Compares the parameter, and this book, and determines which one should go
            /// first, in the AUTHOR list
            /// </summary>
            /// <param name="otherBook"></param>
            /// <returns> -1 if this book goes before the otherBook
            ///           0 if they're duplicate books
            ///           +1 if this book goes AFTER the otherBook</returns>
            public int CompareByAuthor(Book otherBook)
            {
                return string.Compare(title,otherBook.title, true);
            }


            /// <summary>
            /// Compares the parameter, and this book, and determines which one should go
            /// first, in the TITLE list
            /// </summary>
            /// <param name="otherBook"></param>
            /// <returns> -1 if this book goes before the otherBook
            ///           0 if they're duplicate books
            ///           +1 if this book goes AFTER the otherBook</returns>
            public int CompareByTitle(Book otherBook)
            {
                    return string.Compare(title, otherBook.title, true);
            }
        }

        public ErrorCode Add(string author, string title, double price)
        {
            Book cur = new Book(author, title, price);
            if (first == null)
                first = cur;
             
            else if (first.NextAuthor==null &&  first.NextTitle == null)
            {
                if (first.CompareByAuthor(cur) == 0 && first.CompareByTitle(cur) == 0)
                {
                    return ErrorCode.DuplicateBook;
                }
                else if (first.CompareByAuthor(cur) == 0)
                {
                    if (first.CompareByTitle(cur) == -1) //Title
                        first.NextAuthor = cur;
                    else
                    {
                        cur.NextAuthor = first;
                        first = cur;
                    }
                    return ErrorCode.OK;
                }
                else if(first.CompareByTitle(cur) == 0)
                {
                    if(first.CompareByAuthor(cur) == -1)
                    {
                        first.NextTitle = cur;
                    }
                    else
                    {
                        cur.NextTitle = first;
                        first = cur;
                    }
                    return ErrorCode.OK;
                }
                if (first.CompareByAuthor(cur) == -1) //Author   
                {                   
                    first.NextAuthor = cur;
                }    
                else
                {
                    cur.NextAuthor = first;
                    if (first.CompareByTitle(cur) == 1)
                        cur.NextTitle = first;
                    first = cur;
                }
                if (first.CompareByTitle(cur) == -1) //Title
                    first.NextTitle = cur;
                return ErrorCode.OK;
            }
            else if (first != null && first.NextAuthor != null)
            {
                Book temp = first;
                Book temp3 = first;
                int numAuthor;
                int numTitle;
                while (temp3 != null)
                {
                    numAuthor = temp.CompareByAuthor(cur); //NumAuthor == comparison for next author in AUTHOR list
                    numTitle = temp.CompareByTitle(cur); //NumTitle == comparison for next title in AUTHOR li
                    if (numAuthor == 0 && numTitle == 0)
                        return ErrorCode.DuplicateBook;
                    temp3 = temp3.NextAuthor;
                }
                while (temp.NextAuthor != null)
                {
                    numAuthor = temp.NextAuthor.CompareByAuthor(cur); //NumAuthor == comparison for next author in AUTHOR list
                    numTitle = temp.NextAuthor.CompareByTitle(cur); //NumTitle == comparison for next title in AUTHOR list
                    if (numAuthor == 1)
                    {
                        cur.NextAuthor = temp.NextAuthor;
                        temp.NextAuthor = cur;
                        return ErrorCode.OK;
                    }
                    if (numAuthor == 0)
                    {
                        if (numTitle == -1)
                        {
                            cur.NextAuthor = temp.NextAuthor.NextAuthor;
                            temp.NextAuthor.NextAuthor = cur;
                        }
                        else
                        {
                            cur.NextAuthor = temp.NextAuthor;
                            temp.NextAuthor = cur;
                        }
                    }
                    else
                    {
                        if (numAuthor == -1)
                        {
                            cur.NextAuthor = temp.NextAuthor.NextAuthor;
                            temp.NextAuthor.NextAuthor = cur;
                        }
                        else
                        {
                            cur.NextAuthor = temp.NextAuthor;
                            temp.NextAuthor = cur;
                        }
                    }
                    temp = temp.NextAuthor;
                }
                Book temp2 = first;
                //while (null != temp2.NextTitle)
                //{
                //    numAuthor = temp2.NextTitle.CompareByAuthor(cur); //NumAuthor == comparison for next author in TITLE list
                //    numTitle = temp2.NextTitle.CompareByTitle(cur); //NumTitle == comparison for next title in TITLE list
                //    if (numTitle == 0) //Technically doesn't need to search for duplicate books because the above loop already did
                //    {
                //        if (numAuthor == -1)
                //        {
                //            cur.NextTitle = temp2.NextTitle.NextTitle;
                //            temp2.NextTitle.NextTitle = cur;
                //        }
                //        else
                //        {
                //            cur.NextTitle = temp2.NextTitle;
                //            temp2.NextTitle = cur;
                //        }
                //    }
                //    else
                //    {
                //        if (numTitle == -1)
                //        {
                //            cur.NextTitle = temp2.NextTitle.NextTitle;
                //            temp2.NextTitle.NextTitle = cur;
                //        }
                //        else
                //        {
                //            cur.NextTitle = temp2.NextTitle;
                //            temp2.NextTitle = cur;
                //        }
                //    }
                //    temp2 = temp2.NextTitle;
                //}
            }
            // ______________________________________________________________________



            //numAuthor = temp.CompareByAuthor(cur);
            //numTitle = temp2.CompareByTitle(cur);
            //if (numAuthor == 0 && numTitle == 0)
            //    return ErrorCode.DuplicateBook;
            ////else if(numAuthor == 0)
            ////{
            ////    if(numTitle == -1)
            ////    {
            ////        cur.NextAuthor = temp.NextAuthor;
            ////        temp.NextAuthor = cur;
            ////    }
            ////    else
            ////    {
            ////        cur.NextAuthor = temp;
            ////        temp = cur;
            ////    }
            ////    break;
            ////}
            //if(numAuthor == -1)
            //{
            //    if(temp.NextAuthor != null && temp.NextAuthor.CompareByAuthor(cur) == 1)
            //    {
            //        cur.NextAuthor = temp.NextAuthor;
            //        temp.NextAuthor = cur;
            //        break;
            //    }
            //    else if(temp.NextAuthor == null)
            //    {
            //        temp.NextAuthor = cur;
            //        break;
            //    }
            //}
            //temp = temp.NextAuthor;



            //Book temp = new Book(author, title, price);
            //if (temp.CompareByAuthor(first) == -1)
            //{
            //    temp.NextAuthor = first;
            //    first = temp;
            //}
            //else
            //{
            //    first.NextAuthor = temp;
            //}
            //This Works ^^^^^^








            //Book temp = first;
            //Book temp2 = first;
            //while (temp!=null) 
            //{
            //    if(temp.CompareByAuthor(cur)==0 && temp.CompareByTitle(cur)==0) //This will check for duplicate books
            //    {
            //        return ErrorCode.DuplicateBook;
            //    }
            //    if (temp.CompareByAuthor(cur) == 0) //Same Author, different Titles
            //    {
            //        if (temp.CompareByTitle(cur) == -1) //This sorts in order of title, because of the same author
            //        {
            //            cur.NextAuthor = temp.NextAuthor;
            //            temp.NextAuthor = cur;
            //        }
            //        else
            //        {
            //            cur.NextAuthor = temp;  //?????????????????????????????????????????
            //            temp = cur;
            //        }
            //        break;
            //    }
            //    else if(temp.CompareByAuthor(cur) == -1)  //This will compare the authors and add the book into the author list in order
            //    {
            //        if(temp.NextAuthor.CompareByAuthor(cur) == 1)
            //        {
            //            cur.NextAuthor = temp.NextAuthor;
            //            temp.NextAuthor = cur;
            //            break;
            //        }
            //    }
            //    temp = temp.NextAuthor;
            //}
            //while(temp2 != null)
            //{
            //    if(temp2.CompareByTitle(cur) == 0)
            //    {
            //        if(temp2.CompareByAuthor(cur) == -1)
            //        {
            //            cur.NextTitle = temp.NextTitle;
            //            temp.NextTitle = cur;
            //        }
            //        else
            //        {
            //            cur.NextTitle = temp; //?????????????????????????????????????????
            //            temp = cur;
            //        }
            //        break;
            //    }
            //    if (temp2.CompareByTitle(cur) == -1) //This will compare the title and add the book into the title list in order
            //    {
            //        if (temp2.NextTitle.CompareByTitle(cur) == 1)
            //        {
            //            cur.NextTitle = temp2.NextTitle;
            //            temp2.NextTitle = cur;
            //        }
            //        temp2 = temp2.NextTitle;
            //    }
            //}

            return ErrorCode.OK;
            
        }

        public void PrintByAuthor()
        {
            if (first == null)
                Console.WriteLine("List is empty!");
            else
            {
                Book temp = first;
                while(temp!=null)
                {
                    temp.Print();
                    Console.WriteLine(""); //Space
                    temp = temp.NextAuthor;
                }
            }
        }
        public void PrintByTitle()
        {
            if (first == null)
                Console.WriteLine("List is empty!");
            else
            {
                Book temp = first;
                while (temp != null)
                {
                    temp.Print();
                    Console.WriteLine(""); //Space
                    temp =temp.NextTitle;
                }
            }
        }

        public ErrorCode Remove(string author, string title)
        {
            // if there isn't an exact match, then do the following:
            return ErrorCode.BookNotFound;
            // (this includes finding a book by the given author, but with a different title,
            // or a book with the given title, but a different author)
        }
    }
}
