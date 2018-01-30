namespace Bazar.Models.Repository
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string MailFromAdress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MailToAdress { get; set; }
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
        public bool SSL { get; set; }
    }
}
