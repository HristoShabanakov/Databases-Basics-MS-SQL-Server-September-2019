﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.ImportDtos
{
    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Key")]
        [Required]
        [RegularExpression("^[A-Z,0-9]{4}-[A-Z,0-9]{4}-[A-Z,0-9]{4}$")]
        public string Key { get; set; }

        [XmlElement("Card")]
        public string Card { get; set; }

        [XmlElement("Date")]
        public string Date { get; set; }
    }
}
