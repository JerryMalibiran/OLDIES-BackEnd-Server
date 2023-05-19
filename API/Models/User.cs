namespace API.Models
{
    public class User
    {

        public int ID { get; set; }
        public int DiscordID { get; set; }
        public int Experience { get; set; }
        public decimal Balance { get; set; }


        public User(int id, int discordID)
        {
            this.ID = id;
            this.DiscordID = discordID;
            this.Experience = 0;
            this.Balance = 0;
        }

        public void AddBalance(int value) {
            Balance += value;
        }

        public void AddExperience(int value) {  
            Experience += value; }
    }
}
