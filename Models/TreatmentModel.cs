// Kod tillhörande lösningen till projektuppgiften i kurs DT191G, skapat av Cecilia Edvardsson

using System.ComponentModel.DataAnnotations;

namespace Hairsalon.Models;

public class TreatmentModel {

    // Properties
    public int Id { get; set; }  

    [Display(Name ="Namn")]
    [Required(ErrorMessage = "Fyll i namn.")]  
    public string? Name { get; set; }

    [Display(Name ="Pris")]
    [Required(ErrorMessage = "Fyll i pris.")]  
    public int?  Price { get; set; }

    [Display(Name ="Beskrivning")]
    public string?  Description { get; set; }
}