namespace ServiceLayer.IServices
{
    /// <summary>
    /// Interface for handling configuration-related operations.
    /// Provides methods to load configurations from a file and retrieve configuration values.
    /// </summary>
    public interface IConfigValue
    {
        /// <summary>
        /// Loads configuration settings from the specified file.
        /// </summary>
        /// <param name="filePath">The path to the configuration file.</param>
        void LoadConfiguration(string filePath);

        /// <summary>
        /// Retrieves the value associated with the specified key from the configuration.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key associated with the configuration value.</param>
        /// <returns>The value associated with the specified key.</returns>
        T GetValue<T>(string key);
    }
}
