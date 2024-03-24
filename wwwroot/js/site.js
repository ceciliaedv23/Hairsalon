// Kod tillhörande lösningen till projektuppgiften i kurs DT191G, skapat av Cecilia Edvardsson

let dateinput = document.getElementById("AppointmentDate");

if (document.querySelector("#AppointmentDate")) {
  dateinput.min = new Date().toISOString().split("T")[0];
}
