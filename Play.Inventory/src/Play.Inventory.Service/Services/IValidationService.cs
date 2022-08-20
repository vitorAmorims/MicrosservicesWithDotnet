namespace Play.Inventory.Service.Services
{
    public interface IValidationService
    {
         void Validate<T>(T obj);
    }
}
