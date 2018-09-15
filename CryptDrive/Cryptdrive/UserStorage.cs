namespace Cryptdrive
{
    class ActiveUserStorage
    {
        public static ActiveUserStorage instance = new ActiveUserStorage();

        public void setActiveUser(string username, string containername)
        {
            this.username = username;
            this.userContainerName = containername;
        }

        public string username { get; private set; }

        public string userContainerName { get; private set; }
    }
}
