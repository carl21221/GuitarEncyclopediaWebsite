using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Models
{
    public class Guitar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Color { get; set; }

        //Woods
        [Display(Name = "Fretboard Material")]
        public string FretboardMaterial { get; set; }
        [Display(Name = "Body Material")]
        public string BodyMaterial { get; set; }

        // Construction
        [Display(Name = "Neck Joint")]
        public string NeckJoint { get; set; }

        [Range(6,12)]
        [Display(Name = "String Count")]
        public int StringCount { get; set; }

        [Display(Name = "Fret Count")]
        public int FretCount { get; set; }

        [Display(Name = "Fret Size")]
        public string FretSize { get; set; }

        [Display(Name = "Inlays")]
        public string InlayStyle { get; set; }

        [Display(Name = "Binding")]
        public string Binding { get; set; }

        //Pickups
        [Display(Name = "Pickup Configuration")]
        public string PickupConfiguration { get; set; }
        [Display(Name = "Bridge Pickup")]
        public string BridgePickup { get; set; }
        [Display(Name = "Middle Pickup")]
        public string MiddlePickup { get; set; }
        [Display(Name = "Neck Pickup")]
        public string NeckPickup { get; set; }
        [Display(Name = "Pickup Switch Type")]
        public string PickupSwitch { get; set; }
        public bool ActivePickups { get; set; }

        [Display(Name = "Bridge Type")]
        public string BridgeType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
    }
}
