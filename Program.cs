namespace Task_4
{
    public class Book
    {
        public Book(string title, string author, string iSBN)
        {
            Title = title;
            Author = author;
            //Splitted author and title into words
            //Because searching or borrowing or returning methods can take only word as a parameter
            //according to the given test cases by Eng.Mohammed Nabih
            TitleSt = new List<string>();
            AuthorSt = new List<string>();
            string currentWord = "";
            for (int i = 0; i < title.Length; i++)
            {
                if (title[i] != ' ')
                {
                    currentWord += title[i];

                }
                else
                {
                    TitleSt.Add(currentWord);
                    currentWord = "";
                }
            }
            //Last word has no spaces after so we add it lastly
            if (currentWord.Length > 0)
                TitleSt.Add(currentWord);

            currentWord = "";
            for (int i = 0; i < author.Length; i++)
            {
                if (author[i] != ' ')
                {
                    currentWord += author[i];

                }
                else
                {
                    AuthorSt.Add(currentWord);
                    currentWord = "";
                }
            }
            //Last word has no spaces after so we add it lastly
            if (currentWord.Length > 0)
                AuthorSt.Add(currentWord);

            ISBN = Convert.ToUInt64(iSBN);
        }

        //Automatic Properties
        public bool Availability { get; set; } = true;
        public string Title { get; private set; }
        public string Author { get; private set; }
        public UInt64 ISBN { get; private set; }
        //Manually made their Automatic Property to be able to read them and search properly
        public List<string> TitleSt { get; private set; }
        public List<string> AuthorSt { get; private set; }
    }
    public class Library
    {
        public Library()
        {

        }
        //When library is constructed it starts out empty of books
        public List<Book> Books { get; set; } = new List<Book>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }
        public int SearchBook(string s)
        {
            if (Books.Count == 0)
                return -1;
            else
                for (int i = 0; i < Books.Count; i++)
                {
                    int j = Books[i].TitleSt.Count;
                    for (int z = 0; z < j; z++)
                        if (s == Books[i].TitleSt[z])
                            return i;
                    int h = Books[i].AuthorSt.Count;
                    for (int z = 0; z < h; z++)
                        if (s == Books[i].AuthorSt[z])
                            return i;
                }
            return -1;
        }
        public int BorrowBook(string s)
        {
            int booksIndex = SearchBook(s);
            if (booksIndex != -1)
            {
                if (Books[booksIndex].Availability)
                {
                    Books[booksIndex].Availability = false;
                    return booksIndex;
                }
                else
                    return -2;
            }
            return -1;
        }
        public int ReturnBook(string s)
        {
            int booksIndex = SearchBook(s);
            if (booksIndex != -1)
            {
                if (!Books[booksIndex].Availability)
                {
                    Books[booksIndex].Availability = true;
                    return booksIndex;
                }
                else
                    return -2;
            }
            return -1;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            // Adding books to the library
            library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565"));
            library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
            library.AddBook(new Book("1984", "George Orwell", "9780451524935"));


            // Searching and borrowing books
            Console.WriteLine("Searching and borrowing books...");
            //String Test Cases
            string s1 = "Gatsby", s2 = "1984", s3 = "Harry Potter", s4 = "Lee";

            int b1 = library.BorrowBook(s1);
            if (b1 == -1)
                Console.WriteLine($"'{s1}' isn't added to library");
            else if (b1 == -2)
                Console.WriteLine($"'{s1}' isn't currently available");
            else
                Console.WriteLine($"'{library.Books[b1].Title}' has been borrowed successfully");
            //-------------------------------------------------------------------
            b1 = library.BorrowBook(s2);
            if (b1 == -1)
                Console.WriteLine($"'{s2}' isn't added to library");
            else if (b1 == -2)
                Console.WriteLine($"'{s2}' isn't currently available");
            else
                Console.WriteLine($"'{library.Books[b1].Title}' has been borrowed successfully");
            //-------------------------------------------------------------------
            b1 = library.BorrowBook(s3); // This book is not in the library
            if (b1 == -1)
                Console.WriteLine($"'{s3}' isn't added to library");
            else if (b1 == -2)
                Console.WriteLine($"'{s3}' isn't currently available");
            else
                Console.WriteLine($"'{library.Books[b1].Title}' has been borrowed successfully");
            //-------------------------------------------------------------------
            b1 = library.BorrowBook(s2);
            if (b1 == -1)
                Console.WriteLine($"'{s2}' isn't added to library");
            else if (b1 == -2)
                Console.WriteLine($"'{s2}' isn't currently available");
            else
                Console.WriteLine($"'{library.Books[b1].Title}' has been borrowed successfully");
            //-------------------------------------------------------------------
            b1 = library.BorrowBook(s4);
            if (b1 == -1)
                Console.WriteLine($"'{s4}' has no added books to library");
            else if (b1 == -2)
                Console.WriteLine($"{s4}'s book isn't currently available");
            else
                Console.WriteLine($"'{library.Books[b1].Title}' has been borrowed successfully");
            //-------------------------------------------------------------------
            b1 = library.BorrowBook(s4);
            if (b1 == -1)
                Console.WriteLine($"'{s4}' has no added books to library");
            else if (b1 == -2)
                Console.WriteLine($"{s4}'s book isn't currently available");
            else
                Console.WriteLine($"'{library.Books[b1].Title}' has been borrowed successfully");
            //-------------------------------------------------------------------
            Console.WriteLine("\n");
            //-------------------------------------------------------------------
            // Returning books
            Console.WriteLine("\nReturning books...");
            int r1 = library.ReturnBook(s1);
            if (r1 == -1)
                Console.WriteLine($"'{s1}' isn't added to library");
            else if (r1 == -2)
                Console.WriteLine($"'{s1}' is already in the library");
            else
                Console.WriteLine($"'{library.Books[r1].Title}' has been returned successfully");
            //-------------------------------------------------------------------
            r1 = library.ReturnBook(s3);
            if (r1 == -1)
                Console.WriteLine($"'{s3}' isn't added to library");
            else if (r1 == -2)
                Console.WriteLine($"'{s3}' is already in the library");
            else
                Console.WriteLine($"'{library.Books[r1].Title}' has been returned successfully");
            //-------------------------------------------------------------------
            r1 = library.ReturnBook(s1);
            if (r1 == -1)
                Console.WriteLine($"'{s1}' isn't added to library");
            else if (r1 == -2)
                Console.WriteLine($"'{s1}' is already in the library");
            else
                Console.WriteLine($"'{library.Books[r1].Title}' has been returned successfully");
            //-------------------------------------------------------------------
            r1 = library.ReturnBook(s4);
            if (r1 == -1)
                Console.WriteLine($"'{s4}' has no books added to library");
            else if (r1 == -2)
                Console.WriteLine($"{s4}'s book is already in the library");
            else
                Console.WriteLine($"'{library.Books[r1].Title}' has been returned successfully");
            //-------------------------------------------------------------------
            r1 = library.ReturnBook(s4);
            if (r1 == -1)
                Console.WriteLine($"'{s4}' has no books added to library");
            else if (r1 == -2)
                Console.WriteLine($"{s4}'s book is already in the library");
            else
                Console.WriteLine($"'{library.Books[r1].Title}' has been returned successfully");
        }
    }
}
