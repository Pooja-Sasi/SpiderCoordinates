using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SpiderCoordinates.Models
{
    [Bind("MatrixCoordinates,StartingPoint,Direction,finalCoordinates")]
    public class SpiderCoordinatesViewModel
    {
        [Required(ErrorMessage = "Please Enter MatrixCoordinates:")]
        [DisplayName("Size of Matrix:")]
        //[Range(0, 100, ErrorMessage = "The value must be between 0-100 ")]
        [RegularExpression(@"^[0-9](,[0-9])$", ErrorMessage = "Please enter values in x,y format")]
        public string MatrixCoordinates { get; set; }

        [Required(ErrorMessage = "Please Enter StartingPoint:")]
        [DisplayName("Enter StartingPoint:")]
        [RegularExpression(@"^[0-9](,[0-9])(,[L-R])$", ErrorMessage = "Please enter values in [x,y,L/R] format")]
        public string StartingPoint { get; set; }
      
   

        [Required(ErrorMessage = "Please Enter Path/Direction to be followed")]
        [DisplayName("Enter Path/Direction to be followed:")]
        [RegularExpression("^[F-L-R]+$", ErrorMessage = "only 'F','L','R' alphabets")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string Direction { get; set; }

        [DisplayName("Final Coordinates:")]
        public string finalCoordinates { get; set; }

        //public bool IsPosted { get; set; }


    }
}
