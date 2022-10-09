public class CaesarEnDecoder
{
    // Создаем глобальную перменную, где будем хранить алфавит
    const string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    // Создаем приватную функцию для шифрования, которая будет доступна только функциям внутри класса
    private string Encode(string text, int k)
    {
        // Создаем в алфавит в нижнем регистре
        var lowerAlphabet = alphabet.ToLower();
        // Получаем длину алфавита
        var letterQty = alphabet.Length;
        // Создаем пустую переменную, куда будем добавлять зашифрованные символы
        var retVal = "";
        for (int i = 0; i < text.Length; i++)
        {
            // Получаем в переменную символ и помещаем в регистр
            var c = text[i];
            // Получаем индекс символа в алфавите большого регистра
            var index = alphabet.IndexOf(c);

            // Если символ найден, шифруем его
            if (index > 0)
            {
                var codeIndex = (letterQty + index + k) % letterQty;
                retVal += alphabet[codeIndex];
                // Если найден, идем к следующему символу, перейдя к след. итерации цикла
                continue;
            }
            // Если не найден, пытаемся найти в алфавите с нижним регистром
            else
            {
                var lowerIndex = lowerAlphabet.IndexOf(c);
                if (lowerIndex > 0)
                {
                    var codeIndex = (letterQty + lowerIndex + k) % letterQty;
                    retVal += lowerAlphabet[codeIndex];
                    // Если найден, идем к следующему символу, перейдя к след. итерации цикла
                    continue;
                }
            }

            // Если не найден ни в одном из алфавитов, то добавляем его в неизменном виде
            retVal += c.ToString();
        }

        return retVal;
    }

    
    // Взлом шифра (перебор всех вариантов)
    public string[] Hack(string encryptedMessage)
    {
        // Переменная с количеством вариантов (длина алфавита)
        int variantsQty = alphabet.Length;

        // Создаем переменную, в которую будем добавлять варианты расшифрованных сообщений
        string[] retValues = new string[variantsQty];

        // Перебираем всевозможные варианты
        for (int i = 0; i < variantsQty; i++)
        {
            retValues[i] = this.Decrypt(encryptedMessage, i + 1);
        }

        return retValues;
    }

    // Шифрование текста
    public string Encrypt(string plainMessage, int key)
        => Encode(plainMessage, key);

    // Дешифрование текста, просто взяв отрицательное число от ключа
    public string Decrypt(string encryptedMessage, int key)
        => Encode(encryptedMessage, -key);
}

// Входная точка программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем экземпляр класса шифровальщика
        var encoder = new CaesarEnDecoder();
        Console.Write("Введите текст: ");
        // Получаем данные с клавиатуры
        var message = Console.ReadLine();
        Console.Write("Введите ключ: ");
        // Получаем данные с клавиатуры
        var secretKey = Convert.ToInt32(Console.ReadLine());
        // Создаем переменную и присваиваем ей зашифрованное сообщение
        var encryptedText = encoder.Encrypt(message, secretKey);

        // Выводим результаты
        Console.WriteLine("Зашифрованное сообщение: {0}", encryptedText);
        Console.WriteLine();
        Console.WriteLine("Расшифрованное сообщение: {0}", encoder.Decrypt(encryptedText, secretKey));
        Console.WriteLine();
        Console.WriteLine("Взлом ({0}):", encryptedText);

        // Создаем переменную, в которую будут присвоены всевозможные варианты шифрования
        var hacked = encoder.Hack(encryptedText);
        // Выводим их
        for (int i = 0; i < hacked.Length; i++)
        {
            Console.WriteLine("Ключ: {0}; Сообщение: {1}", i + 1, hacked[i]);
        }

        // Ожидаем от пользователя ввода любого символа, чтобы консоль сама не закрылась
        Console.ReadLine();
    }
}
