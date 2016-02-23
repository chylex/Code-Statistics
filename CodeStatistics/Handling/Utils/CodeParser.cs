using System;

namespace CodeStatistics.Handling.Utils{
    public class CodeParser{
        protected readonly string code;
        protected readonly int length;
        protected int cursor;

        public Predicate<char> IsWhiteSpace = char.IsWhiteSpace;
        public Predicate<char> IsIdentifierStart = chr => char.IsLetterOrDigit(chr) || chr == '_';
        public Predicate<char> IsIdentifierPart = chr => char.IsLetterOrDigit(chr) || chr == '_';

        public Predicate<string> IsValidIdentifier = str => true;

        public string Contents { get { return code; } }
        public char Char { get { return cursor < length ? code[cursor] : '\0'; } }
        public bool IsEOF { get { return cursor >= length; } }

        public CodeParser(string code){
            this.code = code;
            this.length = code.Length;
        }

        protected string SubstrIndex(int startIndex, int endIndex){
            startIndex = Math.Min(length-1,startIndex);
            endIndex = Math.Min(length,endIndex);
            return code.Substring(startIndex,endIndex-startIndex);
        }

        /// <summary>
        /// Clones all of the parser settings, optionally with different code contents, and resets the cursor position to 0.
        /// </summary>
        public virtual CodeParser Clone(string newCode = null){
            return new CodeParser(newCode ?? code){
                IsWhiteSpace = this.IsWhiteSpace,
                IsIdentifierStart = this.IsIdentifierStart,
                IsIdentifierPart = this.IsIdentifierPart,
                IsValidIdentifier = this.IsValidIdentifier
            };
        }

        /// <summary>
        /// Skips to the next character and returns it.
        /// </summary>
        public char Next(){
            ++cursor;
            return Char;
        }

        /// <summary>
        /// Skips a single character and returns itself.
        /// </summary>
        public CodeParser Skip(){
            ++cursor;
            return this;
        }

        /// <summary>
        /// Skips all whitespace characters defined by <see cref="IsWhiteSpace"/> and places the cursor after the last found whitespace character.
        /// Returns itself.
        /// </summary>
        public CodeParser SkipSpaces(){
            while(cursor < length){
                if (IsWhiteSpace(Char))++cursor;
                else break;
            }
            
            return this;
        }

        /// <summary>
        /// Skips to the next matching character and returns itself. Caution: The skip may not succeed, so verify if the current character is valid if not certain.
        /// If the skip fails, the cursor will not move.
        /// </summary>
        public CodeParser SkipTo(char chr){
            int prevCursor = cursor;

            while(cursor < length){
                if (Char == chr)break;
                else ++cursor;
            }

            if (Char != chr)cursor = prevCursor;

            return this;
        }

        /// <summary>
        /// Skips a block enclosed by <paramref name="blockStart"/> and <paramref name="blockEnd"/>. If the current character does not match <paramref name="blockStart"/>,
        /// the parser will try skipping to the next <paramref name="blockStart"/> character first. If it fails, the cursor will not move, otherwise the cursor
        /// will be placed after the ending character of the block.
        /// </summary>
        public void SkipBlock(char blockStart, char blockEnd){
            SkipTo(blockStart);
            if (Char != blockStart)return;

            int blockDepth = 0;

            while(cursor < length){
                char next = Next();

                if (next == blockEnd){
                    if (--blockDepth < 0){
                        Next();
                        break;
                    }
                }
                else if (next == blockStart){
                    ++blockDepth;
                }
            }
        }

        /// <summary>
        /// Skips a text that matches a string with tokens. Returns true if the string matched code at current position and moves the cursor after the match,
        /// otherwise returns false and the cursor does not move. <para/>
        /// The <paramref name="matchStr"/> parameter may consist of the following tokens: <para/>
        /// ^s --- Whitespace Character <para/>
        /// ^. --- Any Character
        /// </summary>
        public bool SkipIfMatch(string matchStr){
            int index = 0, prevCursor = cursor;

            while(index < matchStr.Length){
                char matchChr = matchStr[index];
                bool hasMatched;

                if (matchChr == '^' && index < matchStr.Length-1){
                    switch(matchStr[++index]){
                        case 's':
                            hasMatched = IsWhiteSpace(Char);
                            break;

                        case '.':
                            hasMatched = true;
                            break;

                        default:
                            hasMatched = false;
                            break;
                    }
                }
                else{
                    hasMatched = Char == matchChr;
                }

                if (!hasMatched){
                    cursor = prevCursor;
                    return false;
                }
                else{
                    ++cursor;
                    ++index;
                }
            }

            return true;
        }

        /// <summary>
        /// Skips to the next matching character and returns a new instance of CodeParser with contents that were skipped (excluding the target character).
        /// If the skip does not succeed, an empty CodeParser will be returned and the cursor will not move.
        /// </summary>
        public CodeParser ReadTo(char chr){
            int indexStart = cursor;
            SkipTo(chr);
            return Clone(SubstrIndex(indexStart,cursor));
        }

        /// <summary>
        /// Skips to the next matching character and returns a new instance of CodeParser with contents that were skipped (excluding the target character).
        /// If the skip does not succeed, an empty CodeParser will be returned and the cursor will not move. If it succeeds, the parser will also skip the target character.
        /// </summary>
        public CodeParser ReadToSkip(char chr){
            CodeParser read = ReadTo(chr);
            if (Char == chr)Skip();
            return read;
        }

        /// <summary>
        /// Skips a block enclosed by <paramref name="blockStart"/> and <paramref name="blockEnd"/> and returns a new instance of CodeParser with the contents of that
        /// block, excluding the enclosing characters. If the current character does not match <paramref name="blockStart"/>, the parser will try skipping to the
        /// next <paramref name="blockStart"/>, first. If it fails, the returned contents will be empty and the cursor will not move, otherwise the cursor will be
        /// placed after the ending character of the block.
        /// </summary>
        public CodeParser ReadBlock(char blockStart, char blockEnd){
            SkipTo(blockStart);
            if (Char != blockStart)return Clone("");

            int indexStart = cursor;
            SkipBlock(blockStart,blockEnd);

            return Clone(SubstrIndex(indexStart+1,cursor-1));
        }

        /// <summary>
        /// Attempts to read an identifier from the current cursor position. If there is an identifier and it is valid, it is returned and the cursor moves after
        /// the identifier. If any step fails, the cursor does not move and <see cref="string.Empty"/> is returned.
        /// </summary>
        public string ReadIdentifier(){
            if (!IsIdentifierStart(Char))return string.Empty;
            
            int prevCursor = cursor;

            while(cursor < length){
                if (!IsIdentifierPart(Next()))break;
            }

            string identifier = SubstrIndex(prevCursor,cursor);

            if (!IsValidIdentifier(identifier)){
                cursor = prevCursor;
                return string.Empty;
            }

            return identifier;
        }
    }
}
