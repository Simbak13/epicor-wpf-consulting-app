

namespace Epicor_Wpf_Analizer.Helpers
{
    public class FiltersParams
    {

        public string SearchText { get; set; }
        public string RowsNumber{ get; set; }

        public FiltersParams()
        {

        }

        public class FiltersParamsBuilder
        {
            private FiltersParams filtersParams;

            public FiltersParamsBuilder()
            {
                filtersParams = new FiltersParams();
            }

            public FiltersParamsBuilder WithSearchText(string searchText)
            {
                filtersParams.SearchText = searchText;
                return this;
            }

            public FiltersParamsBuilder WithRowsNumber(string rowsNumber)
            {
                filtersParams.RowsNumber = rowsNumber;
                return this;
            }


            public FiltersParams Build()
            {
                return filtersParams;
            }

        }

    }
}