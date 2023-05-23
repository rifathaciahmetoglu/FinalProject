using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);

            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//fiyatı en az 10 lira olacak ama ne zaman category id'si 1 ise
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");//ürün ismi a ile başlayacak.. Must bu metoda uymalı.
            //WithMessage ile kendi istediğimiz hata mesajını oluşturabiliriz.
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");//A ile başlayacak
        }
    }
}
