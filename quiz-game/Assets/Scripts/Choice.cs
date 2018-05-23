using System;

namespace Assets.Scripts
{
    [Serializable]
    public class Choice
    {
        public string Statement;
        public bool   IsTrue;

        public Choice
        (
            string statement
        )
        {
            Statement = statement;
            IsTrue    = false;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Statement))
            {
                return false;
            }
            return true;
        }
    }
}
