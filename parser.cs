using lexer;
using System;
using System.Collections.Generic;

using System.Linq;

using ParseAlpaca1;

namespace parser {
public class Parser {
  private readonly bool debug;
  private Stack<(uint state, dynamic value)> stack = new Stack<(uint state, dynamic value)>();
  private static uint[,] Action = new uint[,] {
    {44,4,44,44,44,44,27,44,44,44,44,34,38,32,44,39},
    {45,44,44,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {1,44,44,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {46,44,44,17,25,44,26,28,44,44,31,44,44,44,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {47,44,47,47,47,47,47,47,44,44,47,44,44,44,44,44},
    {44,44,5,17,25,44,26,28,44,44,31,44,44,44,44,44},
    {44,44,48,17,25,8,26,28,44,44,31,44,44,44,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {44,44,49,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {44,44,50,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {44,44,51,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {44,44,44,44,44,44,44,44,44,44,44,44,44,44,14,44},
    {44,44,44,44,44,44,44,44,12,44,44,44,44,44,44,44},
    {44,44,52,44,44,15,44,44,44,44,44,44,44,44,44,44},
    {44,44,44,44,44,44,44,44,44,44,44,13,44,44,44,44},
    {44,44,53,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {54,44,54,54,54,54,54,54,44,44,31,44,44,44,44,44},
    {55,44,55,17,55,55,55,28,44,44,31,44,44,44,44,44},
    {56,44,56,17,56,56,56,28,44,44,31,44,44,44,44,44},
    {57,44,57,57,57,57,57,57,44,44,57,44,44,44,44,44},
    {58,44,58,58,58,58,58,58,44,44,31,44,44,44,44,44},
    {59,44,44,17,25,44,26,28,44,44,31,44,44,44,44,44},
    {60,44,60,60,60,60,60,60,44,44,31,44,44,44,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {44,44,44,44,44,44,44,44,44,29,44,44,44,44,44,44},
    {44,4,44,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {61,44,61,61,61,61,61,61,44,44,61,44,44,44,44,44},
    {62,35,62,62,62,62,62,62,44,44,62,44,44,44,44,44},
    {62,35,62,62,62,62,62,62,44,63,62,44,44,44,44,44},
    {44,4,64,44,44,44,27,44,44,44,44,33,38,32,44,44},
    {65,44,65,65,65,65,65,65,44,44,65,44,44,44,44,44},
    {44,44,36,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {66,44,66,66,66,66,66,66,44,44,66,44,44,44,44,44},
    {44,44,44,44,44,44,44,44,44,44,44,40,44,44,44,44},
    {44,41,44,44,44,44,44,44,44,44,44,44,44,44,44,44},
    {44,44,67,44,44,44,44,44,44,44,44,13,44,44,44,44},
    {44,44,44,44,44,44,44,44,44,68,44,44,44,44,44,44},
    {44,44,42,44,44,44,44,44,44,44,44,44,44,44,44,44}
  };
  private static uint[,] GOTO = new uint[,] {
    {3,30,2,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {6,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {7,0,0,9,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,16,0,0},
    {0,0,0,0,0,0,0},
    {18,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {19,0,0,0,0,0,0},
    {20,0,0,0,0,0,0},
    {21,0,0,0,0,0,0},
    {22,0,0,0,0,0,0},
    {23,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {24,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {7,0,0,10,0,37,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,11,0,43},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0}
  };
  private uint top() {
    return stack.Count == 0 ? 0 : stack.Peek().state;
  }
  static string[] stateNames = new string[] {".","%eof","S","E","lparen","rparen","E","E","comma","Args1","Args1","DefArgs1","colon","id","type","comma","DefArgs1","mul","E","E","E","E","E","E","E","plus","minus","minus","del","assign","F","pow","double","id","id","lparen","rparen","Args","int","def","id","lparen","rparen","DefArgs"};
  static string[] expectedSyms = new string[] {"S","%eof","%eof","%eof/mul/plus/minus/del/pow","E","%eof/comma/del/minus/mul/plus/pow/rparen","rparen/mul/plus/minus/del/pow","rparen/comma/mul/plus/minus/del/pow","Args1","rparen","rparen","rparen","type/type","colon/colon","rparen/comma","DefArgs1","rparen","E","mul/%eof/comma/del/minus/mul/plus/pow/rparen/plus/minus/del/pow","mul/plus/%eof/comma/del/minus/mul/plus/pow/rparen/minus/del/pow","mul/plus/minus/%eof/comma/del/minus/mul/plus/pow/rparen/del/pow","mul/plus/minus/%eof/comma/del/minus/mul/plus/pow/rparen/del/pow","mul/plus/minus/del/%eof/comma/del/minus/mul/plus/pow/rparen/pow","mul/plus/minus/del/%eof/pow","mul/plus/minus/del/pow/%eof/comma/del/minus/mul/plus/pow/rparen","E","E","E","E","E","assign","E","%eof/comma/del/minus/mul/plus/pow/rparen","lparen/%eof/comma/del/minus/mul/plus/pow/rparen","lparen/%eof/comma/del/minus/mul/plus/pow/rparen/assign","Args","%eof/comma/del/minus/mul/plus/pow/rparen","rparen","%eof/comma/del/minus/mul/plus/pow/rparen","id","lparen","DefArgs","assign","rparen"};

  public Parser(bool debug = false) {
    this.debug = debug;
  }
  public dynamic parse(IEnumerable<(TokenType type, dynamic attr)> tokens) {
    stack.Clear();
    var iter = tokens.GetEnumerator();
    iter.MoveNext();
    var a = iter.Current;
    while (true) {
      var action = Action[top(), (int)a.type];
      switch (action) {
      case 45: {
          stack.Pop();
          return stack.Pop().value;
        }
      case 64: {
          if(debug) Console.Error.WriteLine("Reduce using Args -> ");
          
          var gt = GOTO[top(), 5 /*Args*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray())));
          break;
        }
      case 50: {
          if(debug) Console.Error.WriteLine("Reduce using Args -> Args1");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 5 /*Args*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray((TreeNode[]) _1))));
          break;
        }
      case 48: {
          if(debug) Console.Error.WriteLine("Reduce using Args1 -> E");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 3 /*Args1*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray((TreeNode) _1))));
          break;
        }
      case 49: {
          if(debug) Console.Error.WriteLine("Reduce using Args1 -> E comma Args1");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 3 /*Args1*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray((TreeNode) _1, (TreeNode[]) _3))));
          break;
        }
      case 67: {
          if(debug) Console.Error.WriteLine("Reduce using DefArgs -> ");
          
          var gt = GOTO[top(), 6 /*DefArgs*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray())));
          break;
        }
      case 51: {
          if(debug) Console.Error.WriteLine("Reduce using DefArgs -> DefArgs1");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 6 /*DefArgs*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray((TreeNode[]) _1))));
          break;
        }
      case 52: {
          if(debug) Console.Error.WriteLine("Reduce using DefArgs1 -> id colon type");
          var _3=stack.Pop().value.Item2;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*DefArgs1*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray(new TreeNode((string) _1, (string) _3)))));
          break;
        }
      case 53: {
          if(debug) Console.Error.WriteLine("Reduce using DefArgs1 -> id colon type comma DefArgs1");
          dynamic _5=stack.Pop().value;
          var _4=stack.Pop().value.Item2;
          var _3=stack.Pop().value.Item2;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*DefArgs1*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(TreeNode.MakeNodeArray(new TreeNode((string) _1, (string) _3), (TreeNode[]) _5))));
          break;
        }
      case 61: {
          if(debug) Console.Error.WriteLine("Reduce using E -> double");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode((string) _1, "double"))));
          break;
        }
      case 62: {
          if(debug) Console.Error.WriteLine("Reduce using E -> id");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode((string) _1, "unknown"))));
          break;
        }
      case 65: {
          if(debug) Console.Error.WriteLine("Reduce using E -> id lparen Args rparen");
          var _4=stack.Pop().value.Item2;
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode((string) _1, "function", (TreeNode[]) _3))));
          break;
        }
      case 66: {
          if(debug) Console.Error.WriteLine("Reduce using E -> int");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode((string) _1, "int"))));
          break;
        }
      case 47: {
          if(debug) Console.Error.WriteLine("Reduce using E -> lparen E rparen");
          var _3=stack.Pop().value.Item2;
          dynamic _2=stack.Pop().value;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,((TreeNode) _2)));
          break;
        }
      case 57: {
          if(debug) Console.Error.WriteLine("Reduce using E -> minus E");
          dynamic _2=stack.Pop().value;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode("-", "operation", (TreeNode) _2))));
          break;
        }
      case 58: {
          if(debug) Console.Error.WriteLine("Reduce using E -> E del E");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode("/", "operation", (TreeNode) _1, (TreeNode) _3))));
          break;
        }
      case 56: {
          if(debug) Console.Error.WriteLine("Reduce using E -> E minus E");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode("-", "operation", (TreeNode) _1, (TreeNode) _3))));
          break;
        }
      case 54: {
          if(debug) Console.Error.WriteLine("Reduce using E -> E mul E");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode("*", "operation", (TreeNode) _1, (TreeNode) _3))));
          break;
        }
      case 55: {
          if(debug) Console.Error.WriteLine("Reduce using E -> E plus E");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode("+", "operation", (TreeNode) _1, (TreeNode) _3))));
          break;
        }
      case 60: {
          if(debug) Console.Error.WriteLine("Reduce using E -> E pow E");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 0 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode("^", "operation", (TreeNode) _1, (TreeNode) _3))));
          break;
        }
      case 68: {
          if(debug) Console.Error.WriteLine("Reduce using F -> def id lparen DefArgs rparen");
          var _5=stack.Pop().value.Item2;
          dynamic _4=stack.Pop().value;
          var _3=stack.Pop().value.Item2;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 1 /*F*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode((string) _2, "def", (TreeNode[]) _4))));
          break;
        }
      case 63: {
          if(debug) Console.Error.WriteLine("Reduce using F -> id");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 1 /*F*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode((string) _1, "var"))));
          break;
        }
      case 46: {
          if(debug) Console.Error.WriteLine("Reduce using S -> E");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 2 /*S*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,((TreeNode) _1)));
          break;
        }
      case 59: {
          if(debug) Console.Error.WriteLine("Reduce using S -> F assign E");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 2 /*S*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new TreeNode("=", "assign", (TreeNode) _1, (TreeNode) _3))));
          break;
        }
      case 44: {
          string parsed=stateNames[top()];
          var lastSt = top();
          while(stack.Count > 0) { stack.Pop(); parsed = stateNames[top()] + " " + parsed; }
          throw new ApplicationException(
            $"Rejection state reached after parsing \"{parsed}\", when encoutered symbol \""
            + $"\"{a.type}\" in state {lastSt}. Expected \"{expectedSyms[lastSt]}\"");
        }
      default:
        if(debug) Console.Error.WriteLine($"Shift to {action}");
        stack.Push((action, a));
        iter.MoveNext();
        a=iter.Current;
        break;
      }
    }
  }
}
}