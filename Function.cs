using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseAlpaca1
{
    public class Function
    {
        private readonly string name;
        private readonly string type;
        private readonly TreeNode expr;
        private readonly Dictionary<string, Variable> args;

        public Function(string name, TreeNode expr, params Variable[] args)
        {
            this.name = name;
            this.expr = expr;
            this.args = new();
            args.ToList().ForEach(arg => this.args.Add(arg.Name, arg));
            this.type = FunctionType(expr);
        }

        private string FunctionType(TreeNode node)
        {
            switch (node.NodeType)
            {
                case "int":
                    return "int";
                case "double":
                    return "double";
                case "operation":
                    if (node.Branches.Length == 1)
                    {
                        return FunctionType(node.Branches[0]);
                    }
                    else
                    {
                        string firstType = FunctionType(node.Branches[0]);
                        string secondType = FunctionType(node.Branches[1]);
                        if (firstType.Equals("int")
                            && secondType.Equals("int"))
                            return "int";
                        else
                            return "double";
                    }
                case "function":
                    if (TreeNode.Functions.ContainsKey(node.Character))
                        return TreeNode.Functions[node.Character].Type;
                    else
                        throw new Exception($"invalid expresion {node}");
                case "unknown":
                    if (Args.ContainsKey(node.Character))
                        return Args[node.Character].Type;
                    else if (TreeNode.Variables.ContainsKey(node.Character))
                        return TreeNode.Variables[node.Character].Type;
                    else
                        throw new Exception($"invalid expresion {node}");
                default:
                    throw new Exception($"invalid expresion {node}");
            }
        }

        public string Name { get => name; }
        public string Type { get => type; }
        public TreeNode Expr { get => expr; }
        public Dictionary<string, Variable> Args { get => args; }

        public bool CheckArgs(params string[] argsTypes)
        {
            if (argsTypes.Length == Args.Count)
            {
                var keys = Args.Keys.ToArray();
                for (int i = 0; i < argsTypes.Length; i++)
                {
                    if (Args[keys[i]].Type.Equals("int") && !argsTypes[i].Equals("int"))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            if (Args.Count > 0)
            {
                var keys = Args.Keys.ToArray();
                sb.Append(Args[keys[0]].Type);
                for (int i = 1; i < keys.Length; i++)
                {
                    sb.Append($", {Args[keys[i]].Type}");
                }
            }
            return $"{Name}({sb}):{Type}";
        }
    }
}
