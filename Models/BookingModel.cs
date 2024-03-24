// Kod tillhörande lösningen till projektuppgiften i kurs DT191G, skapat av Cecilia Edvardsson

using System.ComponentModel.DataAnnotations;

namespace Hairsalon.Models;

public class BookingModel {

    public int Id { get; set; }  

    [Display(Name ="Namn")]
    [Required(ErrorMessage = "Fyll i förnamn och efternamn.")]  
    public string? Name { get; set; }

    [Display(Name ="E-postadress")]
    [Required(ErrorMessage = "Fyll i e-postadress.")]
    public string?  Email { get; set; }

    [Display(Name ="Telefonnummer")]
    [Required(ErrorMessage = "Fyll i telefonnummer.")]
    public string?  Phonenr { get; set; }

    [Display(Name ="Datum")]
    [Required(ErrorMessage = "Fyll i önskat datum för behandlingen.")]
    public string?  AppointmentDate { get; set; }

    [Display(Name ="Tidsintervall")]
    [Required(ErrorMessage = "Fyll i önskat tidsintervall för behandlingen.")]
    public string?  AppointmentTime { get; set; }

    [Display(Name ="Frisör")]
    public string?  Stylist { get; set; }

    [Display(Name ="Status")]
    public string? Status { get; set; }

    [Display(Name ="Behandling")]
    public int? TreatmentModelId { get; set; } 

    [Display(Name ="Behandling")]
    public TreatmentModel? TreatmentModel { get; set; } 
}