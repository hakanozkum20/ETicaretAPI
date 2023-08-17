using FluentValidation;
using Namespace;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Lutfen ürün adını boş bırakmayınız.")
            .MaximumLength(150)
            .MinimumLength(5)
            .WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Lütfen stok bilgisini boş bırakmayınız.")
            .Must(s => s >= 0)
            .WithMessage("Stok bilgisi negatif olamaz!!.");

            RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Lütfen fiyat bilgisini boş bırakmayınız.")
            .Must(s => s >= 0)
            .WithMessage("Fiyat bilgisi negatif olamaz!!.");
        }

    }
}

