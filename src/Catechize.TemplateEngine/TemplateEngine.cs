using System.IO;
using System.Collections.Generic;
using System;

namespace Catechize.Templating
{
    public interface ITemplateEngine
    {
        int EngineVersion { get; }
        // TODO: add property that sets error message template string and type (Options: exception, error message, empty string)

        string ProcessTemplate( string templateContents );
        string ProcessTemplate( TextReader reader );
        string ProcessTemplate( string templateContents, object dataSource );
        string ProcessTemplate( TextReader reader, object dataSource );

        void RegisterValueProcessor( ITemplateValueProcessor processor );
        void RemoveValueProcessor( ITemplateValueProcessor processor );
        ITemplateValueProcessor GetValueProcessor( string key );
        void ClearValueProcessors();
    }


    [Serializable]
    public abstract class TemplateEngine : ITemplateEngine
    {
        private IList<ITemplateValueProcessor> _valueProcessors  = new List<ITemplateValueProcessor>();

        protected IList<ITemplateValueProcessor> ValueProcessors {
            get {
                return _valueProcessors;
            }
        }

        public abstract int EngineVersion { get; }

        public abstract string ProcessTemplate( string templateContents );
        public abstract string ProcessTemplate( TextReader reader );
        public abstract string ProcessTemplate( TextReader reader, object dataSource );
        public abstract string ProcessTemplate( string templateContents, object dataSource );

        public static string LoadFileText( string filePath ) {
            if ( File.Exists( filePath ) )
                return File.ReadAllText( filePath );

            throw new FileNotFoundException( "TemplateEngine.LoadFileText() could not load the given file", filePath );
        }

        public void RegisterValueProcessor( ITemplateValueProcessor processor ) {
            lock ( this ) {
                foreach ( ITemplateValueProcessor item in ValueProcessors ) {
                    if ( item.GetType().FullName.Equals( processor.GetType().FullName, System.StringComparison.OrdinalIgnoreCase ) ) 
                        return;

                    if ( item.Key.Equals( processor.Key, System.StringComparison.OrdinalIgnoreCase ) )
                        throw new ArgumentException( "You cannot register two ITemplateValueProcessor's with the same key value.", "processor" );
                }

                // Register this engine with the value processor and register
                // as a value processor for this engine.
                processor.TemplateEngine = this;

                ValueProcessors.Add( processor );
            }
        }

        public void RemoveValueProcessor( ITemplateValueProcessor processor ) {
            lock ( this ) {
                foreach ( ITemplateValueProcessor item in ValueProcessors ) {
                    if ( item.GetType().FullName.Equals( processor.GetType().FullName, System.StringComparison.OrdinalIgnoreCase ) ) {
                        ValueProcessors.Remove( item );
                        return;
                    }
                }
            }
        }

        public void ClearValueProcessors() {
            lock ( this ) {
                ValueProcessors.Clear();
            }
        }

        public ITemplateValueProcessor GetValueProcessor( string key ) {
            foreach ( ITemplateValueProcessor processor in ValueProcessors ) {
                if ( processor.Key.Equals( key, System.StringComparison.OrdinalIgnoreCase ) )
                    return processor;
            }

            return null;
        }
    }

}
