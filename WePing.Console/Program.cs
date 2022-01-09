using System.Text.Json;
using WePing.Gir;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
ModeleRencontre model = new ModeleRencontre();
model.Parties.Add(new ModeleRencontrePartie() { JoueurA = "A", JoueurB = "X" });
Console.WriteLine(JsonSerializer.Serialize(model));
