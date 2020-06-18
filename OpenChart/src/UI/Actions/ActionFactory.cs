namespace OpenChart.UI.Actions
{
    /// <summary>
    /// A factory class for creating IAction instances.
    /// </summary>
    public class ActionFactory
    {
        /// <summary>
        /// The application data.
        /// </summary>
        public ApplicationData ApplicationData { get; private set; }

        /// <summary>
        /// Creates a new ActionFactory instance.
        /// </summary>
        public ActionFactory(ApplicationData appData)
        {
            ApplicationData = appData;
        }
    }
}
