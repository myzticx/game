using System;
using System.Threading;

class Program
{
    static int playerX = 0; // Player's X coordinate
    static int playerY = 0; // Player's Y coordinate

    static bool isJumping = false; // Flag to track if the player is jumping
    static int jumpHeight = 5; // Height of the jump
    static int jumpCount = 0; // Counter to keep track of the jump progress

    static Random random = new Random();

    static void Main()
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(40, 20); // Set console window size

        DrawGame(); // Initial game draw !!

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                // Movement controls
                if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                {
                    MovePlayer(0, -1);
                }
                else if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                {
                    MovePlayer(0, 1);
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                {
                    MovePlayer(-1, 0);
                }
                else if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                {
                    MovePlayer(1, 0);
                }

                // Jump control
                if (key == ConsoleKey.Spacebar && !isJumping)
                {
                    isJumping = true;
                }
            }

            // Jumping logic
            if (isJumping)
            {
                JumpPlayer();
            }

            // Check for collision
            if (CheckCollision())
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X"); // Display collision indicator
                Console.ResetColor();
                break; // End the game
            }

            // Generate obstacles
            if (random.Next(0, 10) < 2) // 20% chance of obstacle generation
            {
                GenerateObstacle();
            }

            Thread.Sleep(50); // Delay to control game speed
        }
    }

    static void DrawGame()
    {
        Console.Clear();

        // Draw player
        Console.SetCursorPosition(playerX, playerY);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("P");
        Console.ResetColor();

        // Draw ground
        Console.SetCursorPosition(0, 19);
        Console.Write(new string('_', 40));

        Console.SetCursorPosition(0, 0); // Reset cursor position
    }

    static void MovePlayer(int offsetX, int offsetY)
    {
        Console.SetCursorPosition(playerX, playerY);
        Console.Write(" "); // Erase previous player position

        playerX += offsetX;
        playerY += offsetY;

        // Keep the player within the screen bounds
        playerX = Math.Max(0, Math.Min(playerX, 39));
        playerY = Math.Max(0, Math.Min(playerY, 18));

        Console.SetCursorPosition(playerX, playerY);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("P"); // Draw player at new position
        Console.ResetColor();

        Console.SetCursorPosition(0, 0); // Reset cursor position
    }

    static void JumpPlayer()
    {
        Console.SetCursorPosition(playerX, playerY);
        Console.Write(" "); // Erase previous player position

