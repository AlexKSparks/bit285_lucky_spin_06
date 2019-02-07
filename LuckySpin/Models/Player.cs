using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Player
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Please enter your Name")]
        public string FirstName { get; set; }

        [Range(1,9, ErrorMessage = "Choose a number")]
        public int Luck { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter a positive money amount")]
        public decimal Balance { get; set; }

    }
}