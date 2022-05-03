using System;
using System.Collections;
using System.Collections.Generic;

namespace lexer {
public enum TokenType : uint {
  eof, Tok_lparen,Tok_rparen,Tok_mul,Tok_plus,Tok_comma,Tok_minus,Tok_del,Tok_colon,Tok_assign,Tok_pow,Tok_id,Tok_int,Tok_double,Tok_type,Tok_def
}

class Buf<T> : IEnumerator<T> {
  IEnumerator<T> current;
  Stack<IEnumerator<T>> stack;

  Object IEnumerator.Current => Current;
  public T Current => Empty ? default(T) : current.Current;
  public bool Empty => current is null;

  public Buf(IEnumerable<T> it) {
    current = it.GetEnumerator();
    stack = new Stack<IEnumerator<T>>();
  }

  public bool MoveNext() {
    if (Empty) return false;
    var res = current.MoveNext();
    if (!res) {
      if (stack.Count > 0) {
        current = stack.Pop();
        return MoveNext();
      } else {
        current = null;
      }
    }
    return res;
  }

  public void Unshift(IEnumerable<T> it) {
    stack.Push(current);
    current = it.GetEnumerator();
  }

  public void Reset() {
    throw new NotSupportedException();
  }

  public void Dispose() {
    /* no-op */
  }
}

public class Lexer {
  public static IEnumerable<(TokenType type, dynamic attr)> lex(IEnumerable<char> input, bool debug = false) {
    var inputBuf = new Buf<char>(input);
    start:
    char curCh;
    int accSt = -1;
    string buf = "";
    string tmp = "";
    state_0:
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == ' ') goto state_1; else if(curCh == '(') goto state_2; else if(curCh == ')') goto state_3; else if(curCh == '*') goto state_4; else if(curCh == '+') goto state_5; else if(curCh == ',') goto state_6; else if(curCh == '-') goto state_7; else if(curCh == '/') goto state_8; else if(curCh == ':') goto state_9; else if(curCh == '=') goto state_10; else if(curCh == '^') goto state_11; else if(curCh == 'd') goto state_13; else if(curCh == 'i') goto state_14; else if(curCh == '_'||(curCh >= 'a' && curCh <= 'z')) goto state_12; else if((curCh >= '0' && curCh <= '9')) goto state_15;
      goto end;
    state_1:
      buf += tmp;
      tmp = "";
      accSt = 1;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == ' ') goto state_1;
      goto end;
    state_2:
      buf += tmp;
      tmp = "";
      accSt = 2;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_3:
      buf += tmp;
      tmp = "";
      accSt = 3;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_4:
      buf += tmp;
      tmp = "";
      accSt = 4;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_5:
      buf += tmp;
      tmp = "";
      accSt = 5;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_6:
      buf += tmp;
      tmp = "";
      accSt = 6;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_7:
      buf += tmp;
      tmp = "";
      accSt = 7;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_8:
      buf += tmp;
      tmp = "";
      accSt = 8;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_9:
      buf += tmp;
      tmp = "";
      accSt = 9;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_10:
      buf += tmp;
      tmp = "";
      accSt = 10;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_11:
      buf += tmp;
      tmp = "";
      accSt = 11;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      
      goto end;
    state_12:
      buf += tmp;
      tmp = "";
      accSt = 12;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_13:
      buf += tmp;
      tmp = "";
      accSt = 13;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'e') goto state_22; else if(curCh == 'o') goto state_23; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_14:
      buf += tmp;
      tmp = "";
      accSt = 14;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'n') goto state_20; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_15:
      buf += tmp;
      tmp = "";
      accSt = 15;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == '.') goto state_16; else if(curCh == 'E'||curCh == 'e') goto state_17; else if((curCh >= '0' && curCh <= '9')) goto state_15;
      goto end;
    state_16:
      buf += tmp;
      tmp = "";
      accSt = 16;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'E'||curCh == 'e') goto state_17; else if((curCh >= '0' && curCh <= '9')) goto state_16;
      goto end;
    state_17:
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == '+'||curCh == '-') goto state_18; else if((curCh >= '0' && curCh <= '9')) goto state_19;
      goto end;
    state_18:
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if((curCh >= '0' && curCh <= '9')) goto state_19;
      goto end;
    state_19:
      buf += tmp;
      tmp = "";
      accSt = 19;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if((curCh >= '0' && curCh <= '9')) goto state_19;
      goto end;
    state_20:
      buf += tmp;
      tmp = "";
      accSt = 20;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 't') goto state_21; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_21:
      buf += tmp;
      tmp = "";
      accSt = 21;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_22:
      buf += tmp;
      tmp = "";
      accSt = 22;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'f') goto state_28; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_23:
      buf += tmp;
      tmp = "";
      accSt = 23;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'u') goto state_24; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_24:
      buf += tmp;
      tmp = "";
      accSt = 24;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'b') goto state_25; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_25:
      buf += tmp;
      tmp = "";
      accSt = 25;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'l') goto state_26; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_26:
      buf += tmp;
      tmp = "";
      accSt = 26;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == 'e') goto state_21; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_28:
      buf += tmp;
      tmp = "";
      accSt = 28;
      
      if(!inputBuf.MoveNext()) goto end;
      curCh = inputBuf.Current;
      tmp += curCh;
      if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    end:
    if (tmp.Length > 0) {
      inputBuf.Unshift(tmp);
    }
    var text = buf;
    switch(accSt){
      case 1:
        if (debug) Console.Error.WriteLine($"Skipping state 1: \"{text}\"");
        goto start;
      case 2:
        if (debug) Console.Error.WriteLine($"Lexed token lparen: \"{text}\"");
        yield return (TokenType.Tok_lparen,  '(' );
        goto start;
      case 3:
        if (debug) Console.Error.WriteLine($"Lexed token rparen: \"{text}\"");
        yield return (TokenType.Tok_rparen,  ')' );
        goto start;
      case 4:
        if (debug) Console.Error.WriteLine($"Lexed token mul: \"{text}\"");
        yield return (TokenType.Tok_mul,  '*' );
        goto start;
      case 5:
        if (debug) Console.Error.WriteLine($"Lexed token plus: \"{text}\"");
        yield return (TokenType.Tok_plus,  '+' );
        goto start;
      case 6:
        if (debug) Console.Error.WriteLine($"Lexed token comma: \"{text}\"");
        yield return (TokenType.Tok_comma,  ',' );
        goto start;
      case 7:
        if (debug) Console.Error.WriteLine($"Lexed token minus: \"{text}\"");
        yield return (TokenType.Tok_minus,  '-' );
        goto start;
      case 8:
        if (debug) Console.Error.WriteLine($"Lexed token del: \"{text}\"");
        yield return (TokenType.Tok_del,  '/' );
        goto start;
      case 9:
        if (debug) Console.Error.WriteLine($"Lexed token colon: \"{text}\"");
        yield return (TokenType.Tok_colon,  ':' );
        goto start;
      case 10:
        if (debug) Console.Error.WriteLine($"Lexed token assign: \"{text}\"");
        yield return (TokenType.Tok_assign,  '=' );
        goto start;
      case 11:
        if (debug) Console.Error.WriteLine($"Lexed token pow: \"{text}\"");
        yield return (TokenType.Tok_pow,  '^' );
        goto start;
      case 12:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 13:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 14:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 15:
        if (debug) Console.Error.WriteLine($"Lexed token int: \"{text}\"");
        yield return (TokenType.Tok_int,  text );
        goto start;
      case 16:
        if (debug) Console.Error.WriteLine($"Lexed token double: \"{text}\"");
        yield return (TokenType.Tok_double,  text );
        goto start;
      case 19:
        if (debug) Console.Error.WriteLine($"Lexed token double: \"{text}\"");
        yield return (TokenType.Tok_double,  text );
        goto start;
      case 20:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 21:
        if (debug) Console.Error.WriteLine($"Lexed token type: \"{text}\"");
        yield return (TokenType.Tok_type,  text );
        goto start;
      case 22:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 23:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 24:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 25:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 26:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        yield return (TokenType.Tok_id,  text );
        goto start;
      case 28:
        if (debug) Console.Error.WriteLine($"Lexed token def: \"{text}\"");
        yield return (TokenType.Tok_def,  text );
        goto start;
    }
    if (inputBuf.Empty) {
      if (debug) Console.Error.WriteLine($"Got EOF while lexing \"{text}\"");
      yield return (TokenType.eof, null);
      goto start;
    }
    throw new ApplicationException("Unexpected input: " + buf + tmp);
  }
}
}