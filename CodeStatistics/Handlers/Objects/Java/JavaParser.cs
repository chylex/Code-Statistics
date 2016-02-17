using CodeStatistics.Handlers.Objects.Java.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeStatistics.Handling;
using CodeStatistics.Handling.Parsing;

namespace CodeStatistics.Handlers.Objects.Java{
    static class JavaParser{
        public static void Parse(string fileContents, JavaStatistics stats){
            string fileParsed = JavaParseUtils.Strings.Replace(JavaParseUtils.CommentMultiLine.Replace(JavaParseUtils.CommentOneLine.Replace(fileContents,""),""),"");
            
            string[] linesPlain = fileContents.Split('\n').Select(line => line.TrimEnd()).Where(line => line.Length > 0).ToArray(); // lines are always \n
            string[] linesParsed = fileParsed.Split('\n').Select(line => line.TrimEnd()).Where(line => line.Length > 0).ToArray(); //  ^

            // Final Variables
            JavaStatistics.JavaFileInfo _info;
            string _package = null;
            JavaType _currentType;
            string _simpleType = null;
            string _fullType;

            // Package
            if (linesParsed.FirstOrDefault(line => line.ExtractBoth("package ",";",out _package)) == null)return; // should not happen, but what if...

            stats.Packages.Add(_package);

            // Current Type
            string typeLine = linesParsed.FirstOrDefault(line => JavaParseUtils.TypeIdentifiersSpace.Any(identifier => line.Contains(identifier) && JavaParseUtils.GetType(line).Key != JavaType.Invalid));
            if (typeLine == null)return; // should not happen either, but we know the drill...

            KeyValuePair<JavaType,string> typeData = JavaParseUtils.GetType(typeLine);
            _currentType = typeData.Key;
            _simpleType = typeData.Value;
            _fullType = _package+"."+_simpleType;

            ++stats.TypeFileCounts[_currentType];
            ++stats.TypeCounts[_currentType];
            stats.SimpleTypes.Add(_simpleType);
            stats.FullTypes.Add(_fullType);

            _info = stats.CreateFileInfo(_fullType);

            // Lines
            _info.Lines = linesPlain.Count(line => !line.TrimStart().Equals("{"));
            stats.LinesTotal += _info.Lines;

            // Characters
            int spaces = 0;

            _info.Characters = linesPlain.Select(line => {
                string noSpaces = line.TrimStart(' ');

                int diff = line.Length-noSpaces.Length;
                if (diff > 0 && spaces == 0)spaces = diff;

                return diff > 0 ? noSpaces.PadLeft(noSpaces.Length+diff/spaces,'\t').Length : line.Length; // replaces spaces with tabs if there are any
            }).Sum();

            stats.CharactersTotal += _info.Characters;

            // Imports
            foreach(string line in linesParsed){
                string trimmed = line.TrimStart();

                if (trimmed.StartsWith("import "))++_info.Imports;
                else if (trimmed.Length != 0 && !trimmed.StartsWith("package "))break;
            }

            stats.ImportsTotal += _info.Imports;

            // Nested Types
            foreach(KeyValuePair<JavaType,string> kvp in linesParsed.Where(line => JavaParseUtils.TypeIdentifiersSpace.Any(identifier => line.Contains(identifier)) && !line.Equals(typeLine)).Select(line => JavaParseUtils.GetType(line)).Where(kvp => kvp.Key != JavaType.Invalid)){
                ++stats.TypeCounts[kvp.Key];
                // TODO go through nested types and add them to type list
            }

            // Primitives
            foreach(IEnumerable<JavaPrimitives> types in linesParsed.Select(line => JavaParseUtils.CountPrimitives(line.TrimStart()))){
                foreach(JavaPrimitives type in types)++stats.PrimitiveCounts[type];
            }

            // Cycles, Switches & Try Blocks
            int foundDo = 0, foundWhile = 0;

            foreach(string line in linesParsed){
                foreach(Match match in JavaParseUtils.Syntax.Matches(line)){
                    string element = match.Groups[1].Value;

                    switch(element){
                        case "switch": ++stats.SyntaxSwitches; break;
                        case "try": ++stats.SyntaxTry; break;
                        case "while": ++foundWhile; break;
                        case "do": ++foundDo; break;
                        case "for":
                            if (line.IndexOf(':',match.Groups[1].Index+4) != -1)++stats.SyntaxForEach;
                            else ++stats.SyntaxFor;

                            break;

                        default: throw new System.NotSupportedException("Invalid match group in Java syntax element detection: "+element);
                    }
                }
            }

            stats.SyntaxDoWhile += foundDo;
            stats.SyntaxWhile += foundWhile-foundDo;

            // Fields & Methods
            int tabs = -1;

            foreach(string line in linesParsed){
                bool isType = JavaParseUtils.TypeIdentifiersSpace.Any(identifier => line.Contains(identifier)) && JavaParseUtils.GetType(line).Key != JavaType.Invalid;

                if (isType)tabs = 1+line.Length-line.TrimStart().Length;
                else if (line.Length-line.TrimStart().Length == tabs){ // assume fields and methods will have the same indentation
                    string[] modifiers;
                    string stripped = JavaParseUtils.StripModifiers(line.TrimStart(),out modifiers);

                    if (JavaParseUtils.MethodLine.IsMatch(stripped)){
                        JavaModifiers.Info mf = new JavaModifiers.Info(modifiers);
                        ++stats.MethodsTotal;
                        ++stats.MethodVisibility[mf.Visibility];
                        ++stats.MethodScope[mf.Scope];
                        ++stats.MethodFinality[mf.Finality];
                    }
                    else if (JavaParseUtils.FieldLine.IsMatch(stripped)){
                        JavaModifiers.Info mf = new JavaModifiers.Info(modifiers);
                        ++stats.FieldsTotal;
                        ++stats.FieldVisibility[mf.Visibility];
                        ++stats.FieldScope[mf.Scope];
                        ++stats.FieldFinality[mf.Finality];
                    }
                }
            }
        }
    }
}
