namespace Model.Core
{
    public interface ISortable
    {
        void Sort(bool ascending = false);
        void SortByName(bool ascending = false);
        void SortByPrice(bool ascending = false);
    }
}