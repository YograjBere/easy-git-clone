namespace console_git_clone_clipboard;

public class UserInput
{
   public static void DisplayMenu(IEnumerable<string> content) 
    {
        foreach (var item in content)
        {
            PrintLine(item);
        }
    }

   public static string Receive() 
    {
        return Console.ReadLine();
    }

    public static void PrintLine(string text) 
    {
        System.Console.WriteLine(text);
    }
}
