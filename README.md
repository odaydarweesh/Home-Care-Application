Läs mig
För att användaren ska kunna använda applikationen för första gången måste han lägga till en ny användare med administratörsbehörighet. När applikationen byggs för första gången på emulatorn är databasen tom eftersom applikationen skapar den databasen på emulatorn. Så följande steg måste vidtas.
1- Ersätta LoginPage på rad 45 i App.xaml.CS-filen med NewUserPage Så att applikationen visar gränssnittet som visar skapandet av en ny användare.
2- När en ny administratör skapas kan LoginPage sedan rullas tillbaka till den tidigare platsen.
3- Starta om applikationen och logga in med den nya användaren.


![image](https://github.com/odaydarweesh/Home-Care-Application/assets/76429458/c136d707-8637-4131-95b1-fe9d761d43f3)
