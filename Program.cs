using System;
using System.Collections.Generic;
using System.Text;

namespace MulitList_Starter
{
    //Phong Nguyen
    //BIT 143
    //Assignment 3.2



    //Line 935:                AllTests tester = new AllTests();
    //This line prevented the program from running. 
    //In order to fix this, create a class called AllTests and the method RunTests()
    //The AllTests method needs code to be added for it to function, otherwise it can be 
    //kept empty to run the program.
    //If I encountered a similar problem in a project that I am working with a team,
    //I would discuss it with my team members and find a solution to the problem.
    //We would also discuss ways of working around the problem, before we fully address it.
    //Also, using the debugger will help find where the error occurs in the program.
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
           //MultiLinkedListOfBooks mlb;
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
                        AllTests tester = new AllTests(); //Error occurs here if there's no AllTests Class
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
            if (ec == ErrorCode.DuplicateBook)
                Console.WriteLine("Book is already in the list! Please add a different book");
            if( ec == ErrorCode.OK)
                Console.WriteLine("Book Successfully Added!"); 
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
            if(ec == ErrorCode.OK)
                Console.WriteLine("Book Successfully Removed!");
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
        Book firstAuthor; //First Pointer
        Book firstTitle; //Second Pointer
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
                if (string.Compare(author, otherBook.author, true) == 0)
                    return string.Compare(title, otherBook.title, true);
                return string.Compare(author, otherBook.author, true); //Non-case sensitive
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
                if (string.Compare(title, otherBook.title, true) == 0)
                    return string.Compare(author, otherBook.author, true);
                return string.Compare(title, otherBook.title, true); //Non-case sensitive
            }
        }

        public ErrorCode Add(string author, string title, double price)
        {
            bool pA = false;
            bool pT = false;
            Book cur = new Book(author, title, price); //Creates new node
            if(firstAuthor == null || firstAuthor.CompareByAuthor(cur) == 1) //Checks if author list is empty and checks if *cur* book goes first
            {
                cur.NextAuthor = firstAuthor; //Similar to InsertInFront
                firstAuthor = cur;
                pA = true;
            }
            else if(firstAuthor.CompareByAuthor(cur) == 0) //Checks for same author in author list, if true compare by titles           
                return ErrorCode.DuplicateBook;

            


            if (firstTitle == null || firstTitle.CompareByTitle(cur) == 1) //Checks if title list is empty and checks if *cur* book goes first
            {
                cur.NextTitle = firstTitle; //Similar to InsertInFront
                firstTitle = cur;
                pT = true;
            } 
            else if(firstTitle.CompareByTitle(cur) == 0) //Checks for same title in title list, if true compare by authors            
                return ErrorCode.DuplicateBook;
            



            if (firstAuthor.NextAuthor == null || firstTitle.NextTitle == null) //Checks for a second book, if not, it will add one. If *cur* book goes after first book, 
            {                                                                   // it'll add it to the second position. If *cur* book goes before first book, above condition would be true, 
                if (pA == false && firstAuthor.CompareByAuthor(cur) == -1)      // so that the program should only reach this line if compare method returns -1 for second book. 
                    firstAuthor.NextAuthor = cur;
                if (pT == false && firstTitle.CompareByTitle(cur) == -1)
                    firstTitle.NextTitle = cur;
                return ErrorCode.OK;
            }



            if(pA != true) //Adding the third book and more
            {                                                                                     //Already checked the first two books with above conditions
                Book temp = firstAuthor;
                while(temp != null)
                {
                    if (temp.NextAuthor.CompareByAuthor(cur) == -1)   //if the new book goes after current *temp* book
                    {
                        if (temp.NextAuthor.NextAuthor == null)
                        {
                            temp.NextAuthor.NextAuthor = cur;
                            break;
                        }
                    }
                    else if (temp.NextAuthor != null && temp.NextAuthor.CompareByAuthor(cur) == 1) 
                    {
                        cur.NextAuthor = temp.NextAuthor;
                        temp.NextAuthor = cur;
                        break;
                    }
                    else if(temp.NextAuthor != null && temp.NextAuthor.CompareByAuthor(cur) == 0) 
                            return ErrorCode.DuplicateBook; //Author is the same and title are the same. Therefor returns duplicate book error code
                    temp = temp.NextAuthor;
                }
            }



            if(pT != true)
            {
                Book temp = firstTitle;
                while (temp != null) //Similar as the above loop but with title list instead
                {
                    if (temp.NextTitle.CompareByTitle(cur) == -1)
                    {
                        if (temp.NextTitle.NextTitle == null)
                        {
                            temp.NextTitle.NextTitle = cur;
                            break;
                        }
                    }
                    else if (temp.NextTitle != null && temp.NextTitle.CompareByTitle(cur) == 1)
                    {
                        cur.NextTitle = temp.NextTitle;
                        temp.NextTitle = cur;
                        break;
                    }
                    else if (temp.NextTitle != null && temp.NextTitle.CompareByTitle(cur) == 0)
                            return ErrorCode.DuplicateBook;
                    temp = temp.NextTitle;
                }
            }
            return ErrorCode.OK; 
        }

        public void PrintByAuthor() //Printing using a traversal
        {
            if (firstAuthor == null)
                Console.WriteLine("List is empty!");
            else
            {
                Book temp = firstAuthor;
                while (temp != null)
                {
                    temp.Print();
                    Console.WriteLine(""); //Space
                    temp = temp.NextAuthor;
                }
            }
        }
        public void PrintByTitle() //Printing using a traversal
        {
            if (firstTitle == null)
                Console.WriteLine("List is empty!");
            else
            {
                Book temp = firstTitle;
                while (temp != null)
                {
                    temp.Print();
                    Console.WriteLine(""); //Space
                    temp = temp.NextTitle;
                }
            }
        }

        public ErrorCode Remove(string author, string title)
        {
            if (firstAuthor == null || firstTitle == null)
                return ErrorCode.BookNotFound;
            else
            {
                Book temp = firstAuthor;
                int skip = 0;
                if (firstAuthor.author == author && firstAuthor.title == title) //Checks Author list and if found, removes the book
                {
                    firstAuthor = firstAuthor.NextAuthor;
                }
                else
                {
                    while (temp != null)
                    {
                        if (temp.NextAuthor != null)
                        {
                            if (temp.NextAuthor.author == author && temp.NextAuthor.title == title)
                            {
                                temp.NextAuthor = temp.NextAuthor.NextAuthor;
                                skip = 1;
                                break;
                            }
                        }
                        temp = temp.NextAuthor;
                    }
                    if (skip == 0)
                        return ErrorCode.BookNotFound;                     
                }
                if (firstTitle.author == author && firstTitle.title == title) //Checks Title list and if found, removes the book
                {
                    firstTitle = firstTitle.NextTitle;
                    return ErrorCode.OK;
                }
                else
                {
                    temp = firstTitle; //temp is now equal to first title
                    while(temp != null)
                    {
                        if(temp.NextTitle != null)
                        {
                            if(temp.NextTitle.author == author && temp.NextTitle.title == title) 
                            {
                                temp.NextTitle = temp.NextTitle.NextTitle;
                                return ErrorCode.OK;
                            }
                        }
                        temp = temp.NextTitle;
                    }
                }
            }
            return ErrorCode.BookNotFound;
        }
    }
}
