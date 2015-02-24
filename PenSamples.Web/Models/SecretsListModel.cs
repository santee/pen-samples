namespace PenSamples.Web.Models
{
    using System.Collections.Generic;

    public class SecretsListModel
    {
        public string User { get; set; }

        public IEnumerable<SecretModel> Secrets { get; set; }
    }
}