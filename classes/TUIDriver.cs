using System.Text;

namespace Tui;

// TODO: define interface to allow for DI for alternative implementations not backed by Console
class TUIDriver
{
    // TODO: context-manager or function decorator
    public bool RequestString(out string value)
    {
        // Yes the try-finally block is an ugly hack. 
        // Learning the equivalent to a py style decorator or context manager would be the correct solution
        Console.Write("Please enter some text. N.B, press enter to submit\n");

        string? input = Console.ReadLine();
        if (input != null)
        {
            value = input;
            return true;
        }
        else
        {
            value = string.Empty;
            return false;
        }
    }

    // TODO: context-manager or function decorator
    public bool RequestNumber(out int value)
    {
        Console.Write("Please enter a number. Format example: 123\nN.B, press enter to submit\n");

        string? input = Console.ReadLine();
        if (input != null)
        {
            if (int.TryParse(input, out int result))
            {
                value = result;
                return true;
            }
            else
            {
                value = int.MinValue;
                return false;
            };
        }
        else
        {
            value = int.MinValue;
            return false;
        }
    }

    // TODO: context-manager or function decorator
    public bool RequestDateTime(out DateTime value)
    {
        Console.Write("Please enter a date. Format example: 2024-12-24 or 2024-12-24T23:59:30 or another valid ISO8601 value\nN.B, press enter to submit\n");

        string? input = Console.ReadLine();
        if (input != null)
        {
            try
            {
                value = DateTime.Parse(input);
                return true;
            }
            catch
            {
                value = DateTime.UnixEpoch;
                return false;
            }
        }
        else
        {
            value = DateTime.UnixEpoch;
            return false;
        }
    }

    // TODO: context-manager or function decorator
    public bool RequestBool(out bool value)
    {
        Console.WriteLine("Yes or No? (Also accepts: Y/N y/n true/false)");
        var input = Console.ReadLine();

        try
        {
            value = input switch
            // TODO: replace with flexible lists or dictionaries?
            {
                "Yes" or "Y" or "y" or "True" or "true" => true,
                "No" or "N" or "n" or "False" or "false" => false,
                _ => throw new Exception(""),
            };
        }
        catch
        {
            value = false;
            return false;
        }
        return true;
    }

    public void Present(string message)
    {
        Console.WriteLine(message);
    }

    public void Present<T>(IEnumerable<T> objs)
    {
        var str = new StringBuilder();
        foreach (var ele in objs)
        {
            str.Append($"{ele}, ");
        }
        str.Remove(str.Length - 2, 2);
        Console.WriteLine(str);
    }

    public void Blank()
    {
        Console.WriteLine();
    }

    public void Header(string str)
    {
        var width = Console.WindowWidth;


        for (int i = 0; i < width; i++)
        {
            if (i == 0)
            {
                Console.Write("\u23a1"); // ⎡
            }
            else if (i == width - 1)
            {
                Console.Write("\u23a4"); // ⎤
            }
            else
            {
                Console.Write("\u23ba"); // ⎺
            }
        }

        for (int i = 0; i < width; i++)
        {
            if (i == 0)
            {
                Console.Write("\u23a2"); // ⎢
            }
            else if (i == width - 1)
            {
                Console.Write("\u23a5"); // ⎥
            }
            else if (i == (width / 2) - str.Length)
            {
                Console.Write(str);
                i += str.Length - 1;
            }
            else
            {
                Console.Write(" ");
            }
        }

        for (int i = 0; i < width; i++)
        {
            if (i == 0)
            {
                Console.Write("\u23a3"); // ⎣ 
            }
            else if (i == width - 1)
            {
                Console.WriteLine("\u23a6"); // ⎦
            }
            else
            {
                Console.Write("\u23bd"); // ⎽
            }
        }
    }

    // TODO: fix unix and unix-like terminals not clearing
    public void Clear()
    {
        Console.Clear();
    }

    public TUIDriver()
    { }
}