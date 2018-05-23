using System;

namespace Assets.Scripts
{
    public interface IMaybe<T>
    {
        bool HasValue { get; }
        TU   Case<TU> (Func<T, TU> some, Func<TU> none);
        void Case     (Action<T> some, Action none);
    }

    public static class Some<T>
    {
        public static IMaybe<T> Exists(T value)
        {
            return new Maybe<T>(value);
        }
    }

    public static class None<T>
    {
        public static IMaybe<T> Exists()
        {
            return new Maybe<T>();
        }
    }

    public class Maybe<T> : IMaybe<T>
    {
        public  bool HasValue { get; private set; }
        private readonly T _value;

        public Maybe(T value)
        {
            _value = value;
            HasValue = true;
        }
        
        public Maybe()
        {
            HasValue = false;
        }

        public TU Case<TU>(Func<T, TU> some, Func<TU> none)
        {
            return HasValue ? some(_value) : none();
        }

        public void Case(Action<T> some, Action none)
        {
            if (HasValue) some(_value);
            else none();
        }
    }
}
