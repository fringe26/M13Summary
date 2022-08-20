using System.IO.MemoryMappedFiles;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


List<string> list = ReadFile();

Console.WriteLine("10 Most Common words : \n");
MostCommonWords(list).ToList().ForEach(Console.WriteLine);


static List<string> ReadFile()
{

    List<string> list = new List<string>();

    using (var mappedFile1 = MemoryMappedFile.CreateFromFile("Text1.txt"))
    {
        using (Stream mmStream = mappedFile1.CreateViewStream())
        {
            using (StreamReader sr = new StreamReader(mmStream, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    var noPunctuationText = new string(line.Where(c => !char.IsPunctuation(c)).ToArray());
                    var lineWords = noPunctuationText.Split(' ').ToList<string>();
                    lineWords.ForEach(list.Add);

                }
            }
        }
        //list.ForEach(Console.WriteLine);
        
    }
    return list;
}



static IEnumerable<string> MostCommonWords(List<string> list)
{
    Dictionary<string,int> keyValuePairs = new Dictionary<string,int>();

    foreach(string word in list)
    {
        if (!keyValuePairs.ContainsKey(word))
        {
            keyValuePairs[word] = 1;
        }
        else
        {
            keyValuePairs[word] = keyValuePairs[word] + 1;
        }
    }

    keyValuePairs = keyValuePairs.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);



    var words = keyValuePairs.Take(10).Select(x => x.Key);

    return words;
}


