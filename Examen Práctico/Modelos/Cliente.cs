namespace Examen_Práctico.Modelos
{
    public class Cliente
    {
        public int customerId { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCumpleaños { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<Direccion> address { get; set; }
    }
}
