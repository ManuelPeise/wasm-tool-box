namespace Web.Core.Client.Services.Interfaces
{
    public interface IValidationService<T> where T : class
    {
        event Action OnChange;
        T OriginalState { get; set; }
        T ValidationModel { get; set; }
        
        void OnValidate(out bool state);

        void ResetChanges();
    }
}
