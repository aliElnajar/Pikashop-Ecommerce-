namespace PikaShop.Common.Filtration
{
    public class FilterOption
    {
        public string Key { get; set; }

        public List<string> Options { get; set; }

        public FilterOption()
        {
            Key = string.Empty;
            Options = new List<string>();
        }
    }
}
