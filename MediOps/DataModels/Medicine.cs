using System;
using System.ComponentModel.DataAnnotations;

namespace MediOps.DataModels
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(50),MinLength(5)]
        public string Name { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string Brand { get; set; }
        //todo : limit upto 2 decimal places
        [Required]
        public decimal Price { get; set; }
        [Required]
        public long Quantity { get; set; }
        private DateTime _expiryDate { get; set; }
        //todo : Validate Expiry Date
        [Required]
        public string ExpiryDate { get { return _expiryDate.ToString(); } set{ this._expiryDate = DateTime.Parse(value); } }
        [Required]
        public string Notes { get; set; }
        // Expiry Date validation handled on server side as Date on client side 
        // vary as per the user time setting
        public bool IsExpired { get { return ExpireDtLessThan30Days(); } }

        private bool ExpireDtLessThan30Days()
        {
            return _expiryDate <= (DateTime.Now + TimeSpan.FromDays(30)) ? true : false;
        }

    }
}
