namespace PenSamples.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SecretModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Text { get; set; }

        public string User { get; set; }
    }
}