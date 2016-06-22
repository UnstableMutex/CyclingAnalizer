using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight;


namespace CyclingAnalizer
{
    public interface IContent
    {
        object Content { get; set; }
    }
    public abstract class VMB : ViewModelBase
    {
        protected void AssignCommands<T>()
        {
            Type t = GetType();
            IEnumerable<PropertyInfo> commands =
                t.GetProperties().Where(p => p.PropertyType == typeof(ICommand) && p.Name.EndsWith("Command"));
            foreach (PropertyInfo propertyInfo in commands)
            {
                TryToAssign<T>(t, propertyInfo);
            }
        }

        private void TryToAssign<T>(Type type, PropertyInfo propertyInfo)
        {
            string methodname = propertyInfo.Name.RemoveEnd("Command");
            string canmethodname = "Can" + methodname;
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo m = type.GetMethod(methodname, flags, null, Type.EmptyTypes, null);
            MethodInfo canm = type.GetMethod(canmethodname, flags);
            Type cmdtype = typeof(T);
            if (m != null)
            {
                if (canm == null)
                {
                    ConstructorInfo ctor = cmdtype.GetConstructor(new[] { typeof(Action) });
                    var cmd = (ICommand)ctor.Invoke(new object[] { (Action)(() => m.Invoke(this, null)) });
                    propertyInfo.SetValue(this, cmd);
                }
                else
                {
                    ConstructorInfo ctor = cmdtype.GetConstructor(new[] { typeof(Action), typeof(Func<bool>) });
                    var cmd =
                        (ICommand)
                            ctor.Invoke(new object[]
                            {(Action) (() => m.Invoke(this, null)), (Func<bool>) (() => (bool) canm.Invoke(this, null))});
                    propertyInfo.SetValue(this, cmd);
                }
            }
        }

        protected virtual void RealSave()
        {
        }

        protected virtual void SaveQuery(string propertyName)
        {
            RealSave();
          
        }

        protected void RaisePropertyChangedNoSave([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
        }

        public override sealed void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            SaveQuery(propertyName);
        }


      

     
    }
    public static class ext
    {
        public static string RemoveEnd(this string s, string end,
            StringComparison c = StringComparison.CurrentCultureIgnoreCase)
        {
            int len = end.Length;
            if (s.EndsWith(end, c))
            {
                return s.Substring(0, s.Length - len);
            }
            throw new WrongStringEndException();
        }

        public static string RemoveEndIfExists(this string s, string end,
            StringComparison c = StringComparison.CurrentCultureIgnoreCase)
        {
            int len = end.Length;
            if (s.EndsWith(end, c))
            {
                return s.Substring(0, s.Length - len);
            }
            return s;
        }
    }
    [Serializable]
    public class WrongStringEndException : Exception
    {
        public WrongStringEndException() { }
        public WrongStringEndException(string message) : base(message) { }
        public WrongStringEndException(string message, Exception inner) : base(message, inner) { }
        protected WrongStringEndException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}