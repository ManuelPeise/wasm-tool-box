using Newtonsoft.Json;
using Web.Core.Client.Services.Interfaces;

namespace Web.Core.Client.Services
{
    public class ValidationService<T> : IValidationService<T> where T : class
    {

        public T ValidationModel { get; set; } = default(T);
        public T OriginalState { get; set; } = default(T);
        
        public event Action OnChange;

        public void OnValidate(out bool state)
        {
            state = JsonConvert.SerializeObject(ValidationModel).Equals(JsonConvert.SerializeObject(OriginalState));
        }

        public void ResetChanges()
        {
            var json = JsonConvert.SerializeObject(OriginalState);

            ValidationModel = JsonConvert.DeserializeObject<T>(json);
        }
    }
}
