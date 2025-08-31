namespace RxDemo
{
  public class Helpers
  {
    // Helpers
    public static void Title(string text)
    {
      Console.WriteLine();
      Console.WriteLine(new string('=', text.Length));
      Console.WriteLine(text);
      Console.WriteLine(new string('=', text.Length));
    }

    public static void Pause(string message)
    {
      Console.WriteLine(message);
      Console.ReadKey();
    }

  }
}
