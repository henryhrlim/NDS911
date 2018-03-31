using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Report911insert
/// </summary>
public class Report911insert
{
   
    public string CallerName { get; set; }
    public string CallerNRIC { get; set; }
    public int CallBackNo { get; set; }
    public string LocationofEmergency { get; set; }
    public DateTime DatetimeOfEmergency { get; set; }
    public string TypeOfSuperBeing { get; set; }
    public int EstimatedNoOfBeings { get; set; }
    public int EstimatedNoOfInjuries { get; set; }
    public int EstimatedNoOfDeath { get; set; }
    public string DescOfEmergency { get; set; }
    public string StaffId { get; set; }
    public string AlertedCMOId { get; set; }
    public DateTime IncidentCreatedDatetime { get; set; }
    public string NatureOfEmergency { get; set; }
    public string NameOfEmergency { get; set; }
}
