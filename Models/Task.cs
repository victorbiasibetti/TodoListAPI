using System.ComponentModel.DataAnnotations;
using System;

namespace API.Models 
{
  public class Task 
  {
    public Task() {
      this.Status = false;
      this.IsDeleted = false;
    }

    [Key]
    public int Id {get;set;}

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve ter entre 3 e 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve ter entre 3 e 60 caracteres")]
    public string Title {get;set;}
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MinLength(5, ErrorMessage = "Este campo deve conter no mínimo 5 caracteres")]
    public string Description {get;set;}

    public bool Status {get;set;}

    public bool IsDeleted {get;set;}

    public Nullable<DateTime> CreateDate {get;set;}

    public Nullable<DateTime> UpdateDate {get;set;}

    public Nullable<DateTime> CompleteDate {get;set;}

  }
}