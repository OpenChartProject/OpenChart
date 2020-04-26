namespace OpenChart.Formats.OpenChart.Version0_1.Data
{
    /// <summary>
    /// An interface for a data object which can validate its state.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Checks if the object's state is valid, otherwise throws an exception.
        /// </summary>
        void Validate();
    }
}
