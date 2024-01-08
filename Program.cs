using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

class Program
{
    public static void Main(string[] args)
    {
        Library library= new Library();

        library.SearshBook("Bricolage");
        library.SearshBook(003).ReadThisBook();
        library.PurchaseBook(002);
        library.AdminConnect("Admi", "AdminOfTheLibrary");
        library.ModifyBook(001);
        library.AdminConnect("Admin", "AdminOfTheLibrary");
        library.ModifyBook(001);
        library.AddBook("New book", "Romain", "Aucunne", 60, 30);
        library.ShowLibrary();
        library.ModifyBook(001);
    }
}
class Library
{
    private bool isAdminConnected = false;
    private List<(string, string)> adminUserNamesPasswords = new List<(string, string)> {("Admin", "AdminOfTheLibrary")};

    private List<Book> books = new List<Book>
    {
        new Book("La maison", "Bricolage & Co", "Bricolage", 200, 20, 001),
        new Book("Le garage", "Bricolage & Co", "Bricolage", 110, 20, 002),
        new Book("Déveloper en C#", "Apprendre à coder", "Programation", 150, 15, 003),
    };

    private void ShowOneBook(Book book)
    {
        Console.WriteLine("-------------------------------------------------------------------------------");
        Console.WriteLine("Name : " + book.Title);
        Console.WriteLine("Author : " + book.Author);
        Console.WriteLine("Catégorie : " + book.Category);
        Console.WriteLine("Nb Pages" + book.NbPages);
        Console.WriteLine("Price : " + book.Price);
        Console.WriteLine("ID" + book.Id);
    }
    private int FindHigerId()
    {
        int higerId = 0;
        foreach(Book book in books)
        {
            if(book.Id > higerId)
            {
                higerId = book.Id;
            }
        }
        return higerId;
    }
    public void ShowLibrary()
    {
        foreach (Book book in books)
        {
            ShowOneBook(book);
        }
    }
    public void SearshBook(string titleAuthorOrCategory)
    {
        bool isBookFind = false;
        Console.WriteLine("Résultat de la recherche");
        foreach(Book book in books)
        {
            if(titleAuthorOrCategory == book.Title || titleAuthorOrCategory == book.Author || titleAuthorOrCategory == book.Category)
            {
                ShowOneBook(book);
                isBookFind = true;
            } 
        }
        if(!isBookFind)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Aucun résultat de recherche");
        }
    }

    public Book SearshBook(int id)
    {
        bool isBookFind = false;
        Console.WriteLine("Résultat de la recherche");
        foreach(Book book in books)
        {
            if (book.Id == id)
            {
                isBookFind = true;
                ShowOneBook(book);
                return book;
            }
        }
        if(!isBookFind)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Aucun résultat de recherche");
        }
        return null;
    }

    public void PurchaseBook(int id)
    {
        foreach(Book book in books)
        {
            if(book.Id == id)
            {
                books.Remove(book);
                Console.WriteLine("Vous avez acheté \"" + book.Title + "\" pour " + book.Price + "€");
                break;
            }
        }
    }

    public void AdminConnect(string UserName, string Password)
    {
        foreach((string, string) userNamePassword in adminUserNamesPasswords)
        {
            if(userNamePassword.Item1 == UserName && userNamePassword.Item2 == Password)
            {
                isAdminConnected = true;
                Console.WriteLine("Connexion réussie");
            }
        }
        if(!isAdminConnected)
        {
            Console.WriteLine("Connexion échouée");
        }
    }

    public void AdminDeconnect()
    {
        isAdminConnected = false;
        Console.WriteLine("Déconnexion réussie");
    }

    public void AddBook(string title, string author, string category, int price, int nbPages)
    {
        if(isAdminConnected)
        {
            Book book= new Book(title, author, category, nbPages, price, FindHigerId() + 1);
            books.Add(book);
        }
        else 
        {
            Console.WriteLine("Vous n'êtes pas connectés en tant qu'admin");
        }
    }

    public void RemoveBook(int id)
    {
        if(isAdminConnected)
        {
            foreach(Book book in books)
            {
                if(book.Id == id)
                {
                    books.Remove(book);
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Vous n'êtes pas connectés en tant qu'admin");
        }
    }

    public void ModifyBook(int id)
    {
        if(isAdminConnected)
        {
            foreach(Book book in books)
            {
                if(book.Id == id)
                {
                    Console.Write("Voulez vous modifier \"" + book.Title + "\" (y N)");
                    string answer = Console.ReadLine();
                    if (answer == "y")
                    {
                        Console.Write("Que voulez vous modifier Titre(T), Auteur(A), Categorie(C), Prix(P), Nb Pages(N)");
                        string answer5 = Console.ReadLine();
                        switch (answer5)
                        {
                            case "T" :
                                Console.Write("Nouveau titre : ");
                                string newTitle = Console.ReadLine();
                                book.Title = newTitle;
                                Console.WriteLine("Le titre est maintenant \"" + newTitle + "\"");
                                break;
                            case "A" :
                                Console.Write("Nouvel auteur : ");
                                string newAuthor = Console.ReadLine();
                                book.Author= newAuthor;
                                Console.WriteLine("L'auteur est maintenant \"" + newAuthor + "\"");
                                break;
                            case "C" :
                                Console.Write("Nouvelle catégorie : ");
                                string newCategory = Console.ReadLine();
                                book.Category = newCategory;
                                Console.WriteLine("La catégorie est maintenant \"" + newCategory + "\"");
                                break;
                            case "P" :
                                Console.Write("Nouveau prix : ");
                                string newPrice = Console.ReadLine();
                                book.Price = int.Parse(newPrice);
                                Console.WriteLine("Le prix est maintenant \"" + newPrice + "\"");
                                break;
                            case "N" :
                                Console.Write("Nouveau nombre de pages : ");
                                string newNbPages = Console.ReadLine();
                                book.NbPages = int.Parse(newNbPages);
                                Console.WriteLine("Le nombre de pages est maintenant \"" + newNbPages + "\"");
                                break;
                            default:
                                Console.WriteLine("Rien n'a été changé");
                                break;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Au revoir !");
                        return;
                    }

                }
            }
            Console.Write("Voulez vous en modifer un autre (y N)");
            string answer2 = Console.ReadLine();
            if (answer2 == "y")
            {
                Console.Write("Identifiant du livre : ");
                string answer3 = Console.ReadLine();
                ModifyBook(int.Parse(answer3));
            }
            else
            {
                Console.WriteLine("Au revoir !");
                return;
            }
            }
        else
        {
            Console.WriteLine("Vous n'êtes pas connecté en tant qu'admin");
        }
    }
}

class Book
{
    public string Title{get; internal set;}
    public string Author{get; internal set;}
    public string Category{get; internal set;} 
    public int NbPages{get; internal set;}
    public int Id{get; internal set;} 
    public int Price{get; internal set;}

    public Book(string title, string author, string category, int nbPages, int price, int id)
    {
        Title = title;
        Author = author;
        Category = category;
        NbPages = nbPages;
        Price = price;
        Id = id;
    }

    public void ReadThisBook()
    {
        Console.WriteLine("Vous lisez " + Title + " de " + Author + ". Il vous reste " + NbPages + " à lire");
    }
}
