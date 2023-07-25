using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Q2.DataAccess;

public partial class Service
{
    public int Id { get; set; }

    public string? RoomTitle { get; set; }

    public byte? Month { get; set; }

    public int? Year { get; set; }

    public string? FeeType { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? Employee { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public virtual Employee? EmployeeNavigation { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public virtual Room? RoomTitleNavigation { get; set; }
}
