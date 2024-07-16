using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using wordFinder.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace wordFinder
{
    public class WordFinder : IWordFinder
    {
        private const int matrixMaxSize = 64;

        private IEnumerable<string> _matrix;
        private IEnumerable<string> _matrixV;

        private int size = 0;


        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
            Check();

            List<string> vList = new List<string>(); ;

            //temporal to convert string to array
            List<string[]> tmp = [];
            foreach (var row in _matrix)
            {
                tmp.Add(row.Split(' '));
            }

            // create new matriz in Vertical
            for (int i = 0; i < tmp.Count(); i++)
            {
                List<string> column = new List<string>();
                foreach (var row in tmp)
                {
                    column.Add(row[i]);
                }

                vList.Add(string.Join(" ", column.ToArray()));
            }

            _matrixV = vList;
        }


        private void Check()
        {

            if (_matrix.Count() > matrixMaxSize) throw new Exception($"Matrix [row] exceed max size permited {matrixMaxSize}");

            foreach (var row in _matrix)
            {
                int _current = row.Trim().Split(' ').Count();

                if (_current == 0) throw new Exception($"Matrix [col] without character");

                if (_current > matrixMaxSize)
                {
                    throw new Exception($"Matrix [col] exceed max size permited {matrixMaxSize}");
                }

                if (size == 0) { size = _current; continue; }

                if (size != _current) { throw new Exception($"Matrix [col] not have same number of characters"); }
            }

        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            List<Word> words = [];

            //foreach array Horizontal
            FindWords(_matrix, wordstream, ref words);

            //foreach array vertical
            FindWords(_matrixV, wordstream, ref words);


            //order by [Count] descending  and return maximun 10 words 
            return words.OrderByDescending(x => x.Count).Select(x => x.Name).Take(10).ToList();
        }


        private void FindWords(IEnumerable<string> matrix, IEnumerable<string> wordstream, ref List<Word> words)
        {
            foreach (var row in matrix)
            {
                string currentRow = Regex.Replace(row, @"\s+", "");

                foreach (var word in wordstream)
                {
                    //Check word was write normal or invert
                    if (currentRow.Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                        currentRow.Contains(word.StringInvert(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        /*Check if the word was previously found. 
                         *in case is true increment the count otherwise add to list.
                         */
                        int index = words.FindIndex(x => x.Name.Equals(word));
                        if (index >= 0)
                        {
                            words[index].Count++;
                            continue;
                        }

                        words.Add(new Word(word));
                    }
                }



            }
        }


        public record Word(string Name)
        {
            public int Count { get; set; } = 1;
        };
    }
}

