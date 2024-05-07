using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Services.Helpers
{
    public class FunctionHelpers
    {

        
        public static Dictionary<string, List<string>> GetSpecificationsByCategory(ICollection<CategorySpecsEntity> categorySpecifications, List<ProductEntity> products)
        {

            if (categorySpecifications == null)
            {
                return new Dictionary<string, List<string>>();
            }
            Dictionary<string, List<string>> specificationsDict = new Dictionary<string, List<string>>();

            foreach (var product in products)
            {
                foreach (var spec in categorySpecifications)
                {
                    var value = product.ProductSpecs.FirstOrDefault(ps => ps.Key == spec.Key)?.Value;

                    if (value != null)
                    {
                        if (!specificationsDict.ContainsKey(spec.Key))
                        {
                            specificationsDict[spec.Key] = new List<string>();
                        }
                        specificationsDict[spec.Key].Add(value);
                    }
                }
            }

            return specificationsDict;
        }
       


    }
}
