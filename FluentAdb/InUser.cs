namespace FluentAdb
{
    public struct InUser
    {
        private string m_userString;
        private InUser(string userString)
        {
            m_userString = string.Format("--user {0} ", userString);
        }

        public static InUser All
        {
            get
            {
                return new InUser("all");
            }
        }

        public static InUser Current
        {
            get
            {
                return new InUser("current");
            }
        }

        public static InUser None
        {
            get
            {
                return new InUser("");
            }
        }

        public static InUser ById(string userId)
        {
            return new InUser(userId);
        }

        public override string ToString()
        {
            return m_userString;
        }
    }
}
