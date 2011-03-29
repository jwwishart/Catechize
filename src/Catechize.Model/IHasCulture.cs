using System.Globalization;

namespace Catechize.Model
{
    public interface IHasCulture
    {
        CultureInfo Culture { get; set; }
    }
}
