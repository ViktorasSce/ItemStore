namespace ItemStore.CustomException
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base("Item was not found")
        {

        }
    }
}
