using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseAlpaca1
{
    public class TreeNode
    {
        private static Dictionary<string, Variable> variables = new();
        private static Dictionary<string, Function> functions = new();

        private readonly TreeNode[] branches;
        private readonly string character;
        private readonly string nodeType;

        public TreeNode(string character, string type, params TreeNode[] branches)
        {
            this.character = character;
            this.branches = branches;
            nodeType = type;
        }

        public string NodeType { get => nodeType; }
        public string Character { get => character; }
        public bool HasChildren { get => Branches.Length > 0; }
        public TreeNode[] Branches { get => branches; }
 
        public static Dictionary<string, Variable> Variables { get => variables; }
        public static Dictionary<string, Function> Functions { get => functions; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append(Character);
            if (Branches.Length > 0)
            {
                sb.Append('(').Append(Branches[0].ToString());
                for (int i = 1; i < Branches.Length; i++)
                {
                    sb.Append(", ").Append(Branches[i].ToString());
                }
                sb.Append(')');
            } else if (NodeType.Equals("function"))
            {
                sb.Append('(').Append(')');
            }
            return sb.ToString();
        }

        public static string CheckTypes(TreeNode node)
        {
            switch (node.NodeType)
            {
                case "assign":
                    if (node.Branches[0].NodeType.Equals("var"))
                    {
                        Variable @var = new(
                                    node.Branches[0].Character,
                                    CheckTypes
                                    (node.Branches[1])
                                    );
                        if (!Variables.ContainsKey(node.Branches[0].Character))
                        {
                            Variables.Add(node.Branches[0].Character, @var);
                        }
                        else
                        {
                            Variables[node.Branches[0].Character] = @var;
                        }
                        return Variables[node.Branches[0].Character].Type;
                    }
                    else
                    {
                        Function func = new(
                                node.Branches[0].Character,
                                node.Branches[1],
                                node.Branches[0].Branches
                                    .Select(branch => new Variable(branch.Character, branch.NodeType))
                                    .ToArray()
                                );
                        if (!Functions.ContainsKey(node.Branches[0].Character))
                        {
                            Functions.Add(node.Branches[0].Character, func);
                        }
                        else
                        {
                            Functions[node.Branches[0].Character] = func;
                        }
                        return Functions[node.Branches[0].Character].Type;
                    }
                case "operation":
                    if (CheckTypes(node.Branches[0]).Equals("int") 
                        && CheckTypes(node.Branches[1]).Equals("int"))
                    {
                        return "int";
                    }
                    else
                    {
                        return "double";
                    }
                case "function":
                    if (Functions[node.Character].CheckArgs(node.Branches.Select(branch => CheckTypes(branch)).ToArray()))
                        return Functions[node.Character].Type;
                    else
                        throw new Exception($"invalid args {node}");
                case "int":
                    return node.NodeType;
                case "double":
                    return node.NodeType;
                case "unknown":
                    if (Variables.ContainsKey(node.Character))
                        return Variables[node.Character].Type;
                    else
                        throw new Exception($"invalid expresion {node}");
                case "var":
                case "def":
                default:
                    throw new Exception($"invalid expresion {node}");
            }
        }

        public static TreeNode[] MakeNodeArray(params TreeNode[] nodeArray)
        {
            return nodeArray;
        }
        public static TreeNode[] MakeNodeArray(TreeNode node, params TreeNode[] nodeArray)
        {
            TreeNode[] newNodeArray = new TreeNode[nodeArray.Length + 1];
            newNodeArray[0] = node;
            for (int i = 0; i < nodeArray.Length; i++)
            {
                newNodeArray[i + 1] = nodeArray[i];
            }
            return newNodeArray;
        }
    }
}
