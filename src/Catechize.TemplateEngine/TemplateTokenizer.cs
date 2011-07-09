using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Templating
{
    public class TemplateToken
    {
        public string TokenString { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int StartIndexInTemplate { get; set; }

        public int Length {
            get {
                return TokenString.Length;
            }
        }

        public TemplateToken( string tokenString, string key, string value, int startIndexInTemplate ) {
            this.TokenString = tokenString;
            this.Key = key;
            this.Value = value;
            this.StartIndexInTemplate = startIndexInTemplate;
        }
    }


    public interface ITemplateTokenizer
    {
        IList<TemplateToken> Parse( string templateContents );
    }


    public class TemplateTokenizer : ITemplateTokenizer
    {
        public string TemplateContent { get; set; }

        private int CurrentIndex { get; set; }

        private char CurrentChar {
            get {
                return TemplateContent[CurrentIndex];
            }
        }

        private int PeekChar {
            get {
                var canPeek = ( CurrentIndex + 1 ) <= ( TemplateContent.Length - 1 );

                if ( canPeek )
                    return (int)TemplateContent[CurrentIndex + 1];

                return -1;
            }
        }

        private bool MoveNext() {
            CurrentIndex += 1;
            return !EOF;
        }

        private bool EOF {
            get {
                return !(CurrentIndex <= ( TemplateContent.Length - 1 ));
            }
        }
                

        /// <summary>
        /// Parses the template for token placeholds.
        /// </summary>
        /// <returns>A <see cref="IList"/> of <see cref="Tuple"/> objects
        /// 1. string containing the template placehold,
        /// 2. int containing the start index of the placehold
        /// 3. int containing the length of the placehold</returns>
        public IList<TemplateToken> Parse(string template) {
            this.TemplateContent = template;
            this.CurrentIndex = -1;

            IList<TemplateToken> result = new List<TemplateToken>();

            while ( MoveNext() ) {
                switch ( CurrentChar ) {
                    case '#':
                        if ( PeekChar != -1 && (char)PeekChar == '{' ) {
                            var item = GetToken();

                            if ( item != null )
                                result.Add( item );

                            CurrentIndex -= 1; // Move back 1 character ready for the next MoveNext() as GetToken() moves to 2nd character after.
                        }
                        break;
                }
            }

            return result;
        }

        private TemplateToken GetToken() {
            /* Tokens have these rules
             * Must have a closing } before end of line (only one line)
             * Must have characters before the first : which must be 
             */
            StringBuilder currentSection = new StringBuilder();
            string key = string.Empty;
            string value = string.Empty;
            int startIndex = CurrentIndex;
            bool foundColon = false;
            bool foundEndBrace = false;
            bool isValidToken = true;

            // Move to the { so next MoveNext will be to content
            MoveNext();

            while ( foundEndBrace == false && MoveNext() && isValidToken ) {
                switch ( CurrentChar ) {
                    case ':':
                        if ( foundColon == false ) {
                            foundColon = true;
                            key = currentSection.ToString();
                            currentSection.Clear();
                            continue;
                        }
                        break;
                    case '}':
                        foundEndBrace = true;
                        if ( foundColon ) {
                            value = currentSection.ToString();
                            currentSection.Clear();
                        } else {
                            key = currentSection.ToString();
                            currentSection.Clear();
                        }
                        break;
                    case '\n':
                        isValidToken = false;
                        break;
                }

                currentSection.Append( CurrentChar );
            }

            if ( isValidToken ) {
                var tokenLength = ( CurrentIndex - startIndex ) + 1;

                return new TemplateToken(
                    TemplateContent.Substring( startIndex, tokenLength ),
                    key, value, startIndex );
            }

            return null;
        }

    }
}
