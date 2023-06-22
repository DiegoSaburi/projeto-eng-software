Dictionary<string, ICommand<LibraryRequest>> Commands = new ()
{
    { "emp", new BorrowCommand<LibraryRequest>() },
    { "user", new GetUserCommand<LibraryRequest>() }
};

while(true)
{
    BookReserve bookReserve = new (new Book(0, "Livro 1", new List<string> { "Autor 1", "Autor 2" }, "edição 1", "2017"), 
            new Teacher(0, "Professor 1"));
    System.Console.WriteLine("Digite um comando: ");
    string[] userInput = Console.ReadLine()?.Split(" ")!;
    var commandString = userInput[0];
    ICommand<LibraryRequest> command;
    Commands.TryGetValue(commandString, out command!);
    LibraryRequest request = new (int.Parse(userInput[1]), int.Parse(userInput[2]));
    command.Execute(request);
}


