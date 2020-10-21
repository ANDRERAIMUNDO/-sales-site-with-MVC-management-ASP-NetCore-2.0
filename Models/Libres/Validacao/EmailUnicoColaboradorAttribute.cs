using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Libres.Validacao
{
    public class EmailUnicoColaboradorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //pega todo valor do email , obtem o repositor e compara com demais
            string Email = (value as string).Trim();//converte valor do email em string
            //string Email = value as string;
            IColaboradorRepository _colaboradorRepository = (IColaboradorRepository) validationContext.GetService(typeof(IColaboradorRepository));
            List<Colaborador> colaboradores = _colaboradorRepository.ObterColaboradorPorEmail(Email);
            Colaborador objcolaborador = (Colaborador)validationContext.ObjectInstance;
            if (colaboradores.Count > 1)
            {
                return new ValidationResult("Email ja existe");
            }
            if (colaboradores.Count == 1 && objcolaborador.Id != colaboradores[0].Id)
            {
                return new ValidationResult("Email ja existe");
            }
            return ValidationResult.Success;
           // return base.IsValid(value, validationContext);
        }
    }
}
