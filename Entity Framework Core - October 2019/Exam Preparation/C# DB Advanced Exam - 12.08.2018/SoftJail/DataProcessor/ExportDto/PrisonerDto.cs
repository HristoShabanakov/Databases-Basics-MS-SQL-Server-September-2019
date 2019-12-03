using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class PrisonerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IncarcerationDate { get; set; }

        [XmlArray]
        public MailDto[] EncryptedMessages { get; set; }
    }

    [XmlType("Message")]
    public class MailDto
    {
        public string Description { get; set; } 
    }
}
