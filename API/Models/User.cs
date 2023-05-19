using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class User
    {

        public int Id { get; set; }
        public int DiscordId { get; set; }
        public int Experience { get; set; }
        
        [Precision(18,2)]
        public decimal Balance { get; set; }


        public User(int discordId)
        {
            DiscordId = discordId;
            Experience = 0;
            Balance = 0;
        }

        public void AddBalance(int value) {
            Balance += value;
        }

        public void AddExperience(int value) {  
            Experience += value; }
    }
}
