using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ParseAlpaca1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader reader = new("input.txt", Encoding.UTF8);
                List<string> input = new(reader.ReadToEnd().Trim().Split('\n'));
                reader.Close();

                int spase = 0;
                input = input.Select(str =>
                {
                    str = str.Trim();
                    if (spase < str.Length)
                    {
                        spase = str.Length;
                    }
                    return str;
                }).ToList();

                parser.Parser parser = new();
                
                foreach (string str in input)
                {
                    TreeNode root = (TreeNode)parser.parse(lexer.Lexer.lex(str));
                    TreeNode.CheckTypes(root);
                    string output = str + new string(' ', spase - str.Length);
                    Console.WriteLine($"{output} | {root}");
                }
                Console.WriteLine();
                TreeNode.Functions.Keys.ToList().ForEach(key => Console.WriteLine(TreeNode.Functions[key].ToString()));
                TreeNode.Variables.Keys.ToList().ForEach(key => Console.WriteLine(TreeNode.Variables[key].ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
