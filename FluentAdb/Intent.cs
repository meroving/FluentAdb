using System.Collections.Generic;
using System.Linq;
using FluentAdb.Util;

namespace FluentAdb
{
    public class Intent
    {
        private string _action;
        private string _dataUri;
        private string _mimeType;
        private string _category;
        private string _component;
        private string _package;
        private List<string> _nullExtra;
        private Dictionary<string, string> _stringExtra;
        private Dictionary<string, bool> _boolExtra;
        private Dictionary<string, int> _integerExtra;
        private Dictionary<string, long> _longExtra;
        private Dictionary<string, float> _floatExtra;

        /// <summary>
        /// Create new Intent
        /// </summary>
        public static Intent New
        {
            get { return new Intent(); }
        }

        /// <summary>
        /// Specify the intent package
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public Intent With(string package)
        {
            _package = package;
            return this;
        }

        /// <summary>
        /// Specify the intent action, such as "android.intent.action.VIEW". You can declare this only once.
        /// </summary>
        /// <param name="action">Intent action</param>
        /// <returns></returns>
        public Intent Action(string action)
        {
            _action = action;
            return this;
        }

        /// <summary>
        /// Specify the intent data URI, such as "content://contacts/people/1". You can declare this only once.
        /// </summary>
        /// <param name="dataUri">Intent data Uri</param>
        /// <returns></returns>
        public Intent DataUri(string dataUri)
        {
            _dataUri = dataUri;
            return this;
        }

        /// <summary>
        /// Specify the intent MIME type, such as "image/png". You can declare this only once.
        /// </summary>
        /// <param name="mimeType">Intent MIME type</param>
        /// <returns></returns>
        public Intent MimeType(string mimeType)
        {
            _mimeType = mimeType;
            return this;
        }

        /// <summary>
        /// Specify an intent category, such as "android.intent.category.APP_CONTACTS".
        /// </summary>
        /// <param name="category">Intent category</param>
        /// <returns></returns>
        public Intent Category(string category)
        {
            _category = category;
            return this;
        }

        /// <summary>
        /// Specify the component name with package name prefix to create an explicit intent, such as "com.example.app/.ExampleActivity".
        /// </summary>
        /// <param name="component">Component name</param>
        /// <returns></returns>
        public Intent Component(string component)
        {
            _component = component;
            return this;
        }

        /// <summary>
        /// Add a null extra. This option is not supported for URI intents.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Intent Extra(string key)
        {
            if (_nullExtra == null)
                _nullExtra = new List<string>();

            _nullExtra.Add(key.ExceptionQuoteIfNeeded());
            return this;
        }

        /// <summary>
        /// Add string data as a key-value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Intent Extra(string key, string value)
        {
            if (_stringExtra == null)
                _stringExtra = new Dictionary<string, string>();

            _stringExtra.Add(key.ExceptionQuoteIfNeeded(), value.ExceptionQuoteIfNeeded());
            return this;
        }

        /// <summary>
        /// Add boolean data as a key-value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Intent Extra(string key, bool value)
        {
            if (_boolExtra == null)
                _boolExtra = new Dictionary<string, bool>();
            _boolExtra.Add(key.ExceptionQuoteIfNeeded(), value);
            return this;
        }

        /// <summary>
        /// Add integer data as a key-value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Intent Extra(string key, int value)
        {
            if (_integerExtra == null)
                _integerExtra = new Dictionary<string, int>();
            _integerExtra.Add(key.ExceptionQuoteIfNeeded(), value);
            return this;
        }

        /// <summary>
        /// Add long data as a key-value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Intent Extra(string key, long value)
        {
            if (_longExtra == null)
                _longExtra = new Dictionary<string, long>();
            _longExtra.Add(key.ExceptionQuoteIfNeeded(), value);
            return this;
        }

        /// <summary>
        /// Add float data as a key-value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Intent Extra(string key, float value)
        {
            if (_floatExtra == null)
                _floatExtra = new Dictionary<string, float>();
            _floatExtra.Add(key.ExceptionQuoteIfNeeded(), value);

            return this;
        }

        public override string ToString()
        {
            List<string> intent = new List<string>();
            if (_action != null)
                intent.Add(string.Format("-a {0}", _package != null ? _package + "." + _action : _action));

            if (_dataUri != null)
                intent.Add(string.Format("-d {0}", _dataUri));

            if (_mimeType != null)
                intent.Add(string.Format("-t {0}", _mimeType));

            if (_category != null)
                intent.Add(string.Format("-c {0}", _category));

            if (_component != null)
                intent.Add(string.Format("-n {0}", _package != null ? _package + "/." + _component : _component));

            if (_stringExtra != null)
                intent.AddRange(_stringExtra.Select(kv => string.Format("-e {0} {1}", kv.Key, kv.Value)));

            if (_boolExtra != null)
                intent.AddRange(_boolExtra.Select(kv => string.Format("--ez {0} {1}", kv.Key, kv.Value)));

            if (_integerExtra != null)
                intent.AddRange(_integerExtra.Select(kv => string.Format("--ei {0} {1}", kv.Key, kv.Value)));

            if (_longExtra != null)
                intent.AddRange(_longExtra.Select(kv => string.Format("--el {0} {1}", kv.Key, kv.Value)));

            if (_floatExtra != null)
                intent.AddRange(_floatExtra.Select(kv => string.Format("--ef {0} {1}", kv.Key, kv.Value)));

            return intent.Aggregate("", (acc, s) => acc + " " + s);
        }
    }
}
