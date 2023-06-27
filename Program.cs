Dictionary<string, ICommand<LibraryRequest>> Commands = new ()
{
    { "emp", new BorrowCommand() },
    { "user", new GetUserCommand() }
};

while(true)
{
    System.Console.WriteLine("Digite um comando: ");
    string[] userInput = Console.ReadLine()?.Split(" ")!;
   
    var commandString = userInput[0];
    if(string.Equals(commandString, "sair"))
        break;

    ICommand<LibraryRequest> command;
    Commands.TryGetValue(commandString, out command!);
    LibraryRequest request = new (int.Parse(userInput[1]), int.Parse(userInput[2]));
    command.Execute(request);
}


