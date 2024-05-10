
namespace Game2048;

public class ConsoleGame : Game
{
    private Menu menu;
    
    public ConsoleGame() : base(){
        menu = new();
    }

    protected override void StartGame(){
        // Overriding the inherited method.

        Console.Clear();

        GameBoard.Start();

        Console.ForegroundColor = ConsoleColor.Magenta; 
        Console.WriteLine($"Use the arrow keys to play\n");
        Console.ForegroundColor = ConsoleColor.Magenta; 

        ConsoleKeyInfo pressedKey;
        bool flag = true;
        bool restartGame = false;

        while (flag)
        {  
            if (Status == GameStatus.Idle || Status == GameStatus.Win){
                pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.UpArrow)
                    Move(Direction.Up);
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                    Move(Direction.Down);  
                else if (pressedKey.Key == ConsoleKey.LeftArrow)
                    Move(Direction.Left);  
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                    Move(Direction.Right);  

            }else if (Status == GameStatus.Lose){
                if (GameBoard.WonTheGame){
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine($"You Won!\n");

                    Console.ForegroundColor = ConsoleColor.DarkCyan; 
                    Console.WriteLine($"Score - {Points}");
                    Console.WriteLine($"Time: {GameBoard.Stopper}");
                    Console.ForegroundColor = ConsoleColor.Gray; 

                    Console.WriteLine();

                }else{
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine($"You Lost!\n");

                    Console.ForegroundColor = ConsoleColor.DarkCyan; 
                    Console.WriteLine($"Score: {Points}");
                    Console.WriteLine($"Time: {GameBoard.Stopper}");
                    Console.ForegroundColor = ConsoleColor.Gray; 

                    Console.WriteLine();

                }

                menu.EndMenu();

                pressedKey = Console.ReadKey(true);

                switch(pressedKey.Key){
                    case ConsoleKey.Enter:
                        // Start the game.

                        AddToLeaderBoard();
                        ResetGame();
                        flag = false;
                        restartGame = true;

                        break;

                    case ConsoleKey.S:
                        // Save and go back.

                        AddToLeaderBoard();
                        ResetGame();
                        Console.Clear();
                        menu.MainMenu();
                        flag = false;

                        break;
                }
            }
        }

        if (restartGame)
            StartGame();

    }

    public void Start(){
        // The method oversees the menu scene.

        Console.Clear();

        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;
        Console.Title = "Is it only 2048 . . ?";
        Console.TreatControlCAsInput = true;

        Console.ForegroundColor = ConsoleColor.Magenta; 
        menu.MainMenu();

        ConsoleKeyInfo pressedKey;

        bool runApp = true;

        while (runApp){
            if (!runApp) 
                return;

            pressedKey = Console.ReadKey(true);

            switch(pressedKey.Key){
                case ConsoleKey.Enter:
                    // Start the game.

                    StartGame();
                    
                    break;

                case ConsoleKey.Q:
                    // Close the game.

                    runApp = false;

                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Game Closed.");
                    Console.ForegroundColor = ConsoleColor.Gray; 

                    break;

                case ConsoleKey.L:
                    // Show leader board.
                   
                    Console.Clear();
                    PrintLeaderBoard();
                    menu.LeaderBoardMenu();

                    bool showLeaderBoard = true;
                    while (showLeaderBoard)
                    {
                        pressedKey = Console.ReadKey(true);

                        if(pressedKey.Key == ConsoleKey.Q){
                            // Open main menu.
                            Console.Clear();
                            menu.MainMenu();
                            showLeaderBoard = false;
                        }
                    }

                    break;

                case ConsoleKey.Escape:
                    // Show challenge.

                    System.Console.WriteLine("Challenge started.");
                    break;
            }
        }
    }
}