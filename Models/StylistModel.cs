// Kod tillhörande lösningen till projektuppgiften i kurs DT191G, skapat av Cecilia Edvardsson

using System.ComponentModel.DataAnnotations;

namespace Hairsalon.Models;

public class StylistModel {

    // Properties
    public int Id { get; set; }  

    [Display(Name ="Förnamn")]
    [Required(ErrorMessage = "Fyll i förnamn.")]  
    public string? Firstname { get; set; }

    [Display(Name ="Efternamn")]
    [Required(ErrorMessage = "Fyll i efternamn.")]  
    public string? Lastname { get; set; }

    [Display(Name ="Beskrivning")]
    public string? Biography { get; set; }
}