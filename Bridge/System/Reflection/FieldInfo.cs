using Bridge;
using System.ComponentModel;

namespace System.Reflection
{
    [External]
    [Unbox(true)]
    public class FieldInfo : MemberInfo
    {
        [Name("rt")]
        public extern Type FieldType
        {
            get;
            private set;
        }

        [Name(true)] //[Field]
        public extern bool IsInitOnly
        {
            [Template("({this}.ro || false)")]
            get;
        }

        [Template("Bridge.Reflection.fieldAccess({this}, {obj})")]
        public extern object GetValue(object obj);

        [Template("Bridge.Reflection.fieldAccess({this}, {obj}, {value})")]
        public extern void SetValue(object obj, object value);

        /// <summary>
        /// Script name of the field
        /// </summary>
        [Name("sn")]
        public extern string ScriptName
        {
            get;
            private set;
        }

        [NonScriptable, EditorBrowsable(EditorBrowsableState.Never)]
        public static extern FieldInfo GetFieldFromHandle(RuntimeFieldHandle h);

        [NonScriptable, EditorBrowsable(EditorBrowsableState.Never)]
        public static extern FieldInfo GetFieldFromHandle(RuntimeFieldHandle h, RuntimeTypeHandle x);

        internal extern FieldInfo();
    }
}