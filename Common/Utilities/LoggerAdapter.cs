namespace Common.Utilities
{
    public class LoggerAdapter
    {
        private readonly ILogger _logger;

        public LoggerAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public void LogMessage(string message)
        {
            _logger.Log(message);
        }
    }
}
