namespace PenSamples.Web.Models
{
    using System.Collections.Generic;

    public class SecretsListModel
    {
        public string User { get; set; }

        public string Filter { get; set; }

        public IEnumerable<SecretModel> Secrets { get; set; }
    }
}