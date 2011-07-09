using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace Catechize.Templating.Web
{
    public class WebTemplateEngine : TemplateEngine, ITemplateEngine
    {
        // Properties
        //

        public override int EngineVersion { get { return 1; } }
        public ITemplateTokenizer Tokenizer {get; set;}


        // Constructors
        //

        public WebTemplateEngine( ITemplateTokenizer tokenizer ) {
            Tokenizer = tokenizer;    
        }


        // Public Methods
        //

        public override string ProcessTemplate( TextReader reader) {
            return ProcessTemplate( reader.ReadToEnd(), null );
        }

        public override string ProcessTemplate( string templateContents ) {
            return ProcessTemplate( templateContents, null );
        }

        public override string ProcessTemplate( TextReader reader, object dataSource ) {
            return ProcessTemplate( reader.ReadToEnd(), dataSource );
        }

        public override string ProcessTemplate( string templateContents, object dataSource ) {
            return ProcessTemplate_Internal( templateContents, dataSource );
        }


        // Private Methods
        //

        private string ProcessTemplate_Internal(string template, object model) {
            // Parse the document
            StringBuilder newContent = new StringBuilder( template );

            var tokens = Tokenizer.Parse( template );

            for ( int i = tokens.Count - 1; i > -1; i-- ) {
                ProcessTemplateItem( newContent, tokens[i], model );
            }

            return newContent.ToString();
        }

        private void ProcessTemplateItem(StringBuilder newContent, TemplateToken token, object model ) {
            var value = token.Value;

            if ( value.Length >= 2 && value[1].Equals( '!' ) ) {
                var newValue = @"#{" + value.Substring( 3 );

                ReplaceMatch(newContent, token, newValue );
            } else {
                // Find the processor key
                ITemplateValueProcessor selectedProcessor 
                    = GetValueProcessor( token.Key );

                if ( selectedProcessor == null )
                    ReplaceMatch(newContent, token, "<b style='color: red'>TEMPLATE ERROR: Key invalid</b>" );
                else {
                    ReplaceMatch( newContent, token, selectedProcessor.ProcessValue( token.Value, model ) );
                }
            }
        }

        private void ReplaceMatch( StringBuilder newContent, TemplateToken token, string newValue ) {
            newContent.Remove( token.StartIndexInTemplate, token.Length );
            newContent.Insert( token.StartIndexInTemplate, newValue );
        }

        private KeyValuePair<string, string> GetKeyValue( Match match ) {
            string [] temp = match.Value.Trim( '{', '#', '}' ).Split( ':' );

            return new KeyValuePair<string, string>(
                 temp[0], temp[1] );
        }

    }
}
