using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Catechize.Templating.Web
{
    public class WebTemplateEngine : TemplateEngine, ITemplateEngine
    {
        // Properties
        //

        public override int EngineVersion { get { return 1; } }
        public string TemplateRegex { get; set; }

        private StringBuilder Template { get; set; }
        private string OriginalTemplate { get; set; }
        private object DataSource { get; set; }
        private MatchCollection Matches { get; set; }
        

        // Constructors
        // 

        public WebTemplateEngine() {
            TemplateRegex = @"(#|!#)\{[^:]+:[^:]+\}";
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
            Initialize( templateContents, dataSource );
            return ProcessTemplate_Internal();
        }


        // Private Methods
        //

        private void Initialize( string template, object data ) {
            Template = new StringBuilder( template );
            OriginalTemplate = template;
            DataSource = data;
            Matches = null;
        }

        private string ProcessTemplate_Internal() {
            Regex regex = new Regex( TemplateRegex, RegexOptions.Compiled & RegexOptions.IgnoreCase );

            this.Matches = regex.Matches( OriginalTemplate );

            for ( int i = Matches.Count - 1; i > -1; i-- ) {
                ProcessTemplateItem( Matches[i] );
            }
            
            return Template.ToString();
        }

        private void ProcessTemplateItem( Match match ) {
            var value = match.Value;

            if ( value.Length >= 2 && value[1].Equals( '!' ) ) {
                var newValue = @"#{" + value.Substring( 3 );

                ReplaceMatch( match, newValue );
            } else {
                KeyValuePair<string,string> item = GetKeyValue( match );

                // Find the processor key
                ITemplateValueProcessor selectedProcessor 
                    = GetValueProcessor( item.Key );

                if ( selectedProcessor == null )
                    ReplaceMatch( match, "<b style='color: red'>TEMPLATE ERROR: Key invalid</b>" );
                else {
                    ReplaceMatch( match, selectedProcessor.ProcessValue( item.Value, DataSource ) );
                }
            }
        }

        private void ReplaceMatch( Match match, string newValue ) {
            Template.Remove( match.Index, match.Length );
            Template.Insert( match.Index, newValue );
        }

        private KeyValuePair<string, string> GetKeyValue( Match match ) {
            string [] temp = match.Value.Trim( '{', '#', '}' ).Split( ':' );

            return new KeyValuePair<string, string>(
                 temp[0], temp[1] );
        }

    }

}
