using LibraryXP.Data;
using LibraryXP.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LibraryXP
{
    /// <summary>
    /// El objetivo de este programa es generar archivos .json para almacenar en solo aquel archivo los datos de una gestión de Libros, ya sea
    /// con las siguientes clases: Autores, Libros, Préstamos y Usuarios. 
    /// Si bien, esto es básico, está optimizado para utilizar menos recursos posibles.
    /// También está hecho a través de .NET 3.5 para compatibilizar con Windows XP y otros equipos viejos que dependan del Terminal (CLI)
    /// </summary>
    internal class Program
    {
        //Inicializa con datos, generando primera instancia como referencia. Luego se sincronizan cambios.
        static List<Author> authors = AuthorController.GetAuthors();
        static List<Book> books = BookController.GetBooks();
        static List<Loan> loans = LoanController.GetLoans();
        static List<User> users = UserController.GetUsers();
        static void Main(string[] args)
        {
            int input = -1;
            
            while (input != 0) {
                input = ShowMenu();
                Console.Clear();
                bool isEditable = false;
                //Clases
                //Consigue aquellos nuevos si se han actualizado
                authors = AuthorController.GetAuthors();
                books = BookController.GetBooks();
                loans = LoanController.GetLoans();
                users = UserController.GetUsers();
                switch (input)
                {
                    case 1:
                        //Caso Libros
                        if (authors == null || authors.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado Autores. Deben existir para poder gestionar Libros.");
                            Console.ReadLine();
                            break;
                        }

                        if (books == null || books.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado Libros");
                        }
                        else
                        {
                            isEditable = true;
                        }
                        ShowMenuBooks(isEditable);
                        break;
                    case 2:
                        //Caso Autores
                        if (authors == null || authors.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado Autores. Empiece a crear para introducir Libros.");
                            Console.ReadLine();
                        }
                        else
                        {
                            isEditable = true;
                        }
                        ShowMenuAuthors(isEditable);
                        break;
                    case 3:
                        //Caso Usuarios
                        if (users == null || users.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado Usuarios. Empiece a crear para prestar Libros.");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            isEditable = true;
                        }
                        ShowMenuUsers(isEditable);
                        break;
                    case 4:
                        //Caso Préstamos
                        if (users == null || users.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado Usuarios. Empiece a crear para prestar Libros.");
                            Console.ReadLine();
                            break;
                        }
                        else if (books == null || books.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado Libros. Deben haber libros para prestar.");
                            Console.ReadLine();
                            break;
                        }
                        else if (loans == null || loans.Count == 0)
                        {
                            Console.WriteLine("No se han encontrado Préstamos. Empiece a prestar Libros.");
                            Console.ReadLine();
                        }
                        else {
                            isEditable = true;
                        }
                            ShowMenuLoans(isEditable);
                        break;
                }
            }

            ///Finalmente hacemos esto para salir del programa
            Console.WriteLine("Presione una tecla para salir...");
            Console.Read();

        }
        /// <summary>
        /// Los siguientes 4 métodos muestran aquellas opciones de gestión de las clases generadas.
        /// </summary>
        /// <param name="isEditable">El bool se genera en false cuando no hay objetos en la clase. Bloquea ciertas funciones del CRUD.</param>
        static void ShowMenuBooks(bool isEditable)
        {
            //Libros
            int input2 = -1;
            while (input2 != 0)
            {
                TableBooks();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("-----------Gestión de Libros------------");

                Console.WriteLine("Seleccione una opción para gestionar Libros:");
                Console.WriteLine("1. Crear Libro");
                Console.WriteLine("2. Actualizar Libros {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("3. Eliminar Libros {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("0. Atrás");

                bool isValid = false;

                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out input2);

                    if (!isValid)
                    {
                        Console.WriteLine("No es una línea válida");
                        continue;
                    }
                    if (input2 < 0 || input2 > 3)
                    {
                        Console.WriteLine("Seleccione un número válido.");
                        isValid = false;
                        continue;
                    }
                } while (!isValid);

                switch (input2)
                {
                    case 1:
                        {
                            var db = JsonHelper.ReadDB();

                            TableAuthors();
                            Console.WriteLine("Seleccione un Autor del Libro (ID):");

                            if (!int.TryParse(Console.ReadLine(), out int targetIdAuthor))
                            {
                                Console.WriteLine("El ID del autor debe ser un número.");
                                Console.ReadLine();
                                break;
                            }

                            // Verificar existencia del autor
                            var authorExists = authors.Any(a => a.IdAuthor == targetIdAuthor);

                            if (!authorExists)
                            {
                                Console.WriteLine("El autor seleccionado no existe.");
                                Console.ReadLine();
                                break;
                            }

                            Console.WriteLine("Introduzca un nombre al Libro");
                            string nameBook = Console.ReadLine();
                            Console.WriteLine("Introduzca un código al Libro");
                            string codeBook = Console.ReadLine();
                            Console.WriteLine("Coloque un género:");
                            string genre = Console.ReadLine();
                            Console.WriteLine("Ingresa el Año de lanzamiento:");
                            if (!int.TryParse(Console.ReadLine(), out int targetYear))
                            {
                                Console.WriteLine("Introduzca un año válido.");
                                Console.ReadLine();
                                break;
                            }
                            int currentYear = DateTime.Now.Year;
                            if (targetYear < 0 || targetYear > currentYear)
                            {
                                Console.WriteLine("Coloque un año adecuado.");
                                Console.ReadLine();
                                break;
                            }
                            Console.WriteLine("Ingresa el Stock:");
                            if (!int.TryParse(Console.ReadLine(), out int targetStock))
                            {
                                Console.WriteLine("Introduzca un número válido para el stock.");
                                Console.ReadLine();
                                break;
                            }

                            Book newBook = new Book
                            {
                                IdBook = JsonHelper.GetNextID(db.Books),
                                NameBook = nameBook,
                                CodeBook = codeBook,
                                IdAuthor = targetIdAuthor,
                                Genre = genre,
                                Year = targetYear,
                                TotalStockBook = targetStock
                            };

                            db.Books.Add(newBook);
                            JsonHelper.SaveDB(db);
                            Console.WriteLine("Creado Libro con el ID: {0}", newBook.IdBook);
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            if (!isEditable)
                            {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID en el que desees Actualizar el Libro");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var book = BookController.GetBookByID(id);
                            if (book == null)
                            {
                                Console.WriteLine("El libro no existe");
                                Console.ReadLine();
                                break;
                            }
                            Console.WriteLine("Nuevo Nombre:");
                            string nameBook = Console.ReadLine();
                            Console.WriteLine("Nuevo Código:");
                            string codeBook = Console.ReadLine();
                            Console.WriteLine("Nuevo Género:");
                            string genre = Console.ReadLine();
                            Console.WriteLine("Nuevo Año:");
                            if (!int.TryParse(Console.ReadLine(), out int targetYear))
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                Console.ReadLine();
                                break;
                            }
                            int currentYear = DateTime.Now.Year;
                            if (targetYear < 0 || targetYear > currentYear)
                            {
                                Console.WriteLine("Coloque un año adecuado.");
                                Console.ReadLine();
                                break;
                            }
                            if (!int.TryParse(Console.ReadLine(), out int targetStock))
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                Console.ReadLine();
                                break;
                            }
                            if (targetStock < LoanController.CountActiveLoans(id)) {
                                Console.WriteLine("Quedan préstamos pendientes.");
                                Console.ReadLine();
                                break;
                            }
                            int IdAuthor = book.IdAuthor;

                            bool updated = BookController.UpdateBook(id, codeBook, nameBook, IdAuthor, genre, targetYear, targetStock);
                            if (updated)
                            {
                                Console.WriteLine("Libro actualizado correctamente.");
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 3:
                        {
                            if (!isEditable)
                            {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID del Libro a eliminar.");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var book = BookController.GetBookByID(id);
                            if (book == null)
                            {
                                Console.WriteLine("El libro no existe");
                                Console.ReadLine();
                                break;
                            }
                            bool deleted = BookController.DeleteBook(id);
                            if (deleted)
                            {
                                Console.WriteLine("Libro eliminado correctamente.");
                                Console.ReadLine();
                            }

                            break;
                        }
                }
                //Sincronizar Cambios
                books = BookController.GetBooks();

                Console.Clear();
            }
        }
        static void ShowMenuAuthors(bool isEditable)
        {
            //Autores
            int input2 = -1;
            while (input2 != 0)
            {
                if (authors.Count > 0) {
                    Console.WriteLine("{0,-5} | {1,-15}", "ID", "Nombre");
                    Console.WriteLine(new string('-', 40));

                    foreach (var author in authors)
                    {
                        Console.WriteLine("{0,-5} | {1,-15}",
                        author.IdAuthor,
                        author.NameAuthor);
                    }
                }

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("-----------Gestión de Autores-----------");

                Console.WriteLine("Seleccione una opción para gestionar Autores:");
                Console.WriteLine("1. Crear Autor");
                Console.WriteLine("2. Actualizar Autores {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("3. Eliminar Autores {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("0. Atrás");

                bool isValid = false;

                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out input2);
                    if (!isValid)
                    {
                        Console.WriteLine("No es una línea válida");
                        continue;
                    }
                    if (input2 < 0 || input2 > 3)
                    {
                        Console.WriteLine("Seleccione un número válido.");
                        isValid = false;
                        continue;
                    }
                } while (!isValid);

                switch (input2)
                {
                    case 1:
                        {
                            var db = JsonHelper.ReadDB();

                            Console.WriteLine("Introduzca un nombre al Autor (Nombre Completo)");
                            string nameAuthor = Console.ReadLine();
                            Author newAuthor = new Author
                            {
                                IdAuthor = JsonHelper.GetNextID(db.Authors),
                                NameAuthor = nameAuthor
                            };

                            db.Authors.Add(newAuthor);
                            JsonHelper.SaveDB(db);
                            Console.WriteLine("Creado Autor con el ID: {0}", newAuthor.IdAuthor);
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            if (!isEditable) {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID en el que desees Actualizar el Autor");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var author = AuthorController.GetAuthorByID(id);
                            if (author == null)
                            {
                                Console.WriteLine("El autor no existe");
                                Console.ReadLine();
                                break;
                            }
                            Console.WriteLine("Nuevo Nombre:");
                            string nameAuthor = Console.ReadLine();

                            bool updated = AuthorController.UpdateAuthor(id, nameAuthor);
                            if (updated)
                            {
                                Console.WriteLine("Autor actualizado correctamente.");
                                Console.ReadLine();
                            }

                            break;
                        }
                    case 3:
                        {
                            if (!isEditable)
                            {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID del Autor a eliminar.");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var author = AuthorController.GetAuthorByID(id);
                            if (author == null)
                            {
                                Console.WriteLine("El autor no existe");
                                Console.ReadLine();
                                break;
                            }
                            bool deleted = AuthorController.DeleteAuthor(id);
                            if (deleted)
                            {
                                Console.WriteLine("Autor eliminado correctamente.");
                                Console.ReadLine();
                            }

                            break;
                        }
                }
                //Sincronizar cambios
                authors = AuthorController.GetAuthors();

                Console.Clear();
            }
        }
        static void ShowMenuUsers(bool isEditable)
        {
            //Usuarios
            int input2 = -1;
            while (input2 != 0)
            {
                TableUsers();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("-----------Gestión de Usuarios----------");

                Console.WriteLine("Seleccione una opción para gestionar Usuarios:");
                Console.WriteLine("1. Crear Usuario");
                Console.WriteLine("2. Actualizar Usuarios {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("3. Eliminar Usuarios {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("0. Atrás");

                bool isValid = false;

                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out input2);
                    if (!isValid)
                    {
                        Console.WriteLine("No es una línea válida");
                        continue;
                    }
                    if (input2 < 0 || input2 > 3)
                    {
                        Console.WriteLine("Seleccione un número válido.");
                        isValid = false;
                        continue;
                    }
                } while (!isValid);

                switch (input2)
                {
                    case 1:
                        {
                            var db = JsonHelper.ReadDB();

                            Console.WriteLine("Introduzca un nombre al Usuario");
                            string nameUser = Console.ReadLine();
                            Console.WriteLine("Introduzca un apellido al Usuario");
                            string lastNameUser = Console.ReadLine();
                            User newUser = new User
                            {
                                IdUser = JsonHelper.GetNextID(db.Users),
                                NameUser = nameUser,
                                LastNameUser = lastNameUser
                            };

                            db.Users.Add(newUser);
                            JsonHelper.SaveDB(db);
                            Console.WriteLine("Creado Usuario con el ID: {0}", newUser.IdUser);
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            if (!isEditable)
                            {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID en el que desees Actualizar el Usuario");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var user = UserController.GetUserByID(id);
                            if (user == null)
                            {
                                Console.WriteLine("El usuario no existe");
                                Console.ReadLine();
                                break;
                            }
                            Console.WriteLine("Nuevo Nombre:");
                            string nameUser = Console.ReadLine();
                            Console.WriteLine("Nuevo Apellido");
                            string lastNameUser = Console.ReadLine();

                            bool updated = UserController.UpdateUser(id, nameUser, lastNameUser);
                            if (updated)
                            {
                                Console.WriteLine("Usuario actualizado correctamente.");
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 3:
                        {
                            if (!isEditable)
                            {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID del Usuario a eliminar.");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var user = UserController.GetUserByID(id);
                            if (user == null)
                            {
                                Console.WriteLine("El usuario no existe");
                                Console.ReadLine();
                                break;
                            }
                            bool deleted = UserController.DeleteUser(id);
                            if (deleted)
                            {
                                Console.WriteLine("Usuario eliminado correctamente.");
                                Console.ReadLine();
                            }
                            break;
                        }
                }
                //Sincronizar cambios
                users = UserController.GetUsers();

                Console.Clear();
            }
        }
        static void ShowMenuLoans(bool isEditable)
        {
            //Préstamos
            int input2 = -1;
            while (input2 != 0)
            {
                if (loans.Count > 0)
                {
                    Console.WriteLine(
                        "{0,-5} | {1,-20} | {2,-20} | {3,-12} | {4,-12} | {5,-8}",
                        "ID",
                        "Usuario",
                        "Libro",
                        "Préstamo",
                        "Devolución",
                        "Estado"
                    );

                    Console.WriteLine(new string('-', 100));

                    foreach (var loan in loans)
                    {
                        //var author = authors.FirstOrDefault(a => a.IdAuthor == book.IdAuthor);
                        //string authorName = author != null ? author.NameAuthor : "Desconocido";
                        var user = users.FirstOrDefault(b => b.IdUser == loan.IdUser);
                        string nameUser = user != null ? user.NameUser : "Desconocido";
                        string lastNameUser = user != null ? user.LastNameUser : "Desconocido";
                        var book = books.FirstOrDefault(a => a.IdBook == loan.IdBook);
                        string nameBook = book != null ? book.NameBook : "Desconocido";

                        Console.WriteLine(
                            "{0,-5} | {1,-20} | {2,-20} | {3,-12} | {4,-12} | {5,-8}",
                            loan.IdLoan,
                            nameUser + " " + lastNameUser,
                            nameBook,
                            loan.DateLoan.ToString("dd/MM/yyyy"),
                            loan.ReturnLoan?.ToString("dd/MM/yyyy") ?? "—",
                            loan.IsActive ? "Activo" : "Cerrado"
                        );
                    }
                }
                else
                {
                    Console.WriteLine("No existen préstamos registrados.");
                }


                Console.WriteLine("----------------------------------------");
                Console.WriteLine("----------Gestión de Préstamos----------");

                Console.WriteLine("Seleccione una opción para gestionar Préstamos:");
                Console.WriteLine("1. Crear Préstamo");
                Console.WriteLine("2. Alternar Préstamo {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("3. Eliminar Préstamo {0}", (!isEditable) ? "(No Accesible)" : "");
                Console.WriteLine("0. Atrás");

                bool isValid = false;

                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out input2);
                    if (!isValid) {
                        Console.WriteLine("No es una línea válida");
                        continue;
                    }
                    if (input2 < 0 || input2 > 3)
                    {
                        Console.WriteLine("Seleccione un número válido.");
                        isValid = false;
                        continue;
                    }
                } while (!isValid);

                switch (input2)
                {
                    case 1:
                        {
                            var db = JsonHelper.ReadDB();
                            
                            //Ingresar ID del Libro
                            TableBooks();
                            Console.WriteLine("Seleccione un Libro (ID):");

                            if (!int.TryParse(Console.ReadLine(), out int targetIdBook))
                            {
                                Console.WriteLine("El ID del libro debe ser un número.");
                                Console.ReadLine();
                                break;
                            }

                            // Verificar existencia del libro
                            var bookExists = books.Any(a => a.IdBook == targetIdBook);

                            if (!bookExists)
                            {
                                Console.WriteLine("El libro seleccionado no existe.");
                                Console.ReadLine();
                                break;
                            }

                            //Verificamos si los préstamos están a tope
                            int countLoans = LoanController.CountActiveLoans(targetIdBook);
                            int stockBook = BookController.GetBookByID(targetIdBook).TotalStockBook;
                            if (stockBook <= countLoans ) {
                                Console.WriteLine("El libro seleccionado no tiene stock disponible para préstamo.");
                                Console.ReadLine();
                                break;
                            }

                            //Ingresar ID del Usuario
                            TableUsers();
                            Console.WriteLine("Seleccione un Usuario (ID):");

                            if (!int.TryParse(Console.ReadLine(), out int targetIdUser))
                            {
                                Console.WriteLine("El ID del usuario debe ser un número.");
                                Console.ReadLine();
                                break;
                            }

                            // Verificar existencia del Usuario
                            var userExists = users.Any(a => a.IdUser == targetIdUser);

                            if (!userExists)
                            {
                                Console.WriteLine("El usuario seleccionado no existe.");
                                Console.ReadLine();
                                break;
                            }

                            //Opcionalmente, puedes limitar una unidad por Usuario
                            if (LoanController.HasActiveLoanByUser(targetIdUser))
                            {
                                Console.WriteLine("El usuario ya tiene un préstamo vigente");
                                Console.ReadLine();
                                break;
                            }

                            Loan newLoan = new Loan
                            {
                                IdLoan = JsonHelper.GetNextID(db.Loans),
                                IdBook = targetIdBook,
                                IdUser = targetIdUser
                            };

                            db.Loans.Add(newLoan);
                            JsonHelper.SaveDB(db);
                            Console.WriteLine("Creado Préstamo con el ID: {0}", newLoan.IdLoan);
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            if (!isEditable)
                            {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID en el que desees Actualizar el Préstamo");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var loan = LoanController.GetLoanByID(id);
                            if (loan == null)
                            {
                                Console.WriteLine("El préstamo no existe");
                                Console.ReadLine();
                                break;
                            }

                            bool updated = LoanController.UpdateLoan(id);
                            if (updated)
                            {
                                Console.WriteLine("El préstamo ha cambiado de estado.");
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 3:
                        {
                            if (!isEditable)
                            {
                                Console.WriteLine("No hay entradas.");
                                break;
                            }
                            Console.WriteLine("Selecciona el ID del Préstamo a eliminar.");
                            bool isValid2 = int.TryParse(Console.ReadLine(), out int id);
                            if (!isValid2)
                            {
                                Console.WriteLine("No es un valor adecuado.");
                                break;
                            }
                            var loan = LoanController.GetLoanByID(id);
                            if (loan == null)
                            {
                                Console.WriteLine("El préstamo no existe");
                                Console.ReadLine();
                                break;
                            }
                            bool deleted = LoanController.DeleteLoan(id);
                            if (deleted)
                            {
                                Console.WriteLine("Préstamo eliminado correctamente.");
                                Console.ReadLine();
                            }
                            break;
                        }
                }
                //Sincronizar Cambios
                loans = LoanController.GetLoans();

                Console.Clear();
            }
        }
        /// <summary>
        /// El menú se muestra para dar opción al usuario qué es lo que quiere gestionar.
        /// </summary>
        /// <returns>El input que el usuario ha elegido para gestionar.</returns>
        static int ShowMenu() {
            Console.Clear();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("------Biblioteca Virtual de Libros------");

            Console.WriteLine("Seleccione una opción para gestionar Libros:");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Autores");
            Console.WriteLine("3. Usuarios");
            Console.WriteLine("4. Préstamos");
            Console.WriteLine("0. Salir");

            bool isValid = false;
            int input = -1;
            do {
                isValid = int.TryParse(Console.ReadLine(), out input);
                if (!isValid) {
                    Console.WriteLine("No es una línea válida");
                    continue;
                }
                if (input < 0 || input > 4) {
                    Console.WriteLine("Seleccione un número válido.");
                    isValid = false;
                    continue;
                }
            } while (!isValid);

            return input;
        }

        /// <summary>
        /// A partir de aquí, se generan tablas reutilizables, que en este caso son los Autores, Libros y Usuarios.
        /// </summary>
        static void TableAuthors() {
            if (authors.Count > 0)
            {
                Console.WriteLine("{0,-5} | {1,-15}", "ID", "Nombre");
                Console.WriteLine(new string('-', 40));

                foreach (var author in authors)
                {
                    Console.WriteLine("{0,-5} | {1,-15}",
                    author.IdAuthor,
                    author.NameAuthor);
                }
            }
        }

        static void TableBooks() {
            if (books.Count > 0)
            {
                // Cabecera de la tabla
                Console.WriteLine("{0,-5} | {1,-10} | {2,-25} | {3,-30} | {4,-20} | {5,-6} | {6,-5}",
                    "ID", "Código", "Nombre", "Autor", "Género", "Año", "Stock");

                Console.WriteLine(new string('-', 130));

                // Filas
                foreach (var book in books)
                {
                    var author = authors.FirstOrDefault(a => a.IdAuthor == book.IdAuthor);
                    string authorName = author != null ? author.NameAuthor : "Desconocido";

                    Console.WriteLine("{0,-5} | {1,-10} | {2,-25} | {3,-30} | {4,-20} | {5,-6} | {6,-5}",
                        book.IdBook,
                        book.CodeBook,
                        book.NameBook,
                        authorName,
                        book.Genre,
                        book.Year,
                        book.TotalStockBook
                    );
                }
            }
            else
            {
                Console.WriteLine("No se han encontrado libros.");
            }
        }
        static void TableUsers() {
            if (users.Count > 0)
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-15} | {3,-10}", "ID", "Nombre", "Apellido", "Puntaje");
                Console.WriteLine(new string('-', 55));

                foreach (var user in users)
                {
                    Console.WriteLine("{0,-5} | {1,-15} | {2,-15} | {3,-10}",
                    user.IdUser,
                    user.NameUser,
                    user.LastNameUser,
                    user.CreditScoreUser);
                }
            }
        }
    }
}
