//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFDemo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kunden
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kunden()
        {
            this.Bestellungen = new HashSet<Bestellungen>();
        }
    
        public int Id { get; set; }
        public string Firma { get; set; }
        public string Name { get; set; }
        public string Straße { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public Nullable<System.DateTime> Geburtsdatum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bestellungen> Bestellungen { get; set; }
    }
}
